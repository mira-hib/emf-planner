import test from "node:test";
import assert from "node:assert/strict";
import { readFileSync, readdirSync } from "node:fs";
import JSZip from "jszip";
import { newProject, newSource } from "../src/lib/project.ts";
import { pasteSourceCells, SOURCE_CLIPBOARD_COLUMNS, sourceCellPatch } from "../src/lib/source-clipboard.ts";
import { convertedPowerFields } from "../src/lib/power-conversion.ts";
import { readAntennaLibrary, readAntennaZip } from "../src/lib/antenna-library.ts";
import { pasteLimitCells, limitFrequencyWarnings } from "../src/lib/limit-clipboard.ts";
import { nativeLimitAt, EMPTY_LIMIT } from "../src/lib/limits.ts";
import { nativeBeamWidth } from "../src/lib/source-fields.ts";
import { parseMsi, decodeMsi, writeMsi } from "../src/lib/antenna.ts";
import { feederLoss, symmetricPattern, envelopeForSource } from "../src/lib/editing.ts";
import feeders from "../src/data/feeders.json";
const root = "test/emf_test_kit/data/";
const text = (name: string) => readFileSync(root + name, "utf8");
const p = () => newProject();
const close = (actual: number, expected: number, tolerance = .0001) => assert.ok(Number.isFinite(actual) && Math.abs(actual - expected) <= tolerance, `${actual} != ${expected}`);

