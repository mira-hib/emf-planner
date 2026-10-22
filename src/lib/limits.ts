import type { LimitRow, LimitSet } from "../types";

// UC_Limits uses -999 for an empty cell. Keep it through JSON and .xemf imports.
export const EMPTY_LIMIT = -999;
export const NATIVE_FALLBACK_LIMIT = Math.sqrt(3.77e6);
export type LimitField = keyof LimitRow;

export function parseLimitCell(text: string, field: LimitField): number {
  const value = Number(text.trim().replace(",", "."));
  if (!text.trim() || !Number.isFinite(value)) return EMPTY_LIMIT;
  const valid =
    field === "exponent"
      ? value >= -3 && value <= 3
      : value > 0 && value <= (field === "frequency" ? 1e6 : 1e5);
  return valid ? Math.fround(value) : EMPTY_LIMIT;
}

export function completeLimit(limit: LimitSet): boolean {
  return (
    limit.rows.length > 0 &&
    limit.rows.every((row) =>
      Object.values(row).every(
        (value) => Number.isFinite(value) && value !== EMPTY_LIMIT,
      ),
    )
  );
}

// Preserve the native row order, exclusive lower bound and inclusive upper bound.
// CalcTmp leaves PDU at 1e6 for empty/incomplete tables or uncovered frequencies.
export function nativeLimitAt(limit: LimitSet, frequency: number): number {
  if (!completeLimit(limit)) return NATIVE_FALLBACK_LIMIT;
  let lower = limit.rows[0].frequency > 10 ? 10 : limit.rows[0].frequency;
  const start = limit.rows[0].frequency > 10 ? 0 : 1;
  for (let i = start; i < limit.rows.length; i++) {
    const row = limit.rows[i];
    if (frequency > lower && frequency <= row.frequency)
      return row.value * frequency ** row.exponent;
    lower = row.frequency;
  }
  return NATIVE_FALLBACK_LIMIT;
}

// Existing clone files may contain independent frequency lists. Refine their
// intervals onto a common grid only when the user edits the shared table.
export function commonLimitRows(limits: LimitSet[]): LimitRow[][] {
  const first = limits[0]?.rows ?? [];
  if (
    limits.every(
      (limit) =>
        limit.rows.length === first.length &&
        limit.rows.every((row, i) => row.frequency === first[i].frequency),
    )
  )
    return limits.map((limit) => limit.rows.map((row) => ({ ...row })));
  const frequencies = [
    ...new Set(
      limits.flatMap((limit) => limit.rows.map((row) => row.frequency)),
    ),
  ].sort((a, b) => a - b);
  return limits.map((limit) =>
    frequencies.map((frequency) => {
      const row = limit.rows.find((row) => row.frequency >= frequency);
      return {
        frequency,
        value: row?.value ?? EMPTY_LIMIT,
        exponent: row?.exponent ?? EMPTY_LIMIT,
      };
    }),
  );
}

export function editLimitCell(
  limits: LimitSet[],
  index: number,
  limitIndex: number,
  field: LimitField,
  value: number,
): LimitSet[] {
  const rows = commonLimitRows(limits);
  return limits.map((limit, li) => ({
    ...limit,
    rows: rows[li].map((row, ri) =>
      ri === index && (field === "frequency" || li === limitIndex)
        ? { ...row, [field]: value }
        : row,
    ),
  }));
}

export function addLimitRow(limits: LimitSet[]): LimitSet[] {
  const rows = commonLimitRows(limits);
  return limits.map((limit, i) => ({
    ...limit,
    rows: [
      ...rows[i],
      { frequency: EMPTY_LIMIT, value: EMPTY_LIMIT, exponent: EMPTY_LIMIT },
    ],
  }));
}

export function removeLimitRows(
  limits: LimitSet[],
  indices: number[],
): LimitSet[] {
  const rows = commonLimitRows(limits);
  return limits.map((limit, i) => ({
    ...limit,
    rows: rows[i].filter((_, index) => !indices.includes(index)),
  }));
}

export function limitSegments(
  limit: LimitSet,
): [number, number, number, number][] {
  if (!completeLimit(limit)) return [];
  let lower = limit.rows[0].frequency > 10 ? 10 : limit.rows[0].frequency;
  const start = limit.rows[0].frequency > 10 ? 0 : 1;
  const segments: [number, number, number, number][] = [];
  for (let i = start; i < limit.rows.length; i++) {
    const row = limit.rows[i];
    const from = Math.max(10, lower),
      to = Math.min(100000, row.frequency);
    if (from > 0 && to > from && row.value > 0)
      segments.push([
        from,
        row.value * from ** row.exponent,
        to,
        row.value * to ** row.exponent,
      ]);
    lower = row.frequency;
  }
  return segments;
}
