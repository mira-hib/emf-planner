import type {
  Contour,
  ExposureResult,
  FieldGrid,
  LimitSet,
  PlannerProject,
  PointResult,
  RadiatingSource,
  VerticalCut,
} from "../types";
import {
  deg,
  inside,
  rad,
  segmentCrosses,
  terrainHeight,
  toLocal,
  wrap,
} from "./geometry";
import { nativeLimitAt } from "./limits";
import { horizontalDimensions } from "./origin-grid";

interface PointContext {
  excludedBuilding?: string;
  inBuilding: boolean;
}

export function limitAt(limit: LimitSet, frequency: number): number {
  return nativeLimitAt(limit, frequency);
}
export function eirp(source: RadiatingSource): number {
  return (
    source.power_w *
    source.num_tx *
    10 ** ((source.gain_dbi - source.total_attenuation_db) / 10)
  );
}
export function patternAmplitude(
  values: number[] | undefined,
  angle: number,
  beamWidth: number,
): number {
  const a = wrap(angle);
  if (!values || values.length !== 360) {
    const signed = Math.min(a, 360 - a);
    return (
      10 ** (-Math.min(30, 12 * (signed / Math.max(0.1, beamWidth)) ** 2) / 20)
    );
  }
  const low = Math.floor(a),
    fraction = a - low;
  // Original CalcPPE interpolates amplitudes, rather than attenuation in dB.
  return (
    10 ** (-values[low] / 20) * (1 - fraction) +
    10 ** (-values[(low + 1) % 360] / 20) * fraction
  );
}
export function sourceField(
  project: PlannerProject,
  source: RadiatingSource,
  x: number,
  y: number,
  elevation: number,
  limitIndex = project.settings.selected_limit,
  lossDb = 0,
  context?: PointContext,
): { field: number; distance: number } {
  if (source.active === false || source.power_w <= 0)
    return { field: 0, distance: 0 };
  const position = toLocal(source.lat, source.lng, project.origin),
    sx = source.x_m ?? position[0],
    sy = source.y_m ?? position[1],
    dx = x - sx,
    dy = y - sy;
  const roof = source.location === "Roof" || source.location === "Facade";
  const building = roof
    ? project.buildings.find((b) => inside(sx, sy, b.points))
    : undefined;
  const sourceZ =
    source.height_m +
    (building
      ? building.foundation_m
      : terrainHeight(project, position[0], position[1]));
  const dz = elevation - sourceZ;
  const distance = Math.max(0.05, Math.hypot(dx, dy, dz));
  const az = rad(source.azimuth_deg < 0 ? 0 : source.azimuth_deg),
    localX = dx * Math.cos(az) - dy * Math.sin(az),
    forward = dx * Math.sin(az) + dy * Math.cos(az);
  // Rotation and cyclic 360-sample H/V interpolation follow CalcSZZ.CalcPPE.
  const tilt = rad(-source.mechanical_downtilt_deg),
    tiltedY = forward * Math.cos(tilt) - dz * Math.sin(tilt),
    tiltedZ = forward * Math.sin(tilt) + dz * Math.cos(tilt);
  const horizontal = wrap(deg(Math.atan2(localX, tiltedY)));
  const vertical = wrap(-deg(Math.atan2(tiltedZ, Math.hypot(localX, tiltedY))));
  const h = patternAmplitude(source.pattern_h, horizontal, 65);
  const v = patternAmplitude(source.pattern_v, vertical, 10);
  const limit = project.limits[limitIndex] ?? project.limits[0];
  const factors = source.limit_factors?.[limitIndex] ?? [
    source.alpha24,
    source.duty_cycle,
    source.power_reduction,
  ];
  const factor =
    (limit.use_alpha24 ? factors[0] : 1) *
    (limit.use_ftdc ? factors[1] : 1) *
    (limit.use_fpr ? factors[2] : 1);
  let extraLoss = lossDb;
  if (project.settings.buildings_mode !== "ignore")
    for (const b of project.buildings.filter((b) => b.active)) {
      const crosses = b.points
        .map((p, i) =>
          segmentCrosses(
            [sx, sy],
            [x, y],
            p,
            b.points[(i + 1) % b.points.length],
          ),
        )
        .filter((t): t is number => t !== null);
      const blocked = crosses.some(
        (t) =>
          sourceZ + dz * t < b.foundation_m + b.height_m &&
          sourceZ + dz * t > b.foundation_m,
      );
      const inBuilding =
        inside(x, y, b.points) && elevation < b.foundation_m + b.height_m;
      if (
        project.settings.buildings_mode === "screen" &&
        blocked &&
        b.id !== context?.excludedBuilding
      )
        return { field: 0, distance };
      if (
        project.settings.buildings_mode === "attenuate" &&
        inBuilding &&
        (context === undefined || context.inBuilding)
      ) {
        // Native penetration loss is configured per frequency band, once per ray.
        extraLoss +=
          project.settings.building_attenuation_high_db !== undefined
            ? source.frequency_mhz <=
              (project.settings.building_frequency_boundary_mhz ?? 400)
              ? project.settings.building_attenuation_db
              : project.settings.building_attenuation_high_db
            : b.attenuation_db;
        break;
      }
    }
  const field =
    ((project.settings.reflection *
      Math.sqrt(Math.max(0, 30 * eirp(source) * factor)) *
      h *
      v) /
      distance) *
    10 ** (-extraLoss / 20);
  return { field, distance };
}
export function exposureAt(
  project: PlannerProject,
  x: number,
  y: number,
  elevation: number,
  limitIndex = project.settings.selected_limit,
  lossDb = 0,
  context?: PointContext,
): ExposureResult {
  let e2 = 0,
    ratio = 0;
  const per_source = project.sources
    .filter((s) => s.active !== false)
    .map((source) => {
      const { field, distance } = sourceField(
          project,
          source,
          x,
          y,
          elevation,
          limitIndex,
          lossDb,
          context,
        ),
        limit = limitAt(
          project.limits[limitIndex] ?? project.limits[0],
          source.frequency_mhz,
        ),
        r = (field / limit) ** 2;
      e2 += field * field;
      ratio += r;
      return {
        source_id: source.id,
        field_v_m: field,
        power_density_w_m2: (field * field) / 377,
        icnirp_limit: limit,
        ratio_contribution: r,
        distance_m: distance,
      };
    });
  return {
    total_field: Math.sqrt(e2),
    total_power_density: e2 / 377,
    exposure_ratio: ratio,
    compliant: ratio <= 1,
    per_source,
  };
}
export function calculatePoints(project: PlannerProject): PointResult[] {
  return project.control_points.flatMap((point) => {
    if (point.group_active === false) return [];
    const x = point.x_m,
      y = point.y_m;
    const b = project.buildings.find((b) => inside(x, y, b.points));
    const terrainPosition = toLocal(point.lat, point.lng, project.origin);
    const base =
      b && point.location === "Building"
        ? b.foundation_m
        : point.location === "Roof" && b
          ? b.foundation_m + b.height_m
          : terrainHeight(project, terrainPosition[0], terrainPosition[1]);
    return (
      point.heights?.length
        ? point.heights
        : [
            {
              height_m: point.height_m,
              attenuation_db: point.attenuation_db,
              measured: point.measured_e_field_v_m ?? 0,
            },
          ]
    ).map((height, i) => {
      const calculated = exposureAt(
        project,
        x,
        y,
        base + height.height_m,
        project.settings.selected_limit,
        height.attenuation_db,
        {
          excludedBuilding: point.location === "Building" ? b?.id : undefined,
          inBuilding:
            point.location === "Building" &&
            !!b &&
            b.active &&
            height.height_m < b.height_m,
        },
      );
      const measured = height.measured;
      const measurementLimit = limitAt(
        project.limits[project.settings.selected_limit] ?? project.limits[0],
        project.settings.point_measurement_frequency_mhz ?? 1000,
      );
      const ratio =
        calculated.exposure_ratio + (measured / measurementLimit) ** 2;
      return {
        ...calculated,
        point_id: point.id,
        name: `${point.name}${(point.heights?.length ?? 1) > 1 ? `.${i + 1}` : ""}`,
        height_m: height.height_m,
        x,
        y,
        measured,
        elevation: base,
        calculated_field: calculated.total_field,
        total_field: Math.hypot(calculated.total_field, measured),
        total_power_density:
          calculated.total_power_density + measured ** 2 / 377,
        exposure_ratio: ratio,
        compliant: ratio <= 1,
      };
    });
  });
}

