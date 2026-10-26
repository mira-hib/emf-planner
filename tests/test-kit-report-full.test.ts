import test from "node:test";
import assert from "node:assert/strict";
import JSZip from "jszip";
import * as THREE from "three";
import { newProject, newSource, newPoint } from "../src/lib/project.ts";
import { renderWordTemplate } from "../src/lib/word-template.ts";
import { REPORT_ALIASES, SOURCE_REPORT_HEADERS, reportAliasDocument, tableAxes, DEFAULT_TABLE_SAMPLING, sampleVerticalTable, pointPolar, nativeSuperFormat, DEFAULT_REPORT_ALIASES } from "../src/lib/report-content.ts";
import { sourceWorkbookBytes, importExcel } from "../src/lib/files.ts";
import { sceneMeshes, meshesIdtf, embedU3dPdf } from "../src/lib/interactive-pdf.ts";
import { exposureAt } from "../src/lib/engine.ts";
const fixture = () => { const p = newProject(); p.sources = [{ ...newSource(p.origin, "Émetteur & <A>"), is_existing: false, frequency_mhz: 900, frequency_end_mhz: 950 }]; p.control_points = [{ ...newPoint(p.origin, "P1"), x_m: .3, y_m: -5.1 }]; p.vertical_cuts = [{ id: "v1", active: true, azimuth_deg: 0, x: 0, y: 0, backward_m: 10, forward_m: 200, max_height_m: 50 }]; return p; };
const xmlBody = (body: string, root = "document") => `<?xml version="1.0"?><w:${root} xmlns:w="http://schemas.openxmlformats.org/wordprocessingml/2006/main" xmlns:r="http://schemas.openxmlformats.org/officeDocument/2006/relationships">${root === "document" ? `<w:body>${body}</w:body>` : body}</w:${root}>`;
async function template(body: string) { const zip = new JSZip(); zip.file("word/document.xml", xmlBody(body)); zip.file("[Content_Types].xml", '<Types xmlns="http://schemas.openxmlformats.org/package/2006/content-types"><Default Extension="xml" ContentType="application/xml"/><Override PartName="/word/document.xml" ContentType="application/vnd.openxmlformats-officedocument.wordprocessingml.template.main+xml"/></Types>'); zip.file("_rels/.rels", '<Relationships xmlns="http://schemas.openxmlformats.org/package/2006/relationships"><Relationship Id="rId1" Type="http://schemas.openxmlformats.org/officeDocument/2006/relationships/officeDocument" Target="word/document.xml"/></Relationships>'); return zip; }
const p = (text: string) => `<w:p><w:r><w:t xml:space="preserve">${text}</w:t></w:r></w:p>`;

