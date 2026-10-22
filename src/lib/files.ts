import { isTauri } from "@tauri-apps/api/core";
import { normalizeProject, newSource, uid } from "./project";
import { importXemf } from "./import-xemf";
import { calculatePoints } from "./engine";
import { buildCadDrawing, cadDrawingDxf, type CadOptions } from "./cad";
import { cutNumericSummary } from "./cut-report";
import { formatCoordinate } from "./coordinates";
import type {
  Contour,
  PlannerProject,
  PointResult,
  RadiatingSource,
} from "../types";

export async function saveBytes(
  name: string,
  bytes: Uint8Array,
  mime: string,
): Promise<boolean> {
  if (isTauri()) {
    const { save } = await import("@tauri-apps/plugin-dialog"),
      { writeFile } = await import("@tauri-apps/plugin-fs");
    const ext = name.split(".").pop()!;
    const path = await save({
      defaultPath: name,
      filters: [{ name: ext.toUpperCase(), extensions: [ext] }],
    });
    if (!path) return false;
    await writeFile(path, bytes);
    return true;
  }
  const url = URL.createObjectURL(
      new Blob([new Uint8Array(bytes)], { type: mime }),
    ),
    a = document.createElement("a");
  a.href = url;
  a.download = name;
  a.click();
  setTimeout(() => URL.revokeObjectURL(url), 10000);
  return true;
}
export const saveText = (name: string, text: string, mime = "text/plain") =>
  saveBytes(name, new TextEncoder().encode(text), mime);
export async function chooseFile(
  extensions: string[],
): Promise<{ name: string; bytes: Uint8Array } | null> {
  if (isTauri()) {
    const { open } = await import("@tauri-apps/plugin-dialog"),
      { readFile } = await import("@tauri-apps/plugin-fs");
    const path = await open({
      multiple: false,
      filters: [{ name: "Fichiers", extensions }],
    });
    return typeof path === "string"
      ? { name: path.split(/[\\/]/).pop()!, bytes: await readFile(path) }
      : null;
  }
  return new Promise((resolve) => {
    const input = document.createElement("input");
    input.type = "file";
    input.accept = extensions.map((e) => `.${e}`).join(",");
    input.oncancel = () => resolve(null);
    input.onchange = async () => {
      const file = input.files?.[0];
      resolve(
        file
          ? { name: file.name, bytes: new Uint8Array(await file.arrayBuffer()) }
          : null,
      );
    };
    input.click();
  });
}
export function readProject(name: string, bytes: Uint8Array): PlannerProject {
  if (bytes[0] === 0)
    return normalizeProject(importXemf(new Uint8Array(bytes).buffer, name));
  return normalizeProject(JSON.parse(new TextDecoder().decode(bytes)));
}
export async function saveProject(project: PlannerProject) {
  return saveText(
    `${project.name}.emfp`,
    JSON.stringify({ ...project, format_version: 2 }, null, 2),
    "application/json",
  );
}
const SOURCE_COLUMNS: [keyof RadiatingSource, string][] = [
  ["name", "Source"],
  ["sector_id", "Secteur"],
  ["service_type", "Service"],
  ["frequency_mhz", "Fréquence MHz"],
  ["frequency_end_mhz", "Fréquence fin MHz"],
  ["power_w", "Puissance W"],
  ["num_tx", "Nombre TX"],
  ["total_attenuation_db", "Atténuation dB"],
  ["antenna_model", "Antenne"],
  ["azimuth_deg", "Azimut °"],
  ["gain_dbi", "Gain dBi"],
  ["height_m", "Hauteur m"],
  ["antenna_size_m", "Taille m"],
  ["mechanical_downtilt_deg", "Tilt mécanique °"],
  ["electrical_downtilt_deg", "Tilt électrique °"],
  ["beam_width_h_deg", "Ouverture H °"],
  ["beam_width_v_deg", "Ouverture V °"],
  ["location", "Position"],
  ["owner", "Propriétaire"],
  ["lat", "Latitude"],
  ["lng", "Longitude"],
  ["alpha24", "Alpha24"],
  ["duty_cycle", "Ftdc"],
  ["power_reduction", "Fpr"],
];
const resultHeaders = [
  "Point",
  "X (m)",
  "Y (m)",
  "Hauteur (m)",
  "E calculé (V/m)",
  "Exposition (%)",
  "Densité (W/m²)",
  "E mesuré (V/m)",
];
const resultRows = (results: PointResult[]) =>
  results.map((r) => [
    r.name,
    r.x,
    r.y,
    r.height_m,
    r.total_field,
    r.exposure_ratio * 100,
    r.total_power_density,
    r.measured,
  ]);
