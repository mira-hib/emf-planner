import {
  calculatePoints,
  contours,
  horizontalGrid,
  verticalGrid,
} from "./engine";
import type { PlannerProject } from "../types";
import { defaultVolumeOptions, volumeSurface } from "./volume";
self.onmessage = (
  event: MessageEvent<{
    id: number;
    kind: "points" | "horizontal" | "vertical" | "volume";
    project: PlannerProject;
    fine: boolean;
  }>,
) => {
  const { id, kind, project, fine } = event.data;
  try {
    const step = fine
      ? project.settings.fine_step_m
      : project.settings.grid_step_m;
    if (kind === "volume") {
      const result = volumeSurface(
        project,
        project.volume_options ?? defaultVolumeOptions(project),
        (progress) => self.postMessage({ id, progress }),
      );
      self.postMessage({ id, result }, { transfer: [result.positions.buffer] });
    } else if (kind === "points")
      self.postMessage({ id, result: calculatePoints(project) });
    else {
      const cuts =
        kind === "horizontal"
          ? project.horizontal_cuts.filter((c) => c.active)
          : project.vertical_cuts.filter((c) => c.active);
      const limitProjects = project.limits
        .map((l, i) =>
          l.active
            ? {
                index: i,
                project: {
                  ...project,
                  settings: { ...project.settings, selected_limit: i },
                },
              }
            : null,
        )
        .filter(
          (v): v is { index: number; project: PlannerProject } => v !== null,
        );
      if (!limitProjects.length)
        throw new Error("Activez au moins un jeu de limites.");
      const jobs = cuts.flatMap((cut) =>
        limitProjects.map((l) => ({ cut, ...l })),
      );
      const result = jobs.map(
        ({ cut, index: limitIndex, project: p }, index) => {
          self.postMessage({
            id,
            progress: Math.round((index / jobs.length) * 100),
          });
          const grid =
            kind === "horizontal"
              ? horizontalGrid(
                  p,
                  (cut as { height_m: number }).height_m,
                  step,
                  p.settings.horizontal_grid_divisions?.[fine ? 1 : 0],
                )
              : verticalGrid(
                  p,
                  cut as Parameters<typeof verticalGrid>[1],
                  step,
                );
          return {
            id: `${cut.id}:${limitIndex}`,
            cut_id: cut.id,
            limit_index: limitIndex,
            grid,
            contours: contours(grid, 1, p.limits[limitIndex].color),
          };
        },
      );
      self.postMessage({ id, result });
    }
  } catch (error) {
    self.postMessage({
      id,
      error: error instanceof Error ? error.message : String(error),
    });
  }
};
