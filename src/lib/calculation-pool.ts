import type { PlannerProject } from "../types";

export interface CalculationRequest {
  id: number;
  kind: "points" | "horizontal" | "vertical" | "volume";
  project: PlannerProject;
  fine: boolean;
  heightRange?: number[];
}

/** Partition whole cuts/points, never sums: worker counts preserve numeric order. */
export function calculationPartitions(request: CalculationRequest): CalculationRequest[] {
  const { project, kind } = request;
  const count = Math.max(1, Math.min(8, Math.floor(project.settings.worker_count ?? 1)));
  if (kind === "volume" || count === 1) return [request];
  // Projection couples the heights; splitting them would change the result.
  if (kind === "horizontal" && (project.limits.filter(l => l.active).length > 1
    || project.settings.project_lower_cuts || project.settings.merge_same_color)) return [request];
  const items = kind === "points" ? project.control_points : kind === "horizontal"
    ? project.horizontal_cuts.filter(c => c.active) : project.vertical_cuts.filter(c => c.active);
  const workers = Math.min(count, items.length);
  const result: CalculationRequest[] = [];
  for (let index = 0; index < workers; index++) {
    const slice = items.slice(Math.floor(items.length * index / workers), Math.floor(items.length * (index + 1) / workers));
    result.push({ ...request, project: { ...project,
      ...(kind === "points" ? { control_points: slice as PlannerProject["control_points"] }
        : kind === "horizontal" ? { horizontal_cuts: slice as PlannerProject["horizontal_cuts"] }
          : { vertical_cuts: slice as PlannerProject["vertical_cuts"] }) } });
  }
  return result.length ? result : [request];
}

export class CalculationPool {
  private workers: Worker[] = [];
  private stopped = false;
  onmessage: ((event: MessageEvent) => void) | null = null;
  onerror: ((event: ErrorEvent) => void) | null = null;
  terminate() { this.stopped = true; for (const worker of this.workers) worker.terminate(); this.workers = []; }
  postMessage(request: CalculationRequest) {
    const partitions = calculationPartitions(request), results: unknown[][] = [], progress = partitions.map(() => 0);
    let completed = 0;
    partitions.forEach((partition, index) => {
      const worker = new Worker(new URL("./calculation.worker.ts", import.meta.url), { type: "module" });
      this.workers.push(worker);
      worker.onmessage = event => {
        if (this.stopped) return;
        if (event.data.error) {
          this.onmessage?.(event);
          this.terminate();
          return;
        }
        if (event.data.progress !== undefined) {
          progress[index] = event.data.progress;
          this.onmessage?.(new MessageEvent("message", { data: { id: request.id,
            progress: Math.round(progress.reduce((sum, value) => sum + value, 0) / partitions.length) } }));
          return;
        }
        if (request.kind === "volume") this.onmessage?.(event);
        else {
          results[index] = event.data.result;
          progress[index] = 100;
          completed++;
          if (completed === partitions.length) this.onmessage?.(new MessageEvent("message", {
            data: { id: request.id, result: results.flat() } }));
        }
        worker.terminate();
      };
      worker.onerror = event => { if (!this.stopped) { this.onerror?.(event); this.terminate(); } };
      worker.postMessage(partition);
    });
  }
}
