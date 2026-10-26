import {
  calculatePoints,
  calculatePointHeightRange,
  contours,
  horizontalGrid,
  verticalGrid,
} from "./engine";
import type { PlannerProject } from "../types";
import { defaultVolumeOptions, volumeSurface } from "./volume";
import { horizontalProjections } from "./cut-projection";
self.onmessage = (
  event: MessageEvent<{
    id: number;
    kind: "points" | "horizontal" | "vertical" | "volume";
    project: PlannerProject;
    fine: boolean;
    heightRange?: number[];
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
      self.postMessage({
        id,
        result: event.data.heightRange
          ? calculatePointHeightRange(project, event.data.heightRange)
          : calculatePoints(project),
      });
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
                  (p.settings.vertical_grid_divisions ?? p.settings.horizontal_grid_divisions)?.[fine ? 1 : 0],
                );
          return {
            id: `${cut.id}:${limitIndex}`,
            cut_id: cut.id,
            limit_index: limitIndex,
            grid,
            contours: { ...contours(grid, 1, kind === "horizontal" && limitProjects.length === 1 ? (cut as { color: string }).color : p.limits[limitIndex].color),
              line_style: kind === "horizontal" ? (cut as { line_style?: string }).line_style : undefined,
              frozen_style: true },
            // The last rendered image keeps its appearance until an explicit calculation.
          };
        },
      );
      const output = kind === "horizontal" ? horizontalProjections(project, result) : result;
      const transfer: Transferable[] = [];
      for (const item of output) for (const data of [item.grid.values, item.grid.fields])
        if (data instanceof Float64Array) transfer.push(data.buffer);
      self.postMessage({ id, result: output }, { transfer });
    }
  } catch (error) {
    self.postMessage({
      id,
      error: error instanceof Error ? error.message : String(error),
    });
  }
};
