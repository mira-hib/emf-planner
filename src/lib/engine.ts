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
import { BuildingIndex } from "./building-index";

interface PointContext {
  excludedBuilding?: string;
  inBuilding: boolean;
}
interface PreparedSource {
  sx: number; sy: number; z: number;
  rayKey: string;
  cosAz: number; sinAz: number; cosTilt: number; sinTilt: number;
  horizontal: Float64Array; vertical: Float64Array;
  amplitudes: number[];
  effectiveLimits?: number[];
}
function prepareSource(project: PlannerProject, source: RadiatingSource): PreparedSource {
  const position = toLocal(source.lat, source.lng, project.origin);
  const sx = source.x_m ?? position[0], sy = source.y_m ?? position[1];
  const building = source.location === "Roof" || source.location === "Facade"
    ? project.buildings.find(b => inside(sx, sy, b.points)) : undefined;
  const az = rad(source.azimuth_deg < 0 ? 0 : source.azimuth_deg), tilt = rad(-source.mechanical_downtilt_deg);
  const pattern = (values: number[] | undefined, width: number) => Float64Array.from({ length: 360 }, (_, i) => patternAmplitude(values, i, width));
  const z = source.height_m + (building ? building.foundation_m : terrainHeight(project, ...position));
  return { sx, sy, z, rayKey: `${sx},${sy},${z}`,
    cosAz: Math.cos(az), sinAz: Math.sin(az), cosTilt: Math.cos(tilt), sinTilt: Math.sin(tilt),
    horizontal: pattern(source.pattern_h, 65), vertical: pattern(source.pattern_v, 10),
    amplitudes: project.limits.map((limit, index) => {
      const f = source.limit_factors?.[index] ?? [source.alpha24, source.duty_cycle, source.power_reduction];
      const factor = (limit.use_alpha24 ? f[0] : 1) * (limit.use_ftdc ? f[1] : 1) * (limit.use_fpr ? f[2] : 1);
      return project.settings.reflection * Math.sqrt(Math.max(0, 30 * eirp(source) * factor));
    }) };
}
const runtime = new WeakMap<PlannerProject, Map<RadiatingSource, PreparedSource>>();
const buildingRuntime = new WeakMap<PlannerProject, BuildingIndex>();
const blockedRuntime = new WeakMap<PlannerProject, Map<string, boolean>>();
function prepareGrid(project: PlannerProject) {
  buildingRuntime.set(project, new BuildingIndex(project.buildings));
  blockedRuntime.set(project, new Map());
  runtime.set(project, new Map(project.sources.filter(s => s.active !== false && s.power_w > 0).map(s => [s, prepareSource(project, s)])));
}
/** Equivalent colocated emitters can share one angular/obstruction calculation.
 * Their squared amplitudes are summed independently for every limit. Point
 * reports retain each individual source through the ordinary exposureAt path. */
