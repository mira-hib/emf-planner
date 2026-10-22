import type { FieldGrid } from "../types";

/** Saved contour paths do not imply a sampled field grid or a zero field. */
export function cutNumericSummary(
  grids: Pick<FieldGrid, "fields" | "values">[],
) {
  let maxField: number | null = null,
    maxRatio: number | null = null,
    samples = 0;
  for (const grid of grids) {
    if (!grid.fields.length) continue;
    for (const field of grid.fields)
      if (Number.isFinite(field)) {
        maxField = maxField === null ? field : Math.max(maxField, field);
        samples++;
      }
    for (const ratio of grid.values)
      if (Number.isFinite(ratio))
        maxRatio = maxRatio === null ? ratio : Math.max(maxRatio, ratio);
  }
  return { maxField, maxRatio, samples: samples || null };
}