export async function exportExcel(
  project: PlannerProject,
  includeSources = true,
) {
  const { default: ExcelJS } = await import("exceljs"),
    book = new ExcelJS.Workbook();
  book.creator = "EMF Planner";
  book.subject = `Coordonnées du site : ${siteCoordinatesText(project)}`;
  const sources = book.addWorksheet("Sources");
  sources.addRow(SOURCE_COLUMNS.map((c) => c[1]));
  sources.addRow(SOURCE_COLUMNS.map((c) => c[0]));
  sources.getRow(2).hidden = true;
  project.sources.forEach((s) =>
    sources.addRow(SOURCE_COLUMNS.map(([key]) => s[key] as string | number)),
  );
  const points = book.addWorksheet("Points de contrôle");
  points.addRow(resultHeaders);
  resultRows(calculatePoints(project)).forEach((r) => points.addRow(r));
  for (const sheet of [sources, points]) {
    sheet.getRow(1).font = { bold: true, color: { argb: "FFFFFFFF" } };
    sheet.getRow(1).fill = {
      type: "pattern",
      pattern: "solid",
      fgColor: { argb: "FF35658A" },
    };
    sheet.columns.forEach((c) => {
      c.width = 20;
    });
    sheet.views = [{ state: "frozen", ySplit: sheet === sources ? 2 : 1 }];
  }
  if (!includeSources) book.removeWorksheet(sources.id);
  const bytes = await book.xlsx.writeBuffer();
  return saveBytes(
    `${project.name}.xlsx`,
    new Uint8Array(bytes),
    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
  );
}
export async function importExcel(
  project: PlannerProject,
  bytes: Uint8Array,
): Promise<RadiatingSource[]> {
  const { default: ExcelJS } = await import("exceljs"),
    book = new ExcelJS.Workbook();
  await book.xlsx.load(new Uint8Array(bytes).buffer);
  const sheet = book.getWorksheet("Sources") ?? book.worksheets[0];
  if (!sheet) throw new Error("Aucune feuille dans le classeur.");
  const aliases = new Map<string, keyof RadiatingSource>(
    SOURCE_COLUMNS.flatMap(([key, title]) => [
      [String(key), key],
      [title.toLowerCase(), key],
    ]),
  );
  const row1 = (sheet.getRow(1).values as unknown[]).slice(1),
    row2 = (sheet.getRow(2).values as unknown[]).slice(1),
    hasKeys = row2.some((v) => v === "frequency_mhz"),
    headers = hasKeys ? row2 : row1;
  const keys = headers.map((h) => aliases.get(String(h ?? "").toLowerCase()));
  if (!keys.includes("frequency_mhz") || !keys.includes("power_w"))
    throw new Error(
      "Colonnes requises : frequency_mhz / Fréquence MHz et power_w / Puissance W. Exportez un classeur modèle depuis l’application.",
    );
  const rows: RadiatingSource[] = [];
  sheet.eachRow((row, index) => {
    if (index <= (hasKeys ? 2 : 1) || !row.hasValues) return;
    const source = newSource(project.origin, `Source ${rows.length + 1}`);
    keys.forEach((key, i) => {
      if (!key) return;
      const v = row.getCell(i + 1).value;
      if (v === null || v === undefined) return;
      if (typeof v === "object")
        throw new Error(
          `Cellule complexe ou formule non prise en charge, ligne ${index}.`,
        );
      if (typeof source[key] === "number" || key === "frequency_end_mhz") {
        const number = Number(String(v).replace(",", "."));
        if (!Number.isFinite(number))
          throw new Error(`Nombre invalide, ligne ${index}.`);
        Object.assign(source, { [key]: number });
      } else Object.assign(source, { [key]: String(v) });
    });
    source.x_m = undefined;
    source.y_m = undefined;
    rows.push(source);
  });
  normalizeProject({ ...project, sources: rows });
  return rows;
}
export async function exportWord(
  project: PlannerProject,
  includeSources = false,
) {
  const {
    Document,
    Packer,
    Paragraph,
    Table,
    TableRow,
    TableCell,
    TextRun,
    WidthType,
  } = await import("docx");
  const data = [
    resultHeaders,
    ...resultRows(calculatePoints(project)).map((r) =>
      r.map((v) => (typeof v === "number" ? v.toFixed(6) : v)),
    ),
  ];
  const table = (data: (string | number)[][]) =>
    new Table({
      width: { size: 100, type: WidthType.PERCENTAGE },
      rows: data.map(
        (row, i) =>
          new TableRow({
            children: row.map(
              (v) =>
                new TableCell({
                  children: [
                    new Paragraph({
                      children: [
                        new TextRun({
                          text: String(v),
                          bold: i === 0,
                          size: 16,
                        }),
                      ],
                    }),
                  ],
                }),
            ),
          }),
      ),
    });
  const sourceData = [
    ["Source", "MHz", "Puissance TX, W", "Gain, dBi", "Hauteur, m"],
    ...project.sources.map((s) => [
      s.name,
      s.frequency_mhz,
      s.power_w,
      s.gain_dbi,
      s.height_m,
    ]),
  ];
  const doc = new Document({
    sections: [
      {
        properties: { page: { size: { orientation: "landscape" } } },
        children: [
          new Paragraph({
            children: [
              new TextRun({ text: project.name, bold: true, size: 30 }),
            ],
          }),
          new Paragraph(project.description),
          new Paragraph(
            `Coordonnées du site : ${siteCoordinatesText(project)}`,
          ),
          new Paragraph(
            `Jeu de limites : ${project.limits[project.settings.selected_limit].name}`,
          ),
          ...(includeSources
            ? [
                new Paragraph({
                  text: "Sources rayonnantes",
                  heading: "Heading2",
                }),
                table(sourceData),
              ]
            : []),
          new Paragraph({ text: "Calculs aux points", heading: "Heading2" }),
          table(data),
        ],
      },
    ],
  });
  return saveBytes(
    `${project.name}.docx`,
    new Uint8Array(await Packer.toArrayBuffer(doc)),
    "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
  );
}
export async function exportPdf(
  project: PlannerProject,
  includeSources = false,
) {
  const { jsPDF } = await import("jspdf"),
    { default: autoTable } = await import("jspdf-autotable");
  const pdf = new jsPDF({ orientation: "landscape" });
  pdf.setFontSize(16);
  pdf.text(project.name, 14, 16);
  pdf.setFontSize(9);
  pdf.text(
    `Limites : ${project.limits[project.settings.selected_limit].name} | ${project.sources.length} sources`,
    14,
    23,
  );
  const site = project.settings.site_coordinates ?? project.origin;
  pdf.text(`Site : ${site[0].toFixed(7)}; ${site[1].toFixed(7)}`, 14, 28);
  if (includeSources) {
    autoTable(pdf, {
      startY: 34,
      head: [["Source", "MHz", "Puissance TX, W", "Gain, dBi", "Hauteur, m"]],
      body: project.sources.map((s) => [
        s.name,
        s.frequency_mhz,
        s.power_w,
        s.gain_dbi,
        s.height_m,
      ]),
      styles: { fontSize: 8 },
      headStyles: { fillColor: [53, 101, 138] },
    });
    pdf.addPage();
    pdf.text("Calculs aux points", 14, 20);
  }
  autoTable(pdf, {
    startY: includeSources ? 30 : 34,
    head: [resultHeaders],
    body: resultRows(calculatePoints(project)).map((r) =>
      r.map((v) => (typeof v === "number" ? v.toFixed(6) : v)),
    ),
    styles: { fontSize: 8 },
    headStyles: { fillColor: [53, 101, 138] },
  });
  return saveBytes(
    `${project.name}.pdf`,
    new Uint8Array(pdf.output("arraybuffer")),
    "application/pdf",
  );
}
export function projectDxf(
  project: PlannerProject,
  contours: Contour[] = [],
  options?: { paper?: Partial<CadOptions> },
): string {
  if (options?.paper)
    return cadDrawingDxf(buildCadDrawing(project, contours, options.paper));
  const pairs: (string | number)[] = [];
  const add = (...p: (string | number)[]) => pairs.push(...p);
  add(
    0,
    "SECTION",
    2,
    "HEADER",
    9,
    "$INSUNITS",
    70,
    6,
    0,
    "ENDSEC",
    0,
    "SECTION",
    2,
    "ENTITIES",
  );
  for (const s of project.sources)
    add(
      0,
      "POINT",
      8,
      "ANTENNAS",
      10,
      s.x_m ?? 0,
      20,
      s.y_m ?? 0,
      30,
      s.height_m,
    );
  for (const b of project.buildings) {
    add(
      0,
      "LWPOLYLINE",
      8,
      "BUILDINGS",
      90,
      b.points.length,
      70,
      1,
      38,
      b.foundation_m,
    );
    for (const [x, y] of b.points) add(10, x, 20, y);
  }
  for (const l of project.lines) {
    if (l.kind === "label") {
      add(
        0,
        "TEXT",
        8,
        "LABELS",
        10,
        l.points[0]?.[0] ?? 0,
        20,
        l.points[0]?.[1] ?? 0,
        40,
        2.5,
        1,
        l.name.replace(/[\r\n]/g, " "),
      );
    } else {
      add(0, "LWPOLYLINE", 8, l.kind.toUpperCase(), 90, l.points.length, 70, 0);
      for (const [x, y] of l.points) add(10, x, 20, y);
    }
  }
  for (const p of project.control_points)
    add(0, "POINT", 8, "CONTROL_POINTS", 10, p.x_m, 20, p.y_m, 30, p.height_m);
  for (const [i, c] of contours.entries()) {
    const rgb = parseInt(c.color.replace("#", ""), 16);
    for (const [a, b] of c.segments)
      add(
        0,
        "LINE",
        8,
        `EMF_${i + 1}_H${c.height_m}`,
        420,
        Number.isFinite(rgb) ? rgb : 0xff0000,
        10,
        a[0],
        20,
        a[1],
        30,
        c.height_m,
        11,
        b[0],
        21,
        b[1],
        31,
        c.height_m,
      );
  }
  add(0, "ENDSEC", 0, "EOF");
  return pairs.join("\r\n") + "\r\n";
}
export function importDxf(
  project: PlannerProject,
  text: string,
): PlannerProject {
  if (/^AC10\d\d/.test(text) || text.includes("AutoCAD Binary DXF"))
    throw new Error(
      "Ce fichier est un DWG ou un DXF binaire. Convertissez-le en DXF texte avant l’import.",
    );
  if (!/\bSECTION\s/.test(text) || !/\bENTITIES\s/.test(text))
    throw new Error("Fichier DXF texte invalide : section ENTITIES absente.");
  const lines = text.split(/\r?\n/),
    entities: { type: string; pairs: [number, string][] }[] = [];
  let current: (typeof entities)[number] | undefined;
  for (let i = 0; i + 1 < lines.length; i += 2) {
    const code = Number(lines[i].trim()),
      value = lines[i + 1].trim();
    if (code === 0) {
      current = { type: value, pairs: [] };
      entities.push(current);
    } else current?.pairs.push([code, value]);
  }
  const buildings = [...project.buildings],
    drawing = [...project.lines];
  for (const e of entities) {
    const get = (code: number) => e.pairs.find((p) => p[0] === code)?.[1],
      number = (code: number) => Number(get(code) ?? 0);
    if (e.type === "LWPOLYLINE") {
      const xs = e.pairs.filter((p) => p[0] === 10).map((p) => Number(p[1])),
        ys = e.pairs.filter((p) => p[0] === 20).map((p) => Number(p[1]));
      if (
        xs.length !== ys.length ||
        xs.length < 2 ||
        !xs.concat(ys).every(Number.isFinite)
      )
        throw new Error("Polyligne DXF invalide.");
      const points = xs.map((x, i) => [x, ys[i]] as [number, number]);
      if (number(70) & 1)
        buildings.push({
          id: uid(),
          name: `Bâtiment ${buildings.length + 1}`,
          points,
          height_m: 10,
          foundation_m: number(38),
          attenuation_db: 20,
          active: true,
        });
      else
        drawing.push({
          id: uid(),
          name: get(8) ?? "Ligne",
          kind: "road",
          color: "#555555",
          width: 1,
          points,
        });
    }
    if (e.type === "TEXT")
      drawing.push({
        id: uid(),
        name: get(1) ?? "",
        kind: "label",
        color: "#333333",
        width: 1,
        points: [[number(10), number(20)]],
      });
  }
  if (
    buildings.length === project.buildings.length &&
    drawing.length === project.lines.length
  )
    throw new Error(
      "Aucune polyligne LWPOLYLINE ou annotation TEXT reconnue dans ce DXF.",
    );
  return { ...project, buildings, lines: drawing };
}

