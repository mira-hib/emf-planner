import { Document, Packer, Paragraph, Table, TableCell, TableRow, TextRun, ImageRun, PageBreak, WidthType } from "docx";
import type { PlannerProject, RadiatingSource, VerticalCut } from "../types";
import { calculatePoints, exposureAt, eirp, limitAt, patternAmplitude } from "./engine";
import { terrainHeight } from "./geometry";

export const REPORT_ALIASES = ["CardinalPointsChart", "ControlPoints_1", "EMF_Safety_Volumes1", "AntennaPatterns", "HorizontalCutsA4_1", "HorizontalCutsA3_1", "VerticalCuts_1", "EFieldStrengthGraph_1", "SimultaneousExposureGraph_1", "RadiatingSources_1", "ControlPoints_2", "EFieldStrengthVerticalCutTables_1", "SimultaneousExposureVerticalCutTables_1"] as const;
export type ReportAlias = typeof REPORT_ALIASES[number];
export const DEFAULT_REPORT_ALIASES = () => ({ ProjectCode: "ProjectCode", ProjectName: "ProjectName", SiteAddress: "SiteAddress", Customer: "Customer", Designer: "Designer", Date: new Date().toLocaleDateString("en-GB") });
export const canonicalReportAlias = (key: string) => key.replace(/А/g, "A");
export function nativeSuperFormat(value: number, length = 8): string { return Math.fround(value).toFixed(8).slice(0, length); }
export function pointPolar(x: number, y: number) { return { distance: Math.hypot(x, y), azimuth: (Math.atan2(x, y) * 180 / Math.PI + 360) % 360 }; }
export interface TableSampling { distance: number; distanceStep: number; height: number; heightStep: number; limitIndex: number }
export const DEFAULT_TABLE_SAMPLING: TableSampling = { distance: 200, distanceStep: 5, height: 50, heightStep: 2, limitIndex: 0 };
export function tableAxes(options: TableSampling) {
  const { distance: d, distanceStep: ds, height: h, heightStep: hs } = options;
  if (![d, ds, h, hs].every(Number.isFinite) || d < 10 || d > 10000 || h < 1 || h > 1000 || ds < .1 || ds > 100 || hs < .1 || hs > 50 || ds >= d || hs >= h) throw new Error("Distance 10–10000 m, hauteur 1–1000 m ; pas positifs inférieurs aux étendues (distance 0,1–100 m, hauteur 0,1–50 m).");
  const nx = Math.floor(d / ds) + 1, ny = Math.floor(h / hs) + 1;
  if (nx * ny > 100000) throw new Error("Tableau limité à 100 000 cellules. Augmentez les pas.");
  return { distances: Array.from({ length: nx }, (_, i) => i * ds), heights: Array.from({ length: ny }, (_, i) => i * hs) };
}
export function sampleVerticalTable(project: PlannerProject, cut: VerticalCut, options: TableSampling = DEFAULT_TABLE_SAMPLING) {
  const axes = tableAxes(options), az = cut.azimuth_deg * Math.PI / 180;
  return { ...axes, rows: axes.heights.map((h) => axes.distances.map((d) => exposureAt(project, cut.x + d * Math.sin(az), cut.y + d * Math.cos(az), h + terrainHeight(project, cut.x, cut.y), options.limitIndex))) };
}
export function sourceSafetyExtents(project: PlannerProject, source: RadiatingSource) {
  const limit = limitAt(project.limits[project.settings.selected_limit], source.frequency_mhz);
  const mask = project.limits[project.settings.selected_limit];
  const factors = source.limit_factors?.[project.settings.selected_limit] ?? [source.alpha24, source.duty_cycle, source.power_reduction];
  const factor = (mask.use_alpha24 ? factors[0] : 1) * (mask.use_ftdc ? factors[1] : 1) * (mask.use_fpr ? factors[2] : 1);
  const forward = project.settings.reflection * Math.sqrt(30 * eirp(source) * factor) / limit;
  const polar = (angle: number, vertical: boolean) => forward * patternAmplitude(vertical ? source.pattern_v : source.pattern_h, angle, vertical ? source.beam_width_v_deg : source.beam_width_h_deg);
  const sample = (vertical: boolean) => Array.from({ length: 360 }, (_, a) => { const r = polar(a, vertical), rad = a * Math.PI / 180; return [r * Math.cos(rad), r * Math.sin(rad)]; });
  const h = sample(false), v = sample(true);
  return { forward, Lv: Math.max(...v.map(p => p[1])) - Math.min(...v.map(p => p[1])),
    Lh: Math.max(...h.map(p => p[1])) - Math.min(...h.map(p => p[1])),
    Lm1: Math.max(...h.map(p => p[0])), Lm2: Math.abs(Math.min(...h.map(p => p[0]))) };
}
type Block = Paragraph | Table;
const para = (text: string) => new Paragraph({ children: [new TextRun({ text, font: "Times New Roman", size: 20 })] });
function table(headers: string[], rows: (string | number)[][], shade?: boolean[]): Table {
  return new Table({ width: { size: 100, type: WidthType.PERCENTAGE }, rows: [headers, ...rows].map((row, i) => new TableRow({ tableHeader: i === 0, children: row.map((value) => new TableCell({ shading: i === 0 ? { fill: "E8E8E8" } : shade?.[i - 1] ? { fill: "CCCCCC" } : undefined, children: [para(String(value))] })) })) });
}
const fmt = (n: number, digits = 2) => Number.isFinite(n) ? String(Number(n.toFixed(digits))) : "—";
export const SOURCE_REPORT_HEADERS = ["№", "Name", "Type of service", "Frequency MHz", "Power W", "TX", "Loss dB", "Antenna", "Azimuth °", "Gain dBi", "Height m", "Size m", "Mech. tilt °", "Elec. tilt °", "H beam °", "V beam °", "Owner"];
function svgPlot(title: string, series: { name: string; points: [number, number][] }[], width = 720, height = 320): string {
  const all = series.flatMap((s) => s.points), xmax = Math.max(1, ...all.map((p) => p[0])), ymax = Math.max(1e-9, ...all.map((p) => p[1])), colors = ["#215e9b", "#cf5324", "#248345", "#a238a2"];
  const escape = (s: string) => s.replace(/[&<>\"]/g, (c) => ({ "&": "&amp;", "<": "&lt;", ">": "&gt;", '"': "&quot;" })[c]!);
  return `<svg xmlns="http://www.w3.org/2000/svg" width="${width}" height="${height}" viewBox="0 0 ${width} ${height}"><rect width="100%" height="100%" fill="white"/><text x="40" y="22" font-size="16">${escape(title)}</text><path d="M45 40V280H700" stroke="black" fill="none"/><text x="48" y="55" font-size="12">${fmt(ymax, 5)}</text><text x="645" y="305" font-size="12">${fmt(xmax)} m/°</text>${series.map((s, i) => `<polyline fill="none" stroke="${colors[i % colors.length]}" stroke-width="1.8" points="${s.points.map(([x, y]) => `${45 + x / xmax * 650},${280 - y / ymax * 235}`).join(" ")}"/><text x="${60 + (i % 4) * 155}" y="${height - 3}" fill="${colors[i % colors.length]}" font-size="11">${escape(s.name)}</text>`).join("")}</svg>`;
}
// Word 2019+ consumes the SVG; older Word receives a raster fallback generated in the browser.
async function image(svg: string): Promise<Paragraph> {
  let png = Uint8Array.from(atob("iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAQAAAC1HAwCAAAAC0lEQVR42mP8/x8AAwMCAO+jRZkAAAAASUVORK5CYII="), (c) => c.charCodeAt(0));
  if (typeof document !== "undefined") {
    const blob = URL.createObjectURL(new Blob([svg], { type: "image/svg+xml" }));
    try { const img = new Image(); img.src = blob; await img.decode(); const canvas = document.createElement("canvas"); canvas.width = 1440; canvas.height = 640; canvas.getContext("2d")!.drawImage(img, 0, 0, 1440, 640); png = Uint8Array.from(atob(canvas.toDataURL("image/png").split(",")[1]), (c) => c.charCodeAt(0)); } finally { URL.revokeObjectURL(blob); }
  }
  return new Paragraph({ children: [new ImageRun({ type: "svg", data: new TextEncoder().encode(svg), fallback: { type: "png", data: png }, transformation: { width: 680, height: 302 } })] });
}
export async function reportAliasBlocks(project: PlannerProject, requested: string, sampling?: TableSampling): Promise<Block[]> {
  const alias = canonicalReportAlias(requested), sources = project.sources.filter((s) => s.active !== false), points = calculatePoints(project);
  if (alias === "RadiatingSources_1") return [table(SOURCE_REPORT_HEADERS, sources.map((s, i) => [i + 1, s.name, s.service_type, s.frequency_end_mhz ? `${s.frequency_mhz}-${s.frequency_end_mhz}` : s.frequency_mhz, s.power_w, s.num_tx, fmt(s.total_attenuation_db, 4), s.antenna_model, s.azimuth_deg, s.gain_dbi, s.height_m, s.antenna_size_m, s.mechanical_downtilt_deg, s.electrical_downtilt_deg, s.beam_width_h_deg, s.beam_width_v_deg, s.owner]), sources.map((s) => s.is_existing === false))];
  if (alias === "ControlPoints_1" || alias === "ControlPoints_2") {
    const blocks: Block[] = [table(["Point", "Azimuth °", "X m", "Y m", "R m", "Location", "Height m", "Exposure %", "E V/m", "Density W/m²"], points.map((p) => { const polar = pointPolar(p.x, p.y); return [p.name, fmt(polar.azimuth), fmt(p.x, 1), fmt(p.y, 1), fmt(polar.distance), project.control_points.find((c) => c.id === p.point_id)?.location ?? "", p.height_m, nativeSuperFormat(p.exposure_ratio * 100), nativeSuperFormat(p.total_field), nativeSuperFormat(p.total_power_density)]; }))];
    if (alias === "ControlPoints_2") for (const p of points) blocks.push(para(p.name), table(["Source", "Distance m", "E V/m", "Density W/m²", "Exposure %"], p.per_source.map((s) => [sources.find((v) => v.id === s.source_id)?.name ?? s.source_id, fmt(s.distance_m), nativeSuperFormat(s.field_v_m), nativeSuperFormat(s.power_density_w_m2), nativeSuperFormat(s.ratio_contribution * 100)])));
    return blocks;
  }
  if (alias === "EMF_Safety_Volumes1") return [table(["Source", "Lv m", "Lh m", "Lm1 m", "Lm2 m"], sources.map((s) => { const v = sourceSafetyExtents(project, s); return [s.name, fmt(v.Lv), fmt(v.Lh), fmt(v.Lm1), fmt(v.Lm2)]; }))];
  if (alias === "AntennaPatterns") return (await Promise.all(sources.map(async (s) => [para(`${s.name} — ${s.antenna_model}`), await image(svgPlot("Antenna patterns (relative amplitude)", [{ name: "Horizontal", points: Array.from({ length: 361 }, (_, a) => [a, patternAmplitude(s.pattern_h, a, s.beam_width_h_deg)] as [number, number]) }, { name: "Vertical", points: Array.from({ length: 361 }, (_, a) => [a, patternAmplitude(s.pattern_v, a, s.beam_width_v_deg)] as [number, number]) }]))]))).flat();
  if (alias === "CardinalPointsChart") return [await image(svgPlot("Cardinal points: N 0° · E 90° · S 180° · W 270°", sources.map((s) => ({ name: s.name, points: Array.from({ length: 361 }, (_, a) => [a, patternAmplitude(s.pattern_h, a - s.azimuth_deg, s.beam_width_h_deg)] as [number, number]) }))))];
  const vertical = project.vertical_cuts.filter((c) => c.active);
  if (alias.includes("VerticalCutTables")) {
    const blocks: Block[] = [], exposure = alias.startsWith("Simultaneous");
    for (const cut of vertical) { const sample = sampleVerticalTable(project, cut, sampling); blocks.push(para(`Vertical cut along azimuth ${cut.azimuth_deg}°`)); for (let start = 0; start < sample.distances.length; start += 10) { if (start) blocks.push(new Paragraph({ children: [new PageBreak()] })); blocks.push(table(["Height m / Distance m", ...sample.distances.slice(start, start + 10).map(String)], sample.rows.map((row, y) => [sample.heights[y], ...row.slice(start, start + 10).map((r) => nativeSuperFormat(exposure ? r.exposure_ratio * 100 : r.total_field))]))); } }
    return blocks.length ? blocks : [para("No active vertical cuts.")];
  }
  if (alias.endsWith("Graph_1")) {
    const exposure = alias.startsWith("Simultaneous"), opts = sampling ?? DEFAULT_TABLE_SAMPLING, axes = tableAxes(opts);
    return [await image(svgPlot(exposure ? "Simultaneous exposure, %" : "E field strength, V/m", vertical.map((c) => ({ name: `${c.azimuth_deg}°`, points: axes.distances.map((d) => { const a = c.azimuth_deg * Math.PI / 180, r = exposureAt(project, c.x + d * Math.sin(a), c.y + d * Math.cos(a), opts.height + terrainHeight(project, c.x, c.y), opts.limitIndex); return [d, exposure ? r.exposure_ratio * 100 : r.total_field]; }) }))))];
  }
  if (alias === "VerticalCuts_1" || alias.startsWith("HorizontalCuts")) {
    const saved = alias === "VerticalCuts_1" ? project.saved_cut_results?.vertical ?? [] : project.saved_cut_results?.horizontal ?? [];
    const cuts = alias === "VerticalCuts_1" ? vertical : project.horizontal_cuts.filter((c) => c.active);
    const blocks: Block[] = [];
    for (const [i, cut] of cuts.entries()) { if (i) blocks.push(new Paragraph({ children: [new PageBreak()] })); const title = "azimuth_deg" in cut ? `Vertical cut along azimuth ${cut.azimuth_deg}° (along direction of antenna ${sources.find((s) => s.azimuth_deg === cut.azimuth_deg)?.name ?? "—"})` : `Horizontal cut at height ${cut.height_m} m`; blocks.push(para(title)); const contours = saved.filter((r) => r.cut_id === cut.id).flatMap((r) => r.contours.segments); if (contours.length) { const xs = contours.flatMap((s) => s.map((p) => p[0])), ys = contours.flatMap((s) => s.map((p) => p[1])), minx = Math.min(...xs), miny = Math.min(...ys); blocks.push(await image(svgPlot(title, contours.map((s, n) => ({ name: `Contour ${n + 1}`, points: s.map(([x, y]) => [x - minx, y - miny]) }))))); } else blocks.push(para("No calculated contours. Run the calculation before exporting cut geometry.")); }
    return blocks.length ? blocks : [para("No active cuts.")];
  }
  throw new Error(`Alias de rapport inconnu : {${requested}}`);
}
export async function reportAliasDocument(project: PlannerProject, alias: string, sampling?: TableSampling): Promise<Uint8Array> {
  return new Uint8Array(await Packer.toBuffer(new Document({ sections: [{ properties: { page: { size: { width: alias.includes("A3") ? 23811 : 16838, height: alias.includes("A3") ? 16838 : 11906 }, margin: { top: 720, bottom: 720, left: 720, right: 720 } } }, children: [para(project.name), ...await reportAliasBlocks(project, alias, sampling)] }] })));
}
