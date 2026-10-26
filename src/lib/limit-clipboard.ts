import type { LimitSet } from "../types";
import { commonLimitRows, EMPTY_LIMIT, parseLimitCell } from "./limits";

export function pasteLimitCells(limits: LimitSet[], text: string, startRow = 0, startColumn = 0): LimitSet[] {
  const matrix = text.replace(/^\uFEFF/, "").replace(/\r/g, "").replace(/\n+$/, "").split("\n").map((line) => line.split("\t"));
  if (!matrix.length || matrix.length + startRow > 10000) throw new Error("Le tableau accepte au maximum 10 000 intervalles.");
  const rows = commonLimitRows(limits);
  rows.forEach((plane) => { while (plane.length < startRow + matrix.length) plane.push({ frequency: EMPTY_LIMIT, value: EMPTY_LIMIT, exponent: EMPTY_LIMIT }); });
  matrix.forEach((cells, ri) => cells.forEach((text, ci) => {
    const column = startColumn + ci;
    if (column > limits.length * 2) throw new Error(`Ligne ${ri + 1} : trop de colonnes.`);
    const field = column === 0 ? "frequency" : column % 2 ? "value" : "exponent";
    const value = parseLimitCell(text, field);
    if (value === EMPTY_LIMIT && text.trim()) throw new Error(`Ligne ${ri + 1}, colonne ${column + 1} : valeur invalide.`);
    rows.forEach((row, li) => { if (!column || li === Math.floor((column - 1) / 2)) row[startRow + ri][field] = value; });
  }));
  return limits.map((limit, li) => ({ ...limit, rows: rows[li] }));
}
export function limitFrequencyWarnings(limits: LimitSet[]): string[] {
  const frequencies = limits[0]?.rows.map((r) => r.frequency) ?? [];
  return frequencies.flatMap((frequency, i) => i && frequency !== EMPTY_LIMIT && frequencies[i - 1] !== EMPTY_LIMIT && frequency <= frequencies[i - 1] ? [`Intervalle ${i + 1} : fréquence non croissante ; l’ordre natif est conservé.`] : []);
}