/** frmCalcOneControlPoint: range calculations ignore manual point losses and measurements. */
export function calculatePointHeightRange(
  project: PlannerProject,
  heights: number[],
): PointResult[] {
  if (
    !heights.length ||
    heights.length > 1000 ||
    heights.some((h) => !Number.isFinite(h) || h < 0 || h > 10000)
  )
    throw new Error(
      "Plage de hauteurs invalide (1 à 1000 valeurs entre 0 et 10 000 m).",
    );
  return calculatePoints({
    ...project,
    control_points: project.control_points.map((c) => ({
      ...c,
      heights: heights.map((height_m, i) => ({
        id: `range-${i}`,
        height_m,
        attenuation_db: 0,
        measured: 0,
        description: "",
      })),
    })),
  });
}
export function horizontalGrid(
  project: PlannerProject,
  height: number,
  step: number,
  divisions?: number,
): FieldGrid {
  const radius = project.settings.radius_m;
  const area = project.settings.calculation_area_m ?? {
    x_min: -radius,
    x_max: radius,
    y_min: -radius,
    y_max: radius,
  };
  const dimensions =
    divisions === undefined
      ? undefined
      : horizontalDimensions(project, divisions);
  const countX =
    dimensions?.width ??
    Math.max(2, Math.ceil((area.x_max - area.x_min) / Math.max(0.2, step)) + 1);
  const countY =
    dimensions?.height ??
    Math.max(2, Math.ceil((area.y_max - area.y_min) / Math.max(0.2, step)) + 1);
  if (
    countX > (divisions === undefined ? 501 : 1001) ||
    countY > (divisions === undefined ? 501 : 1001)
  )
    throw new Error(
      "Grille trop grande. Augmentez le pas ou réduisez la zone (501 × 501 maximum).",
    );
  const dx = (area.x_max - area.x_min) / (countX - 1),
    dy = (area.y_max - area.y_min) / (countY - 1),
    values: number[] = [],
    fields: number[] = [];
  const originElevation = terrainHeight(project, 0, 0);
  for (let j = 0; j < countY; j++)
    for (let i = 0; i < countX; i++) {
      const x = area.x_min + i * dx,
        y = area.y_min + j * dy,
        r = exposureAt(project, x, y, height + originElevation);
      values.push(r.exposure_ratio);
      fields.push(r.total_field);
    }
  return {
    width: countX,
    height: countY,
    x0: area.x_min,
    y0: area.y_min,
    dx,
    dy,
    values,
    fields,
    height_m: height,
  };
}
export function verticalGrid(
  project: PlannerProject,
  cut: VerticalCut,
  step: number,
): FieldGrid {
  const width =
      Math.ceil((cut.forward_m + cut.backward_m) / Math.max(step, 0.5)) + 1,
    height = Math.ceil(cut.max_height_m / Math.max(step, 0.5)) + 1;
  if (width > 501 || height > 301)
    throw new Error("Coupe trop grande. Augmentez le pas de calcul.");
  const dx = (cut.forward_m + cut.backward_m) / (width - 1),
    dy = cut.max_height_m / (height - 1),
    values: number[] = [],
    fields: number[] = [];
  const originElevation = terrainHeight(project, 0, 0);
  for (let j = 0; j < height; j++)
    for (let i = 0; i < width; i++) {
      const d = -cut.backward_m + i * dx,
        x = cut.x + d * Math.sin(rad(cut.azimuth_deg)),
        y = cut.y + d * Math.cos(rad(cut.azimuth_deg));
      const r = exposureAt(project, x, y, j * dy + originElevation);
      values.push(r.exposure_ratio);
      fields.push(r.total_field);
    }
  return {
    width,
    height,
    x0: -cut.backward_m,
    y0: 0,
    dx,
    dy,
    values,
    fields,
    height_m: 0,
  };
}
export function contours(
  grid: FieldGrid,
  level: number,
  color: string,
): Contour {
  const segments: [number, number][][] = [];
  for (let j = 0; j < grid.height - 1; j++)
    for (let i = 0; i < grid.width - 1; i++) {
      const x = grid.x0 + i * grid.dx,
        y = grid.y0 + j * grid.dy;
      const points: [number, number][] = [
        [x, y],
        [x + grid.dx, y],
        [x + grid.dx, y + grid.dy],
        [x, y + grid.dy],
      ];
      const v = [
        grid.values[j * grid.width + i],
        grid.values[j * grid.width + i + 1],
        grid.values[(j + 1) * grid.width + i + 1],
        grid.values[(j + 1) * grid.width + i],
      ];
      const intersections: [number, number][] = [];
      for (let k = 0; k < 4; k++) {
        const n = (k + 1) % 4;
        if (v[k] >= level !== v[n] >= level) {
          const t = (level - v[k]) / (v[n] - v[k]);
          intersections.push([
            points[k][0] + t * (points[n][0] - points[k][0]),
            points[k][1] + t * (points[n][1] - points[k][1]),
          ]);
        }
      }
      if (intersections.length === 2) segments.push(intersections);
      if (intersections.length === 4) {
        const center = v.reduce((a, b) => a + b, 0) / 4;
        const pairs =
          center >= level === v[0] >= level
            ? [
                [0, 1],
                [2, 3],
              ]
            : [
                [0, 3],
                [1, 2],
              ];
        for (const [a, b] of pairs)
          segments.push([intersections[a], intersections[b]]);
      }
    }
  return { level, color, segments, height_m: grid.height_m };
}
