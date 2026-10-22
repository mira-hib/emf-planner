import type { Contour, PlannerProject } from "../types";
export function dashPattern(style?: string): string | undefined {
  return (
    { dash: "7 4", dot: "1 3", dashdot: "7 3 1 3" } as Record<string, string>
  )[style ?? ""];
}
export function styledContours(
  project: PlannerProject,
  results: { cut_id?: string; limit_index?: number; contours: Contour }[],
): Contour[] {
  const single = project.limits.filter((l) => l.active).length === 1;
  return results
    .filter(
      (r) =>
        (r.limit_index === undefined ||
          project.limits[r.limit_index]?.active !== false) &&
        project.horizontal_cuts.find((c) => c.id === r.cut_id)?.active !==
          false,
    )
    .map((r) => {
      const cut = project.horizontal_cuts.find((c) => c.id === r.cut_id);
      return {
        ...r.contours,
        line_style: cut?.line_style,
        color: single && cut ? cut.color : r.contours.color,
      };
    });
}