function gridSources(project: PlannerProject): RadiatingSource[] {
  const sources = project.sources.filter(s => s.active !== false).sort((a,b)=>a.id.localeCompare(b.id));
  if (sources.length < 64) return sources;
  const prepared = runtime.get(project)!, groups = new Map<string, RadiatingSource[]>();
  for (const source of sources) {
    const p = prepared.get(source); if (!p) continue;
    // Frequency only changes the penetration-loss band and the exposure divisor.
    // Aggregate the latter independently from total field power for every limit.
    const lossBand = project.settings.buildings_mode === "attenuate" && project.settings.building_attenuation_high_db !== undefined
      ? source.frequency_mhz <= (project.settings.building_frequency_boundary_mhz ?? 400) : false;
    const key = JSON.stringify([p.sx,p.sy,p.z,p.cosAz,p.sinAz,p.cosTilt,p.sinTilt,lossBand,source.pattern_h,source.pattern_v]);
    const group = groups.get(key); if(group) group.push(source); else groups.set(key,[source]);
  }
  if (groups.size > sources.length / 2) return sources;
  return [...groups.values()].map(group => {
    if(group.length===1)return group[0];
    const first = group[0], prototype = prepared.get(first)!;
    const representative = { ...first, power_w: 1, num_tx: 1, gain_dbi: 0, total_attenuation_db: 0 };
    const amplitudes = project.limits.map((_, index) => Math.sqrt(group.reduce((sum, source) => sum + prepared.get(source)!.amplitudes[index] ** 2, 0)));
    const effectiveLimits = project.limits.map((limit,index) => {
      const ratioPower = group.reduce((sum,source) => sum + (prepared.get(source)!.amplitudes[index] / limitAt(limit,source.frequency_mhz)) ** 2, 0);
      return ratioPower > 0 ? amplitudes[index] / Math.sqrt(ratioPower) : 1;
    });
    prepared.set(representative, { ...prototype, amplitudes, effectiveLimits });
    return representative;
  });
}
function preparedPattern(values: Float64Array, angle: number) {
  const a = wrap(angle), low = Math.floor(a), f = a - low;
  return values[low] * (1 - f) + values[(low + 1) % 360] * f;
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
  const cached = runtime.get(project)?.get(source);
  const position = cached ? [cached.sx, cached.sy] : toLocal(source.lat, source.lng, project.origin),
    sx = source.x_m ?? position[0],
    sy = source.y_m ?? position[1],
    dx = x - sx,
    dy = y - sy;
  const roof = source.location === "Roof" || source.location === "Facade";
  const building = roof && !cached
    ? project.buildings.find((b) => inside(sx, sy, b.points))
    : undefined;
  const sourceZ = cached?.z ?? (
    source.height_m +
    (building
      ? building.foundation_m
      : terrainHeight(project, position[0], position[1])));
  const dz = elevation - sourceZ;
  const distance = Math.max(0.05, Math.hypot(dx, dy, dz));
  let extraLoss = lossDb;
  const blocked = project.settings.buildings_mode === "screen" && cached && !context ? blockedRuntime.get(project) : undefined;
  const previousRay = cached ? blocked?.get(cached.rayKey) : undefined;
  if (previousRay === true) return { field: 0, distance };
  if (project.settings.buildings_mode !== "ignore" && previousRay !== false) {
    const index = buildingRuntime.get(project);
    const candidates = index ? (project.settings.buildings_mode === "screen" ? index.ray(sx, sy, x, y) : index.point(x, y)).map(entry => entry.building)
      : project.buildings.filter(b => b.active);
    for (const b of candidates) {
      const inBuilding =
        project.settings.buildings_mode === "attenuate" && inside(x, y, b.points) && elevation < b.foundation_m + b.height_m;
      if (
        project.settings.buildings_mode === "screen" &&
        b.id !== context?.excludedBuilding
      ) {
        if (Math.min(sourceZ, elevation) >= b.foundation_m + b.height_m || Math.max(sourceZ, elevation) <= b.foundation_m) continue;
        for (let i = 0; i < b.points.length; i++) {
          const t = segmentCrosses([sx, sy], [x, y], b.points[i], b.points[(i + 1) % b.points.length]);
          if (t !== null && sourceZ + dz * t < b.foundation_m + b.height_m && sourceZ + dz * t > b.foundation_m) {
            if(cached)blocked?.set(cached.rayKey,true);
            return { field: 0, distance };
          }
        }
      }
      if (
        project.settings.buildings_mode === "attenuate" &&
        inBuilding &&
        b.custom_attenuation !== false &&
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
    if(cached)blocked?.set(cached.rayKey,false);
  }
  // Opaque rays exit before evaluating either antenna pattern.
  const az = rad(source.azimuth_deg < 0 ? 0 : source.azimuth_deg),
    localX = dx * (cached?.cosAz ?? Math.cos(az)) - dy * (cached?.sinAz ?? Math.sin(az)),
    forward = dx * (cached?.sinAz ?? Math.sin(az)) + dy * (cached?.cosAz ?? Math.cos(az));
  const tilt = rad(-source.mechanical_downtilt_deg),
    tiltedY = forward * (cached?.cosTilt ?? Math.cos(tilt)) - dz * (cached?.sinTilt ?? Math.sin(tilt)),
    tiltedZ = forward * (cached?.sinTilt ?? Math.sin(tilt)) + dz * (cached?.cosTilt ?? Math.cos(tilt));
  const horizontal = wrap(deg(Math.atan2(localX, tiltedY)));
  const vertical = wrap(-deg(Math.atan2(tiltedZ, Math.hypot(localX, tiltedY))));
  const h = cached && source.pattern_h?.length === 360 ? preparedPattern(cached.horizontal, horizontal) : patternAmplitude(source.pattern_h, horizontal, 65);
  const v = cached && source.pattern_v?.length === 360 ? preparedPattern(cached.vertical, vertical) : patternAmplitude(source.pattern_v, vertical, 10);
  const limit = project.limits[limitIndex] ?? project.limits[0];
  const factors = source.limit_factors?.[limitIndex] ?? [source.alpha24, source.duty_cycle, source.power_reduction];
  const factor = (limit.use_alpha24 ? factors[0] : 1) * (limit.use_ftdc ? factors[1] : 1) * (limit.use_fpr ? factors[2] : 1);
  const field =
    (((cached?.amplitudes[limitIndex] ?? (project.settings.reflection *
      Math.sqrt(Math.max(0, 30 * eirp(source) * factor)))) *
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
      return {
        source_id: source.id,
        field_v_m: field,
        power_density_w_m2: (field * field) / 377,
        icnirp_limit: limit,
        ratio_contribution: r,
        distance_m: distance,
      };
    });
  // The visual row order must not affect floating point reduction (MET-09).
  const ordered = [...per_source].sort((a, b) => a.source_id.localeCompare(b.source_id));
  let e2 = 0, ratio = 0;
  for (const item of ordered) {
    e2 += item.field_v_m * item.field_v_m;
    ratio += item.ratio_contribution;
  }
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
    values = new Float64Array(countX * countY),
    fields = new Float64Array(countX * countY);
  prepareGrid(project);
  const orderedSources = gridSources(project);
  const originElevation = terrainHeight(project, 0, 0);
  for (let j = 0; j < countY; j++)
    for (let i = 0; i < countX; i++) {
      const x = area.x_min + i * dx,
        y = area.y_min + j * dy,
        r = gridTotals(project, orderedSources, x, y, height + originElevation);
      values[j * countX + i] = r.ratio;
      fields[j * countX + i] = r.field;
    }
  runtime.delete(project);
  buildingRuntime.delete(project);
  blockedRuntime.delete(project);
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
  divisions?: number,
): FieldGrid {
  const below = cut.min_height_m ?? 0;
  if (![cut.forward_m, cut.backward_m, cut.max_height_m, below, cut.x, cut.y, cut.azimuth_deg].every(Number.isFinite)
      || cut.forward_m <= 0 || cut.backward_m < 0 || cut.max_height_m <= 0 || below < 0)
    throw new Error("Dimensions de coupe invalides.");
  if (divisions !== undefined && (!Number.isInteger(divisions) || divisions < 50 || divisions > 1000))
    throw new Error("La matrice doit comporter entre 50 et 1000 divisions.");
  const width =
      divisions !== undefined ? divisions + 1 : Math.ceil((cut.forward_m + cut.backward_m) / Math.max(step, 0.5)) + 1,
    height = divisions !== undefined ? divisions + 1 : Math.ceil((cut.max_height_m + below) / Math.max(step, 0.5)) + 1;
  if (width > (divisions === undefined ? 501 : 1001) || height > (divisions === undefined ? 301 : 1001))
    throw new Error("Coupe trop grande. Augmentez le pas de calcul.");
  const dx = (cut.forward_m + cut.backward_m) / (width - 1),
    dy = (cut.max_height_m + below) / (height - 1),
    values = new Float64Array(width * height),
    fields = new Float64Array(width * height);
  prepareGrid(project);
  const orderedSources = gridSources(project);
  const originElevation = terrainHeight(project, 0, 0);
  for (let j = 0; j < height; j++)
    for (let i = 0; i < width; i++) {
      const d = -cut.backward_m + i * dx,
        x = cut.x + d * Math.sin(rad(cut.azimuth_deg)),
        y = cut.y + d * Math.cos(rad(cut.azimuth_deg));
      const r = gridTotals(project, orderedSources, x, y, j * dy - below + originElevation);
      values[j * width + i] = r.ratio;
      fields[j * width + i] = r.field;
    }
  runtime.delete(project);
  buildingRuntime.delete(project);
  return {
    width,
    height,
    x0: -cut.backward_m,
    y0: -below,
    dx,
    dy,
    values,
    fields,
    height_m: 0,
  };
}
function gridTotals(project: PlannerProject, sources: RadiatingSource[], x: number, y: number, z: number) {
  let e2 = 0, ratio = 0;
  blockedRuntime.get(project)?.clear();
  const index = project.settings.selected_limit, limit = project.limits[index] ?? project.limits[0];
  for (const source of sources) {
    const { field } = sourceField(project, source, x, y, z, index);
    e2 += field * field;
    ratio += (field / (runtime.get(project)?.get(source)?.effectiveLimits?.[index] ?? limitAt(limit, source.frequency_mhz))) ** 2;
  }
  return { field: Math.sqrt(e2), ratio };
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