test("RPT-03 RPT-04: literal aliases span runs and replace headers, footers, tables and nested textbox paragraphs", async () => {
  const project = fixture(); project.report_aliases = { "nom avec espace": 'É & < > " ; ☃', Empty: "" };
  const zip = await template(p("{nom ").replace("</w:p>", '<w:r><w:t>avec espace}</w:t></w:r></w:p>') + `<w:tbl><w:tr><w:tc>${p("{Empty}")}</w:tc></w:tr></w:tbl>` + `<w:p><w:r><w:drawing><w:txbxContent>${p("{nom avec espace}")}</w:txbxContent></w:drawing></w:r></w:p>`);
  zip.file("word/header1.xml", xmlBody(p("{nom avec espace}"), "hdr")); zip.file("word/footer1.xml", xmlBody(p("{Empty}"), "ftr"));
  const before = await zip.generateAsync({ type: "uint8array" }), original = new Uint8Array(before);
  const result = await renderWordTemplate(project, before); assert.equal(result.replaced, 5); assert.deepEqual(result.unknownAliases, []); assert.deepEqual(before, original);
  const output = await JSZip.loadAsync(result.bytes), xml = await output.file("word/document.xml")!.async("string");
  assert.equal(xml.includes("{Empty}"), false); assert.equal((xml.match(/É &amp; &lt; &gt; &quot; ; ☃/g) ?? []).length, 2);
  assert.match(await output.file("word/header1.xml")!.async("string"), /É &amp;/); assert.doesNotMatch(await output.file("word/footer1.xml")!.async("string"), /Empty/);
});
test("RPT-02 RPT-08: binary/invalid templates fail explicitly; unknown aliases are reported without dropping content", async () => {
  await assert.rejects(renderWordTemplate(fixture(), new Uint8Array([0xd0, 0xcf, 0x11, 0xe0])), /binaire/);
  const zip = await template(p("Unchanged {missing}")); const result = await renderWordTemplate(newProject(), await zip.generateAsync({ type: "uint8array" }));
  assert.deepEqual(result.unknownAliases, ["missing"]); assert.match(await (await JSZip.loadAsync(result.bytes)).file("word/document.xml")!.async("string"), /Unchanged/);
});
test("RPT-06 RPT-10: all structural aliases produce actual Word tables, geometry or chart media; new sources shaded", async () => {
  const project = fixture(); project.saved_cut_results = { horizontal: [{ id: "h", cut_id: project.horizontal_cuts[0].id, contours_only: true, grid: { width: 0, height: 0, x0: 0, y0: 0, dx: 1, dy: 1, values: [], fields: [], height_m: 1.5 }, contours: { level: 1, color: "red", height_m: 1.5, segments: [[[0, 0], [10, 10], [0, 0]]] } }], vertical: [] };
  for (const alias of REPORT_ALIASES) {
    const bytes = await reportAliasDocument(project, alias), zip = await JSZip.loadAsync(bytes), xml = await zip.file("word/document.xml")!.async("string");
    assert.doesNotMatch(xml, new RegExp(`\\{${alias}\\}`));
    if (alias === "RadiatingSources_1") { assert.equal(SOURCE_REPORT_HEADERS.length, 17); assert.match(xml, /CCCCCC/); assert.match(xml, /Owner/); assert.match(xml, /Émetteur &amp; &lt;A&gt;/); }
    if (["CardinalPointsChart", "AntennaPatterns", "HorizontalCutsA4_1", "HorizontalCutsA3_1", "EFieldStrengthGraph_1", "SimultaneousExposureGraph_1"].includes(alias)) { const media = Object.keys(zip.files).filter((n) => n.endsWith(".svg")); assert.ok(media.length, alias); assert.match(await zip.file(media[0])!.async("string"), /polyline/); }
    if (alias.includes("Tables")) { assert.match(xml, /Height m \/ Distance m/); assert.ok((xml.match(/<w:tbl>/g) ?? []).length >= 5); }
  }
});
test("RPT-04 RPT-06: template structural replacement merges real image relationships and content types", async () => {
  const zip = await template(p("{AntennaPatterns}") + p("{RadiatingSources_1}"));
  const result = await renderWordTemplate(fixture(), await zip.generateAsync({ type: "uint8array" })), output = await JSZip.loadAsync(result.bytes);
  const xml = await output.file("word/document.xml")!.async("string"), rels = await output.file("word/_rels/document.xml.rels")!.async("string");
  assert.match(xml, /rIdReport/); assert.match(xml, /<w:tbl/); assert.match(rels, /\/image/);
  for (const match of rels.matchAll(/Target="([^"]+)"/g)) assert.ok(output.file(`word/${match[1]}`), match[1]);
  assert.match(await output.file("[Content_Types].xml")!.async("string"), /image\/svg\+xml/);
});
test("PNT-11 DOC-10: Word polar convention and original SuperFormat prefix truncation", () => {
  for (const [x, y, distance, azimuth] of [[.3, -5.1, 5.11, 176.63], [-28.2, -81.8, 86.52, 199.02], [111.3, 33.5, 116.23, 73.25], [-40.3, 68.8, 79.73, 329.64]]) { const polar = pointPolar(x, y); assert.ok(Math.abs(polar.distance - distance) < .01); assert.ok(Math.abs(polar.azimuth - azimuth) < .01); }
  assert.equal(nativeSuperFormat(13.9988, 6), "13.998"); assert.equal(nativeSuperFormat(.123456789, 8), "0.123456");
});
test("TBL-01 TBL-02 TBL-03: default 41×26 grid, limits, rejected steps and same engine values as points/graphs", () => {
  const axes = tableAxes(DEFAULT_TABLE_SAMPLING); assert.equal(axes.distances.length, 41); assert.equal(axes.heights.length, 26);
  for (const distanceStep of [0, -1, 201, 1e6]) assert.throws(() => tableAxes({ ...DEFAULT_TABLE_SAMPLING, distanceStep }));
  assert.throws(() => tableAxes({ ...DEFAULT_TABLE_SAMPLING, distance: 1e6 }));
  assert.equal(tableAxes({ ...DEFAULT_TABLE_SAMPLING, distance: 203 }).distances.at(-1), 200);
  const project = fixture(), sample = sampleVerticalTable(project, project.vertical_cuts[0]); const actual = exposureAt(project, 0, 100, 20, 0);
  assert.equal(sample.rows[10][20].total_field, actual.total_field); assert.equal(sample.rows[10][20].exposure_ratio, actual.exposure_ratio);
});
test("SRC-27: active-only native 19-column workbook round trips frequencies, units and New/Existing", async () => {
  const project = fixture(); project.sources[0].mechanical_downtilt_deg = -3; project.sources[0].electrical_downtilt_deg = 7; project.sources[0].location = "Roof"; project.sources.push({ ...newSource(project.origin, "Inactive"), active: false });
  const bytes = await sourceWorkbookBytes(project), zip = await JSZip.loadAsync(bytes); assert.match(await zip.file("xl/worksheets/sheet1.xml")!.async("string"), /dimension ref="A1:S2"/);
  const sources = await importExcel(project, bytes); assert.equal(sources.length, 1); assert.equal(sources[0].name, project.sources[0].name); assert.equal(sources[0].frequency_end_mhz, 950); assert.equal(sources[0].mechanical_downtilt_deg, -3); assert.equal(sources[0].electrical_downtilt_deg, 7); assert.equal(sources[0].is_existing, false); assert.equal(sources[0].location, "Roof");
});
test("HCT-13: scene exports transformed triangles and real U3D annotation; invalid encoder result rejected", () => {
  const scene = new THREE.Group(), box = new THREE.Mesh(new THREE.BoxGeometry(1, 2, 3), new THREE.MeshBasicMaterial({ color: "red" })); box.position.set(20, 30, 40); scene.add(box);
  const meshes = sceneMeshes(scene); assert.equal(meshes[0].faces.length / 3, 12); assert.equal(Math.min(...meshes[0].positions.filter((_, i) => i % 3 === 0)), 19.5); assert.match(meshesIdtf(meshes), /FACE_COUNT 12/);
  assert.throws(() => embedU3dPdf(new Uint8Array([1, 2, 3]), meshes), /U3D/);
  // Structural packaging check only; this synthetic stream does not certify a viewer-compatible U3D encoder.
  const pdf = new TextDecoder().decode(embedU3dPdf(new Uint8Array([85, 51, 68, 0, 0]), meshes)); assert.match(pdf, /\/Subtype \/3D/); assert.match(pdf, /\/Subtype \/U3D/); assert.match(pdf, /\/VA \[6 0 R\]/); assert.match(pdf, /startxref/);
});
test("RPT-05: native six default aliases can be restored independently per project", () => { const a = DEFAULT_REPORT_ALIASES(), b = DEFAULT_REPORT_ALIASES(); a.Customer = "Changed"; assert.equal(b.Customer, "Customer"); assert.equal(Object.keys(b).length, 6); });
