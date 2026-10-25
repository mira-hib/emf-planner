import type { Contour, FieldGrid, PlannerProject } from "../types";
import { contours } from "./engine";
export interface CalculatedCut { id: string; cut_id?: string; limit_index?: number; grid: FieldGrid; contours: Contour }
export function mergeGridMaximum(target: FieldGrid, next: FieldGrid) {
  if (target.width !== next.width || target.height !== next.height || target.x0 !== next.x0 || target.y0 !== next.y0 || target.dx !== next.dx || target.dy !== next.dy)
    throw new Error("Les grilles projetées doivent avoir la même géométrie.");
  for (let i = 0; i < target.values.length; i++) {
    target.values[i] = Math.max(target.values[i], next.values[i]);
    target.fields[i] = Math.max(target.fields[i], next.fields[i]);
  }
}
export function horizontalProjections(project: PlannerProject, results: CalculatedCut[]): CalculatedCut[] {
  const multipleLimits = project.limits.filter(limit => limit.active).length > 1;
  const cuts = project.horizontal_cuts.filter(cut => cut.active);
  const output: CalculatedCut[] = [];
  for (const limitIndex of [...new Set(results.map(r => r.limit_index))]) {
    const ordered = results.filter(r => r.limit_index === limitIndex).sort((a, b) => a.grid.height_m - b.grid.height_m);
    if (multipleLimits && cuts.length > 1) {
      const first = ordered[0]; if (!first) continue;
      for (const next of ordered.slice(1)) mergeGridMaximum(first.grid, next.grid);
      first.contours = { ...first.contours, ...contours(first.grid, 1, first.contours.color) };
      output.push(first); continue;
    }
    let lower: CalculatedCut | undefined;
    for (const result of ordered) {
      const cut = project.horizontal_cuts.find(c => c.id === result.cut_id);
      if (lower && project.settings.project_lower_cuts) mergeGridMaximum(result.grid, lower.grid);
      const sameColor = lower && cut?.color === project.horizontal_cuts.find(c => c.id === lower!.cut_id)?.color
        && cut?.line_style === project.horizontal_cuts.find(c => c.id === lower!.cut_id)?.line_style;
      if (lower && sameColor && project.settings.merge_same_color) {
        mergeGridMaximum(result.grid, lower.grid);
        output.pop();
      }
      result.contours = { ...result.contours, ...contours(result.grid, 1, result.contours.color) };
      output.push(result); lower = result;
    }
  }
  return output;
}
