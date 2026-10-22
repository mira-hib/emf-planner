import type { PlannerProject } from "../types";
import { toGeo, toLocal } from "./geometry";
import {
  mercatorGeo,
  mercatorPoint,
  nativeLocal,
  nativeScale,
} from "./native-coordinates";

export type Area = NonNullable<
  PlannerProject["settings"]["calculation_area_m"]
>;
export type AreaExtents = {
  left: number;
  right: number;
  top: number;
  bottom: number;
};
type Pair = [number, number];
const roundEven = (x: number) => {
  const n = Math.floor(x),
    f = x - n;
  return f === 0.5 ? n + (n % 2) : Math.round(x);
};
export function calculationArea(project: PlannerProject): Area {
  const r = project.settings.radius_m;
  return (
    project.settings.calculation_area_m ?? {
      x_min: -r,
      x_max: r,
      y_min: -r,
      y_max: r,
    }
  );
}
export function areaWorldCorners(project: PlannerProject): [Pair, Pair] {
  if (project.settings.calculation_area_wm)
    return project.settings.calculation_area_wm;
  const area = calculationArea(project);
  return [
    mercatorPoint(toGeo(area.x_min, area.y_min, project.origin)),
    mercatorPoint(toGeo(area.x_max, area.y_max, project.origin)),
  ];
}
export function localArea(corners: [Pair, Pair], origin: Pair): Area {
  const [a, b] = corners.map((point) =>
    nativeLocal(...mercatorGeo(point), origin),
  );
  return {
    x_min: Math.min(a[0], b[0]),
    x_max: Math.max(a[0], b[0]),
    y_min: Math.min(a[1], b[1]),
    y_max: Math.max(a[1], b[1]),
  };
}
export function areaFromExtents(
  origin: Pair,
  extents: AreaExtents,
): { calculation_area_m: Area; calculation_area_wm: [Pair, Pair] } {
  for (const key of ["left", "right", "top", "bottom"] as const) {
    const min = key === "right" || key === "top" ? 10 : 0;
    if (
      !Number.isFinite(extents[key]) ||
      extents[key] < min ||
      extents[key] > 5000
    )
      throw new Error(
        `Dimension ${key} : valeur attendue entre ${min} et 5000 m.`,
      );
  }
  const [x, y] = mercatorPoint(origin),
    scale = nativeScale(origin);
  const corners: [Pair, Pair] = [
    [x - extents.left / scale - 0.1, y - extents.bottom / scale - 0.1],
    [x + extents.right / scale + 0.1, y + extents.top / scale + 0.1],
  ];
  return {
    calculation_area_wm: corners,
    calculation_area_m: localArea(corners, origin),
  };
}
export function areaFromBounds(origin: Pair, area: Area) {
  if (
    !Object.values(area).every(Number.isFinite) ||
    area.x_max <= area.x_min ||
    area.y_max <= area.y_min
  )
    throw new Error(
      "Les bords de la zone de calcul ne peuvent pas se croiser.",
    );
  const corners: [Pair, Pair] = [
    mercatorPoint(toGeo(area.x_min, area.y_min, origin)),
    mercatorPoint(toGeo(area.x_max, area.y_max, origin)),
  ];
  return { calculation_area_m: area, calculation_area_wm: corners };
}
export function areaExtents(project: PlannerProject): AreaExtents {
  const area = calculationArea(project);
  return {
    left: roundEven(Math.abs(area.x_min)),
    right: roundEven(Math.abs(area.x_max)),
    top: roundEven(Math.abs(area.y_max)),
    bottom: roundEven(Math.abs(area.y_min)),
  };
}
export function initialNativeArea(origin: Pair) {
  const [x, y] = mercatorPoint(origin);
  const corners: [Pair, Pair] = [
    [x - 100, y - 100],
    [x + 100, y + 100],
  ];
  return {
    calculation_area_wm: corners,
    calculation_area_m: localArea(corners, origin),
  };
}

// Geographic anchors stay fixed while the local origin changes. Map geometry
// uses the clone's scaled Mercator frame; TX/CP/VP use native 0.1 m rounding.
export function rebaseProject(
  project: PlannerProject,
  origin: Pair,
): PlannerProject {
  if (
    !origin.every(Number.isFinite) ||
    Math.abs(origin[0]) > 85 ||
    Math.abs(origin[1]) > 180
  )
    throw new Error("Origine géographique invalide.");
  const convert = ([x, y]: Pair): Pair =>
    toLocal(...toGeo(x, y, project.origin), origin);
  const corners = areaWorldCorners(project);
  const area = localArea(corners, origin);
  return {
    ...project,
    origin,
    settings: {
      ...project.settings,
      site_coordinates: project.settings.site_coordinates ?? project.origin,
      calculation_area_wm: corners,
      calculation_area_m: area,
    },
    sources: project.sources.map((source) => {
      const [x_m, y_m] = nativeLocal(source.lat, source.lng, origin, true);
      return { ...source, x_m, y_m };
    }),
    control_points: project.control_points.map((point) => {
      const [x_m, y_m] = nativeLocal(point.lat, point.lng, origin, true);
      return { ...point, x_m, y_m };
    }),
    buildings: project.buildings.map((b) => ({
      ...b,
      points: b.points.map(convert),
    })),
    lines: project.lines.map((l) => ({ ...l, points: l.points.map(convert) })),
    vertical_cuts: project.vertical_cuts.map((cut) => {
      const geo_position =
        cut.geo_position ?? toGeo(cut.x, cut.y, project.origin);
      const [x, y] = nativeLocal(...geo_position, origin, true);
      return { ...cut, x, y, geo_position };
    }),
    terrain_points: project.terrain_points.map((p) => {
      const [x, y] = convert([p.x, p.y]);
      return { ...p, x, y };
    }),
    terrain_contours: project.terrain_contours?.map((c) => ({
      ...c,
      points: c.points.map(convert),
    })),
    volume_options: project.volume_options
      ? {
          ...project.volume_options,
          center_x: convert([
            project.volume_options.center_x,
            project.volume_options.center_y,
          ])[0],
          center_y: convert([
            project.volume_options.center_x,
            project.volume_options.center_y,
          ])[1],
        }
      : undefined,
    saved_cut_results: undefined,
  };
}

export function horizontalDimensions(
  project: PlannerProject,
  divisions: number,
) {
  if (!Number.isInteger(divisions) || divisions < 50 || divisions > 1000)
    throw new Error(
      "La résolution doit être un entier entre 50 et 1000 divisions.",
    );
  const a = calculationArea(project),
    w = a.x_max - a.x_min,
    h = a.y_max - a.y_min;
  if (!(w > 0 && h > 0) || !Number.isFinite(w + h))
    throw new Error("Zone de calcul invalide.");
  const nx = w >= h ? divisions : roundEven((divisions * w) / h);
  const ny = h >= w ? divisions : roundEven((divisions * h) / w);
  if (nx < 1 || ny < 1)
    throw new Error(
      "La zone est trop étroite pour cette résolution de grille.",
    );
  return { width: nx + 1, height: ny + 1, dx: w / nx, dy: h / ny };
}
