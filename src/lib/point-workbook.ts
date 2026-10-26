import ExcelJS from "exceljs";
import type { ControlPoint, PlannerProject } from "../types";
import { newPoint, uid } from "./project";
import { toGeo, toLocal } from "./geometry";
import { validateArchive } from "./archive-validation";
const HEADERS = ["Name", "Group", "Latitude", "Longitude", "X_m", "Y_m", "Location", "Description", "Height_m", "Attenuation_dB", "Measured_E_Vm", "Height_description", "Point_data"];
const key = (value: unknown) => String(value ?? "").toLowerCase().normalize("NFD").replace(/[\u0300-\u036f\s_°,./()-]/g, "");
function number(value: unknown, label: string) {
  if (value === undefined || value === null || String(value).trim() === "") return undefined;
  const n = Number(String(value).trim().replace(",", "."));
  if (!Number.isFinite(n)) throw new Error(`${label} : valeur numérique invalide.`);
  return n;
}
export function importPointRows(project: PlannerProject, rows: unknown[][], priority: "geographic" | "local" = "geographic"): ControlPoint[] {
  if (!rows.length || rows.length > 20001) throw new Error("Le tableau doit contenir un en-tête et au maximum 20 000 lignes.");
  const headers = rows[0].map(key), output = new Map<string, ControlPoint>();
  const column = (...names: string[]) => headers.findIndex(header => names.some(name => header === key(name)));
  const c = { name: column("Name", "Point", "Nom"), group: column("Group", "Groupe"), lat: column("Latitude", "lat"), lng: column("Longitude", "lng", "lon"), x: column("X_m", "X"), y: column("Y_m", "Y"), location: column("Location", "Position"), description: column("Description"), height: column("Height_m", "Height", "Hauteur"), loss: column("Attenuation_dB"), measured: headers.findIndex(header => header.startsWith("measurede")), heightDescription: column("Height_description"), data: column("Point_data") };
  if ((c.lat < 0 || c.lng < 0) && (c.x < 0 || c.y < 0)) throw new Error("Colonnes Latitude/Longitude ou X/Y manquantes.");
  for (const [index, row] of rows.slice(1).entries()) {
    if (row.every(value => value === null || value === undefined || value === "")) continue;
    if (row.length > headers.length) throw new Error(`Ligne ${index + 2} : trop de colonnes.`);
    let stored: Partial<ControlPoint> = {};
    if (c.data >= 0 && row[c.data]) { stored = JSON.parse(String(row[c.data])); if (!stored || typeof stored !== "object" || Array.isArray(stored)) throw new Error("Métadonnées de point invalides."); }
    const read = (col: number) => col < 0 ? undefined : row[col], numeric = (col: number, label: string) => number(read(col), label);
    let lat = numeric(c.lat, "Latitude"), lng = numeric(c.lng, "Longitude"), x = numeric(c.x, "X"), y = numeric(c.y, "Y");
    if (lat !== undefined && Math.abs(lat) > 85 || lng !== undefined && Math.abs(lng) > 180) throw new Error("Coordonnées géographiques hors limites.");
    if (priority === "local" && x !== undefined && y !== undefined || lat === undefined || lng === undefined) {
      if (x === undefined || y === undefined) throw new Error("Paire de coordonnées incomplète.");
      [lat, lng] = toGeo(x, y, project.origin);
    } else if (stored.id && x !== undefined && y !== undefined) {
      // Our workbook preserves both anchors exactly; external files use geographic priority.
    } else [x, y] = toLocal(lat, lng, project.origin);
    const name = String(read(c.name) ?? stored.name ?? index + 1), group = String(read(c.group) ?? stored.group ?? "Groupe 1");
    const identity = String(stored.id ?? `${group}:${name}:${lat}:${lng}`);
    const point = output.get(identity) ?? { ...newPoint(project.origin, name), ...stored, name, group, lat, lng, x_m: x!, y_m: y!, heights: [] };
    const height = numeric(c.height, "Hauteur") ?? (c.measured >= 0 ? Number(String(rows[0][c.measured]).match(/@h([\d.]+)/)?.[1] ?? 1.5) : stored.height_m ?? 1.5);
    const loss = numeric(c.loss, "Affaiblissement") ?? 0, measured = numeric(c.measured, "Champ mesuré") ?? 0;
    if (height < 0 || height > 10000 || loss < 0 || measured < 0) throw new Error("Hauteur, affaiblissement ou mesure hors limites.");
    const location = String(read(c.location) ?? stored.location ?? "Ground");
    if (!["Ground", "Building", "Roof"].includes(location)) throw new Error("Position de point invalide.");
    Object.assign(point, { location, description: String(read(c.description) ?? stored.description ?? "") });
    point.heights!.push({ id: stored.heights?.[point.heights!.length]?.id ?? uid(), height_m: height, attenuation_db: loss, measured, description: String(read(c.heightDescription) ?? "") });
    if (point.heights!.length === 1) Object.assign(point, { height_m: height, attenuation_db: loss, measured_e_field_v_m: measured });
    output.set(identity, point);
  }
  return [...output.values()];
}
export async function pointWorkbookBytes(project: PlannerProject) {
  const book = new ExcelJS.Workbook(), sheet = book.addWorksheet("Control points"); sheet.addRow(HEADERS);
  for (const point of project.control_points) for (const height of point.heights?.length ? point.heights : [{ height_m: point.height_m, attenuation_db: point.attenuation_db, measured: point.measured_e_field_v_m ?? 0, description: "" }])
    sheet.addRow([point.name, point.group, point.lat, point.lng, point.x_m, point.y_m, point.location, point.description, height.height_m, height.attenuation_db, height.measured, height.description, JSON.stringify(point)]);
  sheet.getColumn(13).hidden = true; sheet.getRow(1).font = { bold: true }; sheet.views = [{ state: "frozen", ySplit: 1 }];
  return new Uint8Array(await book.xlsx.writeBuffer());
}
export async function importPointWorkbook(project: PlannerProject, bytes: Uint8Array) {
  validateArchive(bytes); const book = new ExcelJS.Workbook(); await book.xlsx.load(new Uint8Array(bytes).buffer);
  const sheet = book.getWorksheet("Control points") ?? book.worksheets[0]; if (!sheet) throw new Error("Classeur sans feuille.");
  const rows: unknown[][] = []; sheet.eachRow(row => { rows.push(Array.from({ length: sheet.columnCount }, (_, i) => row.getCell(i + 1).value)); });
  return importPointRows(project, rows);
}
