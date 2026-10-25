import type {
  ControlPoint,
  PlannerProject,
  ProjectData,
  RadiatingSource,
} from "../types";
import { applyProjectPreset } from "./presets";
import { initialNativeArea } from "./origin-grid";
import { readPreferences } from "./preferences";

export const uid = () => crypto.randomUUID();
export const COLORS = ["#d32246", "#ed9834", "#2855dd", "#18813d"];
export function newProject(): PlannerProject {
  const preferences = readPreferences();
  return {
    owner_names: preferences.owner_names,
    owner_colors: preferences.owner_colors,
    name: "Nouveau projet",
    description: "",
    sources: [],
    antennas: [],
    control_points: [],
    propagation_model: "FreeSpace",
    safety_limit_v_m: 28,
    map_center: [5.345, -4.025],
    origin: [5.345, -4.025],
    map_zoom: 18,
    buildings: [],
    terrain_points: [],
    lines: [],
    horizontal_cuts: [
      { id: uid(), active: true, height_m: 1.5, color: COLORS[0] },
    ],
    vertical_cuts: [],
    limits: applyProjectPreset("ICNIRP", [])!,
    settings: {
      coordinate_format: preferences.coordinate_format,
      site_coordinates: [5.345, -4.025],
      ...initialNativeArea([5.345, -4.025]),
      horizontal_grid_divisions: [600, 200],
      show_calculation_area: true,
      show_origin: true,
      reflection: 1.3,
      radius_m: 150,
      grid_step_m: 3,
      fine_step_m: 1,
      consider_terrain: true,
      buildings_mode: "ignore",
      building_attenuation_db: 3,
      building_attenuation_high_db: 6,
      building_frequency_boundary_mhz: 400,
      point_measurement_frequency_mhz: 1000,
      selected_limit: 0,
      map_provider: "Carto Basemap",
      antenna_capture_distance: 15,
      show_basemap: true,
    },
  };
}
export function newSource(
  origin: [number, number],
  name = "",
): RadiatingSource {
  return {
    id: uid(),
    name,
    active: true,
    color: "#ffffff",
    frequency_mhz: 1000,
    power_w: 0,
    gain_dbi: 0,
    lat: origin[0],
    lng: origin[1],
    x_m: 0,
    y_m: 0,
    height_m: 0,
    azimuth_deg: 0,
    tilt_deg: 0,
    duty_cycle: 1,
    power_reduction: 1,
    alpha24: 1,
    ground_reflection_k: 1,
    service_type: "Other",
    num_tx: 1,
    total_attenuation_db: 0,
    antenna_model: "",
    antenna_size_m: 1,
    mechanical_downtilt_deg: 0,
    electrical_downtilt_deg: 0,
    beam_width_h_deg: 65,
    beam_width_v_deg: 10,
    location: "Tower",
    owner: "",
    sector_id: "",
    is_existing: true,
  };
}
export function newPoint(origin: [number, number], name: string): ControlPoint {
  return {
    id: uid(),
    name,
    group: "Groupe 1",
    group_active: true,
    lat: origin[0],
    lng: origin[1],
    x_m: 0,
    y_m: 0,
    height_m: 1.5,
    distance_m: 0,
    azimuth_deg: 0,
    attenuation_db: 0,
    measured_e_field_v_m: null,
    calculated_e_field_v_m: null,
    exposure_ratio: null,
    compliant: null,
    location: "Ground",
    heights: [
      {
        id: uid(),
        height_m: 1.5,
        attenuation_db: 0,
        measured: 0,
        description: "",
      },
    ],
  };
}
export function normalizeProject(data: ProjectData): PlannerProject {
  if (
    !data ||
    !Array.isArray(data.sources) ||
    !Array.isArray(data.map_center) ||
    data.map_center.length !== 2
  )
    throw new Error("Le fichier ne contient pas un projet EMF valide.");
  const defaults = newProject();
  const project = {
    ...defaults,
    ...data,
    owner_names: defaults.owner_names ?? data.owner_names,
    owner_colors: defaults.owner_colors ?? data.owner_colors,
    origin: data.origin ?? data.map_center,
    settings: {
      ...defaults.settings,
      ...data.settings,
      coordinate_format: defaults.settings.coordinate_format ?? data.settings?.coordinate_format,
      site_coordinates:
        data.settings?.site_coordinates ?? data.origin ?? data.map_center,
      horizontal_grid_divisions: data.settings?.horizontal_grid_divisions,
      calculation_area_m: data.settings?.calculation_area_m,
      calculation_area_wm: data.settings?.calculation_area_wm,
      building_attenuation_high_db: data.settings?.building_attenuation_high_db,
      building_frequency_boundary_mhz:
        data.settings?.building_frequency_boundary_mhz,
    },
  } as PlannerProject;
  project.sources = data.sources.map((s) => ({
    ...newSource(project.origin, s.name),
    ...s,
    id: s.id || uid(),
  }));
  project.control_points = (data.control_points ?? []).map((p) => ({
    ...newPoint(project.origin, p.name),
    ...p,
    heights: p.heights?.length
      ? p.heights
      : [
          {
            id: uid(),
            height_m: p.height_m ?? 1.5,
            attenuation_db: p.attenuation_db ?? 0,
            measured: p.measured_e_field_v_m ?? 0,
            description: "",
          },
        ],
  }));
  for (const [lat, lng] of [project.origin, project.map_center])
    if (
      !Number.isFinite(lat) ||
      !Number.isFinite(lng) ||
      Math.abs(lat) > 85 ||
      Math.abs(lng) > 180
    )
      throw new Error("Coordonnées géographiques invalides.");
  for (const s of project.sources)
    if (
      ![s.frequency_mhz, s.power_w, s.gain_dbi, s.height_m, s.lat, s.lng].every(
        Number.isFinite,
      ) ||
      s.frequency_mhz <= 0 ||
      s.power_w < 0 ||
      s.num_tx < 1
    )
      throw new Error(`Paramètres de source invalides : ${s.name}`);
  if (project.sources.length > 5000 || project.control_points.length > 20000)
    throw new Error("Le projet dépasse la capacité de cette version.");
  const finite = (values: unknown[]) =>
    values.every((v) => typeof v === "number" && Number.isFinite(v));
  const settings = project.settings;
  if (
    !finite([
      settings.reflection,
      settings.radius_m,
      settings.grid_step_m,
      settings.fine_step_m,
    ]) ||
    settings.reflection < 1 ||
    settings.reflection > Math.fround(1.6) ||
    settings.radius_m <= 0 ||
    settings.grid_step_m <= 0 ||
    settings.fine_step_m <= 0 ||
    !Number.isInteger(settings.selected_limit) ||
    settings.selected_limit < 0 ||
    settings.selected_limit >= project.limits.length
  )
    throw new Error(
      "Paramètres de calcul invalides (réflexion entre 1 et 1,6 et pas positifs requis).",
    );
  if (
    settings.horizontal_grid_divisions?.some(
      (n) => !Number.isInteger(n) || n < 50 || n > 1000,
    )
  )
    throw new Error("Résolution de grille invalide : 50 à 1000 divisions.");
  if (
    settings.point_measurement_frequency_mhz !== undefined &&
    (!finite([settings.point_measurement_frequency_mhz]) ||
      settings.point_measurement_frequency_mhz < 30 ||
      settings.point_measurement_frequency_mhz > 300000)
  )
    throw new Error("Fréquence de mesure invalide : 30 à 300 000 MHz.");
  if (
    !finite([settings.building_attenuation_db]) ||
    settings.building_attenuation_db < 0 ||
    (settings.building_attenuation_high_db !== undefined &&
      (!finite([settings.building_attenuation_high_db]) ||
        settings.building_attenuation_high_db < 0)) ||
    (settings.building_frequency_boundary_mhz !== undefined &&
      (!finite([settings.building_frequency_boundary_mhz]) ||
        settings.building_frequency_boundary_mhz < 0))
  )
    throw new Error("Paramètres d’atténuation des bâtiments invalides.");
  for (const s of project.sources) {
    if (
      !finite([
        s.num_tx,
        s.azimuth_deg,
        s.mechanical_downtilt_deg,
        s.electrical_downtilt_deg,
        s.total_attenuation_db,
        s.duty_cycle,
        s.power_reduction,
        s.alpha24,
        s.lat,
        s.lng,
      ]) ||
      !Number.isInteger(s.num_tx) ||
      s.num_tx < 1 ||
      Math.abs(s.lat) > 85 ||
      Math.abs(s.lng) > 180 ||
      s.total_attenuation_db < 0 ||
      [s.alpha24, s.duty_cycle, s.power_reduction].some(
        (n) => n < 0 || n > 1,
      ) ||
      [s.x_m, s.y_m, s.frequency_end_mhz].some(
        (n) => n !== undefined && !finite([n]),
      ) ||
      [s.pattern_h, s.pattern_v].some(
        (p) =>
          p !== undefined &&
          (!Array.isArray(p) || p.length !== 360 || !finite(p)),
      )
    )
      throw new Error(`Paramètres de source invalides : ${s.name}`);
    if (
      s.limit_factors?.some(
        (f) => f.length !== 3 || !finite(f) || f.some((n) => n < 0 || n > 1),
      )
    )
      throw new Error(`Facteurs de source invalides : ${s.name}`);
  }
  let heightCount = 0;
  for (const p of project.control_points) {
    heightCount += p.heights?.length ?? 1;
    if (
      !finite([p.lat, p.lng, p.x_m, p.y_m]) ||
      Math.abs(p.lat) > 85 ||
      Math.abs(p.lng) > 180 ||
      p.heights?.some(
        (h) =>
          !finite([h.height_m, h.attenuation_db, h.measured]) ||
          h.height_m < 0 ||
          h.attenuation_db < 0 ||
          h.measured < 0,
      )
    )
      throw new Error(`Paramètres de point invalides : ${p.name}`);
  }
  if (heightCount > 20000)
    throw new Error("Le projet dépasse 20 000 hauteurs de points.");
  if (project.buildings.length > 20000 || project.terrain_points.length > 100000 || (project.terrain_contours?.length ?? 0) > 10000 || project.lines.length > 20000)
    throw new Error("Le projet dépasse la capacité des objets géométriques.");
  const validPoints = (points: unknown, minimum: number): boolean => Array.isArray(points) && points.length >= minimum && points.length <= 100000
    && points.every(point => Array.isArray(point) && point.length === 2 && finite(point));
  for (const source of project.sources)
    if (!Number.isFinite(30 * source.power_w * source.num_tx * 10 ** ((source.gain_dbi - source.total_attenuation_db) / 10)))
      throw new Error(`La puissance apparente de ${source.name} dépasse la capacité numérique.`);
  for (const building of project.buildings)
    if (!finite([building.height_m, building.foundation_m, building.attenuation_db]) || building.height_m < 0 || building.attenuation_db < 0 || !validPoints(building.points, 3))
      throw new Error(`Bâtiment invalide : ${building.name}`);
  for (const point of project.terrain_points)
    if (!finite([point.x, point.y, point.elevation])) throw new Error("Point de terrain invalide.");
  for (const line of project.terrain_contours ?? [])
    if (!finite([line.elevation]) || !validPoints(line.points, 2)) throw new Error("Courbe de niveau invalide.");
  for (const line of project.lines)
    if (!finite([line.width]) || line.width < 0 || !validPoints(line.points, line.kind === "label" ? 1 : 2)) throw new Error(`Ligne invalide : ${line.name}`);
  for (const cut of project.horizontal_cuts)
    if (!finite([cut.height_m]) || cut.height_m < 0) throw new Error("Hauteur de coupe horizontale invalide.");
  for (const cut of project.vertical_cuts)
    if (!finite([cut.azimuth_deg, cut.x, cut.y, cut.forward_m, cut.backward_m, cut.max_height_m, cut.min_height_m ?? 0]) || cut.forward_m <= 0 || cut.backward_m < 0 || cut.max_height_m <= 0 || (cut.min_height_m ?? 0) < 0)
      throw new Error("Dimensions de coupe verticale invalides.");
  return project;
}
