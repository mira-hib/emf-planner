import type {
  ControlPoint,
  PlannerProject,
  ProjectData,
  RadiatingSource,
} from "../types";

export const uid = () => crypto.randomUUID();
export const COLORS = ["#d32246", "#ed9834", "#2855dd", "#18813d"];
export function newProject(): PlannerProject {
  return {
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
    limits: COLORS.map((color, i) => ({
      id: uid(),
      name: `Limites ${i + 1}`,
      color,
      active: true,
      use_alpha24: true,
      use_ftdc: true,
      use_fpr: true,
      rows: [
        { frequency: 400, value: 28, exponent: 0 },
        { frequency: 2000, value: 1.375, exponent: 0.5 },
        { frequency: 100000, value: 61, exponent: 0 },
      ],
    })),
    settings: {
      reflection: 1,
      radius_m: 150,
      grid_step_m: 3,
      fine_step_m: 1,
      consider_terrain: true,
      buildings_mode: "ignore",
      building_attenuation_db: 20,
      selected_limit: 0,
      map_provider: "Carto clair",
      show_basemap: false,
    },
  };
}
export function newSource(
  origin: [number, number],
  name: string,
): RadiatingSource {
  return {
    id: uid(),
    name,
    active: true,
    color: "#ffffff",
    frequency_mhz: 900,
    power_w: 10,
    gain_dbi: 16,
    lat: origin[0],
    lng: origin[1],
    x_m: 0,
    y_m: 0,
    height_m: 25,
    azimuth_deg: 0,
    tilt_deg: 0,
    duty_cycle: 1,
    power_reduction: 1,
    alpha24: 1,
    ground_reflection_k: 1,
    service_type: "GSM",
    num_tx: 1,
    total_attenuation_db: 0,
    antenna_model: "Secteur 65°",
    antenna_size_m: 1.9,
    mechanical_downtilt_deg: 0,
    electrical_downtilt_deg: 0,
    beam_width_h_deg: 65,
    beam_width_v_deg: 10,
    location: "Tower",
    owner: "",
    sector_id: "",
    is_existing: false,
  };
}
export function newPoint(origin: [number, number], name: string): ControlPoint {
  return {
    id: uid(),
    name,
    group: "Groupe 1",
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
    origin: data.origin ?? data.map_center,
    settings: { ...defaults.settings, ...data.settings },
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
  return project;
}