export async function exportCutReport(
  project: PlannerProject,
  kind: "horizontal" | "vertical",
  cutResults: {
    id: string;
    cut_id?: string;
    grid: import("../types").FieldGrid;
    contours: Contour;
  }[],
) {
  const { jsPDF } = await import("jspdf");
  const { autoTable } = await import("jspdf-autotable");
  const doc = new jsPDF({ orientation: "landscape" });
  doc.setFontSize(14);
  doc.text(
    `Rapport des coupes ${kind === "horizontal" ? "horizontales" : "verticales"} — ${project.name}`,
    14,
    15,
  );
  doc.setFontSize(10);
  doc.text(
    `Limites : ${project.limits[project.settings.selected_limit]?.name ?? "—"}`,
    14,
    22,
  );
  doc.setFontSize(8);
  doc.text(
    "Cellules vides : valeurs numériques indisponibles (contours importés ou coupe non calculée).",
    14,
    28,
  );

  if (kind === "horizontal") {
    const cuts = project.horizontal_cuts.filter((c) => c.active);
    const headers = [
      "Hauteur, m",
      "Contour",
      "E max, V/m",
      "Ratio max",
      "Points",
    ];
    const rows = cuts.map((cut) => {
      const results = cutResults.filter(
        (r) => r.grid.height_m === cut.height_m,
      );
      const { maxField, maxRatio, samples } = cutNumericSummary(
        results.map((r) => r.grid),
      );
      const segs = results.reduce((n, r) => n + r.contours.segments.length, 0);
      return [
        cut.height_m.toFixed(1),
        segs > 0 ? "Oui" : "Non",
        maxField?.toFixed(4) ?? "",
        maxRatio?.toFixed(6) ?? "",
        samples === null ? "" : String(samples),
      ];
    });
    autoTable(doc, { head: [headers], body: rows, startY: 33 });
  } else {
    const cuts = project.vertical_cuts.filter((c) => c.active);
    const headers = [
      "Azimut, °",
      "Avant, m",
      "Arrière, m",
      "Hmax, m",
      "E max, V/m",
      "Ratio max",
    ];
    const rows = cuts.map((cut) => {
      const results = cutResults.filter((r) => r.cut_id === cut.id);
      const { maxField, maxRatio } = cutNumericSummary(
        results.map((r) => r.grid),
      );
      return [
        cut.azimuth_deg.toFixed(1),
        cut.forward_m.toFixed(0),
        cut.backward_m.toFixed(0),
        cut.max_height_m.toFixed(0),
        maxField?.toFixed(4) ?? "",
        maxRatio?.toFixed(6) ?? "",
      ];
    });
    autoTable(doc, { head: [headers], body: rows, startY: 33 });
  }
  const bytes = doc.output("arraybuffer");
  await saveBytes(
    `${project.name}_coupes_${kind}.pdf`,
    new Uint8Array(bytes),
    "application/pdf",
  );
}