test("SRC-02 independent twelve-column paste equals the headered oracle in every source field", () => {
  const project = p(); let rows = project.sources;
  const files = readdirSync(root + "sources/columns").sort();
  files.forEach((file, i) => { rows = pasteSourceCells(rows, project.origin, text(`sources/columns/${file}`), rows[0]?.id, SOURCE_CLIPBOARD_COLUMNS[i], SOURCE_CLIPBOARD_COLUMNS); });
  const oracle = pasteSourceCells([], project.origin, text("sources/sources_oracle.tsv"), undefined, "name");
  assert.equal(rows.length, oracle.length);
  for (let i = 0; i < oracle.length; i++) for (const key of SOURCE_CLIPBOARD_COLUMNS) assert.equal(rows[i][key], oracle[i][key], `${i}:${key}`);
});
test("SRC-02 transactional source paste supports decimal commas, reordered columns, growth and rejects partial invalid batches", () => {
  const project = p(), source = newSource(project.origin, "old");
  const rows = pasteSourceCells([source], project.origin, "12,05\t3\n20\t2", source.id, "power_w", ["power_w", "num_tx"]);
  assert.equal(rows[0].power_w, 12.05); assert.equal(rows[0].num_tx, 3); assert.equal(rows[1].power_w, 20);
  assert.throws(() => pasteSourceCells([source], project.origin, "12\nNaN", source.id, "power_w", ["power_w"]));
  assert.equal(source.power_w, 0); assert.equal(source.name, "old");
});
test("SRC-10 native invalid azimuth sentinel is retained while valid boundaries remain exact", () => {
  for (const value of ["x", "360", "-1", "Infinity"]) assert.equal(sourceCellPatch("azimuth_deg", value).azimuth_deg, -1);
  assert.equal(sourceCellPatch("azimuth_deg", "359,99").azimuth_deg, 359.99);
  assert.equal(sourceCellPatch("azimuth_deg", "").azimuth_deg, 0);
});
test("SRC-11 synchronized native converter shows 12.05 W as 12050 mW, 10.8 dBW and 40.8 dBm", () => {
  assert.deepEqual(convertedPowerFields("12.05", "W"), { W: "12.05", mW: "12050", dBW: "10.8", dBm: "40.8" });
  assert.equal(convertedPowerFields("40.8", "dBm").W, "12.0226");
});
test("SRC-12 converter validates every kit unit case and never returns NaN or infinity for zero, negatives or text", () => {
  for (const line of text("units/unit_conversion_cases.tsv").trim().split(/\r?\n/).slice(1)) {
    const cells = line.split("\t"), value = Number(cells[0]);
    if (!Number.isFinite(value) || value <= 0) { assert.throws(() => convertedPowerFields(cells[0], "W")); continue; }
    const fields = convertedPowerFields(cells[0], "W");
    assert.equal(Number(fields.mW), Number((value * 1000).toFixed(1)));
    close(Number(fields.dBW), 10 * Math.log10(value), .0500001);
    close(Number(fields.dBm), 10 * Math.log10(value) + 30, .0500001);
  }
  for (const invalid of ["0", "-5", "texte", "NaN", "Infinity", ""]) assert.throws(() => convertedPowerFields(invalid, "W"));
});
const feeder = (name: string) => { const found = feeders.find((row) => row.name.includes(name)); assert.ok(found); return found.points; };
test("SRC-13 SRC-14 native feeder calculation matches the manual baseline and excluded connector branch", () => {
  const cable = feeder("LDF5"), jumper = feeder("LDF1");
  const loss = feederLoss(cable, 900, 50, jumper, "3+3", 2, .05, 0);
  close(loss.total, 3.018, .001);
  const excluded = feederLoss(cable, 900, 50, jumper, "3+3", 2, .05, 0, true, false);
  close(loss.total - excluded.total, loss.connectors * .05, .001);
  assert.throws(() => feederLoss(cable, 900, -1, jumper, "3+3", 2, .05, 0));
});
test("SRC-15 feeder attenuation increases at all four kit frequencies", () => {
  const values = [900, 1800, 2600, 3500].map((frequency) => feederLoss(feeder("LDF5"), frequency, 50, feeder("LDF1"), "3+3", 2, .05, 0).total);
  assert.ok(values.every((value, i) => !i || value > values[i - 1]));
});
test("SRC-20 native transmitter count floors fractions and rejects every out-of-range kit input", () => {
  assert.equal(sourceCellPatch("num_tx", "1.5").num_tx, 1);
  assert.equal(sourceCellPatch("num_tx", "99.9").num_tx, 99);
  for (const invalid of [0, -1, 1000, "text"]) assert.throws(() => sourceCellPatch("num_tx", invalid));
});
test("SRC-28 sector MSI native -3 dB widths match 65 and 7 degrees", () => {
  const antenna = parseMsi(text("msi/ok_sector65x7_17dBi.msi"));
  close(nativeBeamWidth(antenna.horizontal), 65, 1); close(nativeBeamWidth(antenna.vertical), 7, 1);
});
test("SRC-32 explicit dBd gain is converted into dBi", () => {
  assert.equal(parseMsi(text("msi/gain_in_dBd.msi")).gain, 2.15);
});
test("SRC-33 directory and ZIP loading preserve Unicode file names, handle empty folders and reject bad files individually", async () => {
  const bytes = readFileSync(root + "msi/ok_utf8_bom.msi");
  const files = [{ name: "Dossier/émetteur_日本.MSI", bytes: async () => bytes }, { name: "bad.msi", bytes: async () => new Uint8Array() }, { name: "ignore.txt", bytes: async () => bytes }];
  const direct = await readAntennaLibrary(files);
  assert.equal(direct.patterns.length, 1); assert.equal(direct.patterns[0].file_name, "émetteur_日本.MSI"); assert.equal(direct.patterns[0].name, parseMsi(decodeMsi(bytes)).name); assert.equal(direct.rejected.length, 1);
  assert.deepEqual((await readAntennaLibrary([])).patterns, []);
  const zip = new JSZip(); zip.file(files[0].name, bytes); zip.file("bad.msi", "");
  const packed = await readAntennaZip(await zip.generateAsync({ type: "uint8array" }));
  assert.equal(packed.patterns[0].name, direct.patterns[0].name); assert.deepEqual(packed.patterns[0].horizontal, direct.patterns[0].horizontal);
  await assert.rejects(readAntennaZip(new Uint8Array([1, 2, 3])));
});
test("SRC-33 10000-file folder yields progress to the event loop and refuses a larger directory", async () => {
  const bytes = readFileSync(root + "msi/ok_isotropic.msi"), updates: number[] = [];
  const files = Array.from({ length: 10000 }, (_, i) => ({ name: `${i}.msi`, bytes: async () => bytes }));
  const result = await readAntennaLibrary(files, (done) => updates.push(done));
  assert.equal(result.patterns.length, 10000); assert.equal(result.rejected.length, 0); assert.equal(new Set(result.patterns.map((row) => row.id)).size, 10000);
  assert.ok(updates.length >= 200); assert.equal(updates.at(-1), 10000);
  await assert.rejects(readAntennaLibrary([...files, files[0]]), /10 000/);
});
test("SRC-35 envelope matches an independent per-angle maximum for both gain choices and omits an unchecked pattern", () => {
  const patterns = ["env_tilt0.msi", "env_tilt3.msi", "env_tilt6.msi"].map((name, i) => ({ ...parseMsi(text(`msi/envelope/${name}`)), id: String(i), gain: [17, 16.8, 16.5][i] }));
  for (const withGains of [false, true]) for (const chosen of [patterns, patterns.slice(0, 2)]) {
    const output = envelopeForSource(chosen, "envelope", 17, withGains);
    for (const plane of ["horizontal", "vertical"] as const) {
      const max = Array.from({ length: 360 }, (_, angle) => Math.max(-60, ...chosen.map((row) => (withGains ? row.gain : 0) - row[plane][angle] + Math.min(...row[plane]))));
      const peak = Math.max(...max);
      output[plane].forEach((loss, angle) => close(loss, peak - max[angle], .00001));
    }
    const reimported = parseMsi(writeMsi(output.name, output.gain, output.horizontal, output.vertical));
    output.vertical.forEach((loss, angle) => close(reimported.vertical[angle], loss, .00005));
  }
});
test("SRC-36 historical F699 diagram matches each integer-angle kit oracle within its native 0.1 dB precision", () => {
  const generated = symmetricPattern(7000, 31, .6);
  for (const line of text("msi/f699_expected_7GHz_31dBi_0.6m.tsv").trim().split(/\r?\n/).slice(1)) {
    const [angle, , attenuation] = line.split("\t").map(Number); if (Number.isInteger(angle)) close(generated.horizontal[angle], attenuation, .051);
  }
});
test("SRC-38 historical generator rejects nonfinite, zero diameter, excessive gain and out-of-range inputs explicitly", () => {
  for (const [f, g, d] of [[86001, 31, .6], [7000, -5, .6], [7000, 31, 0], [7000, 31, 1e6], [NaN, 31, .6]]) assert.throws(() => symmetricPattern(f, g, d));
  assert.equal(symmetricPattern(99, 31, .6).horizontal.length, 360); // Native/guide disagreement, deliberately visible.
});
test("LIM-02 flat kit TSV paste changes every coefficient and all four shared frequencies", () => {
  const result = pasteLimitCells(p().limits, text("limits/limits_flat.tsv"));
  [6, 20, 4.5, 10].forEach((value, i) => { assert.equal(result[i].rows[0].frequency, 1e6); assert.equal(result[i].rows[0].value, value); assert.equal(result[i].rows[0].exponent, 0); });
});
test("LIM-03 ICNIRP kit TSV paste retains piecewise intervals and inclusive native 30 MHz boundary", () => {
  const project = p(); project.limits.forEach((limit) => { limit.rows = []; });
  const result = pasteLimitCells(project.limits, text("limits/limits_icnirp.tsv"));
  close(nativeLimitAt(result[0], 30), 824 / 30, .00001);
  close(nativeLimitAt(result[0], 900), Math.fround(1.585) * Math.sqrt(900), .00001);
  close(nativeLimitAt(result[0], 2100), 61.4, .00001);
});
test("LIM-05 invalid pasted frequencies abort the transaction without corrupting existing limits", () => {
  const original = p().limits, before = JSON.stringify(original);
  for (const value of ["0", "-900", "1000001", "text", "Infinity"]) assert.throws(() => pasteLimitCells(original, value));
  assert.equal(JSON.stringify(original), before);
});
test("LIM-06 large paste is bounded, duplicate and unsorted frequencies are signalled with native order preserved", () => {
  const original = p().limits;
  const data = Array.from({ length: 1000 }, (_, i) => String(i + 1)).join("\n");
  const result = pasteLimitCells(original, data);
  assert.equal(result[0].rows.length, 1000); assert.equal(result[3].rows[999].frequency, 1000);
  const badOrder = pasteLimitCells(original, "900\n300\n300");
  assert.equal(limitFrequencyWarnings(badOrder).length, 2); assert.equal(badOrder[0].rows[1].frequency, 300);
  const beyond = pasteLimitCells(original, "1", original[0].rows.length, 0);
  assert.equal(beyond[0].rows.at(-1)!.value, EMPTY_LIMIT);
});
