import type { PlannerProject, VerticalCut } from "../types";
import { uid } from "./project";
import { wrap } from "./geometry";

/** UC_VertPlanes: one azimuth per active direction, with grouped source names. */
export function sourceVerticalCuts(project: PlannerProject, followMaximum = false): VerticalCut[] {
  const groups = new Map<string, { direction: number; x: number; y: number; names: string[] }>();
  for (const source of project.sources.filter(source => source.active !== false)) {
    let direction = source.azimuth_deg < 0 ? 0 : source.azimuth_deg;
    if (followMaximum && source.pattern_h?.length === 360) {
      const minimum = Math.min(...source.pattern_h);
      direction += source.pattern_h.indexOf(minimum);
    }
    direction = wrap(direction);
    const x = source.x_m ?? 0, y = source.y_m ?? 0, key = `${direction}:${x}:${y}`;
    const name = `${project.settings.cut_use_antenna_names ? source.antenna_model : source.name}${project.settings.cut_add_owner && source.owner ? ` ${source.owner}` : ""}`;
    groups.set(key, { direction, x, y, names: [...groups.get(key)?.names ?? [], name] });
  }
  return [...groups.values()].sort((a, b) => a.direction - b.direction).map(({ direction: azimuth_deg, x, y, names }) => {
    const name = names.join(", "), existing = project.vertical_cuts.find(c => c.name === name && c.x === x && c.y === y && c.azimuth_deg === azimuth_deg);
    return { id: uid(), name, active: true, azimuth_deg, x, y, backward_m: 0, forward_m: 150, max_height_m: 100, min_height_m: 0, ...existing };
  });
}

export function nativeTruncate(value: number, digits: number): string {
  if (!Number.isFinite(value)) return "—";
  const factor = 10 ** digits;
  return (Math.trunc(value * factor) / factor).toFixed(digits);
}