export const STANDARD_ALIASES: Record<string, string> = {
  PROJET: "Nom du projet",
  DESCRIPTION: "Description du projet",
  DATE: "Date du rapport",
  AUTEUR: "Auteur du rapport",
  LIMITES: "Jeu de limites sélectionné",
  NB_SOURCES: "Nombre de sources",
  NB_POINTS: "Nombre de points de contrôle",
  RAYON: "Rayon de la zone de calcul, m",
  PAS: "Pas de la grille, m",
  REFLEXION: "Coefficient de réflexion",
  NB_BATIMENTS: "Nombre de bâtiments",
  NB_COUPES_H: "Nombre de coupes horizontales",
  NB_COUPES_V: "Nombre de coupes verticales",
  CONFORME: "Conformité globale (Oui/Non)",
};

export function resolveAliases(
  project: PlannerProject,
  template: string,
): string {
  const limit = project.limits[project.settings.selected_limit];
  const pts = calculatePoints(project);
  const values: Record<string, string> = {
    PROJET: project.name,
    DESCRIPTION: project.description,
    DATE: new Date().toLocaleDateString("fr-FR"),
    AUTEUR: "",
    LIMITES: limit?.name ?? "",
    NB_SOURCES: String(project.sources.length),
    NB_POINTS: String(project.control_points.length),
    RAYON: String(project.settings.radius_m),
    PAS: String(project.settings.grid_step_m),
    REFLEXION: String(project.settings.reflection),
    NB_BATIMENTS: String(project.buildings.length),
    NB_COUPES_H: String(project.horizontal_cuts.length),
    NB_COUPES_V: String(project.vertical_cuts.length),
    CONFORME: pts.every((p) => p.exposure_ratio <= 1) ? "Oui" : "Non",
    Coordinates: siteCoordinatesText(project),
    ...(project.settings.report_aliases ?? {}),
  };
  return template.replace(/\{(\w+)\}/g, (m, key) => values[key] ?? m);
}

export function siteCoordinatesText(project: PlannerProject): string {
  const [lat, lng] = project.settings.site_coordinates ?? project.origin;
  const format = project.settings.coordinate_format ?? "decimal";
  return `${formatCoordinate(lat, format, true)}; ${formatCoordinate(lng, format, false)}`;
}

/** Native Word-template processing is deliberately unavailable until implemented. */
export async function processWordTemplate(
  _project: PlannerProject,
  _templateBytes: Uint8Array,
): Promise<boolean> {
  throw new Error(
    "Le traitement des modèles Word n’est pas encore implémenté.",
  );
}
