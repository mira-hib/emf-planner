import type { RadiatingSource } from "../types";
import { newSource } from "./project";
import { sourceFrequency } from "./source-fields";
import { decimalInput } from "./numeric-input";

/** Order of the kit/native 12-column source clipboard sheet (not display order). */
export const SOURCE_CLIPBOARD_COLUMNS: (keyof RadiatingSource)[] = ["name", "sector_id", "service_type", "frequency_mhz", "power_w", "num_tx", "total_attenuation_db", "antenna_model", "azimuth_deg", "gain_dbi", "height_m", "antenna_size_m"];
const bounds: Partial<Record<keyof RadiatingSource, [number, number, boolean?, boolean?]>> = {
  power_w: [0, 100000], num_tx: [1, 100, false, true], total_attenuation_db: [0, 100], gain_dbi: [-100, 100, true, true], height_m: [0, 1000, false, true], antenna_size_m: [0, 30, true, true], mechanical_downtilt_deg: [-90, 90], electrical_downtilt_deg: [-90, 90], beam_width_h_deg: [.1, 360], beam_width_v_deg: [.1, 360],
};
export function sourceCellPatch(key: keyof RadiatingSource, raw: unknown): Partial<RadiatingSource> {
  if (key === "frequency_mhz") return sourceFrequency(raw);
  if (key === "azimuth_deg") {
    const text = String(raw).trim();
    if (!text) return { azimuth_deg: 0 };
    try { const n = decimalInput(text); return { azimuth_deg: n >= 0 && n < 360 ? n : -1 }; }
    catch { return { azimuth_deg: -1 }; }
  }
  const range = bounds[key];
  if (range) {
    const text = String(raw).trim(), n = decimalInput(text);
    if (!text || !Number.isFinite(n) || n < range[0] || n > range[1] || (range[2] && n === range[0]) || (range[3] && n === range[1])) throw new Error(`${String(key)} : valeur hors limites (${range[0]} à ${range[1]}).`);
    return { [key]: key === "num_tx" ? Math.floor(n) : n };
  }
  if (key === "active" || key === "is_existing") return { [key]: /^(true|1|yes|oui|existing)$/i.test(String(raw)) };
  if (key === "id") return {};
  return { [key]: String(raw) };
}
/** Transactional paste: validate all cells before exposing any changed rows. */
export function pasteSourceCells(sources: RadiatingSource[], origin: [number, number], text: string, startId: string | undefined, startKey: keyof RadiatingSource, displayColumns = SOURCE_CLIPBOARD_COLUMNS): RadiatingSource[] {
  const matrix = text.replace(/^\uFEFF/, "").replace(/\r/g, "").replace(/\n+$/, "").split("\n").map((line) => line.split("\t"));
  const headered = /radiation source name|nom de la source/i.test(matrix[0]?.[0] ?? "");
  if (headered) matrix.shift();
  if (!matrix.length || matrix.length > 10000) throw new Error("Collez entre 1 et 10 000 lignes de sources.");
  const keys = headered ? SOURCE_CLIPBOARD_COLUMNS : displayColumns.slice(Math.max(0, displayColumns.indexOf(startKey)));
  const start = Math.max(0, sources.findIndex((source) => source.id === startId));
  const next = sources.map((source) => ({ ...source }));
  matrix.forEach((cells, ri) => {
    if (cells.length > keys.length) throw new Error(`Ligne ${ri + 1} : trop de colonnes.`);
    const index = start + ri;
    next[index] ??= newSource(origin);
    cells.forEach((value, ci) => { next[index] = { ...next[index], ...sourceCellPatch(keys[ci], value) }; });
  });
  return next;
}
