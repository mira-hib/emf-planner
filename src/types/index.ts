export interface RadiatingSource {
  feeder?: {
    cable: string;
    length_m: number;
    jumper: string;
    jumper_lengths: string;
    automatic_connectors: boolean;
    include_connectors?: boolean;
    connectors: number;
    connector_loss: number;
    other_loss: number;
  };
  active?: boolean;
  color?: string;
  is_existing?: boolean;
  pattern_h?: number[];
  pattern_v?: number[];
  limit_factors?: number[][];
  x_m?: number;
  y_m?: number;
  id: string;
  name: string;
  frequency_mhz: number;
  frequency_end_mhz?: number;
  power_w: number;
  gain_dbi: number;
  lat: number;
  lng: number;
  height_m: number;
  azimuth_deg: number;
  tilt_deg: number;
  duty_cycle: number;
  power_reduction: number;
  alpha24: number;
  ground_reflection_k: number;
  service_type: string;
  num_tx: number;
  total_attenuation_db: number;
  antenna_model: string;
  antenna_size_m: number;
  mechanical_downtilt_deg: number;
  electrical_downtilt_deg: number;
  beam_width_h_deg: number;
  beam_width_v_deg: number;
  location: string;
  owner: string;
  sector_id: string;
}

export interface AntennaPattern {
  name: string;
  horizontal: PatternPoint[];
  vertical: PatternPoint[];
}

export interface PatternPoint {
  angle_deg: number;
  gain_db: number;
}

export type PropagationModel = "FreeSpace" | "ITU_R_P1546" | "CostHata";

export interface ProjectData {
  name: string;
  description: string;
  sources: RadiatingSource[];
  antennas: AntennaPattern[];
  control_points: ControlPoint[];
  propagation_model: PropagationModel;
  safety_limit_v_m: number;
  map_center: [number, number];
  map_zoom: number;
  origin?: [number, number];
  buildings?: Building[];
  terrain_points?: TerrainPoint[];
  lines?: DrawingLine[];
  horizontal_cuts?: HorizontalCut[];
  vertical_cuts?: VerticalCut[];
  limits?: LimitSet[];
  settings?: CalculationSettings;
  background_image?: BackgroundImage;
  antenna_library?: StoredAntenna[];
  owner_names?: string[];
  custom_maps?: CustomMap[];
  terrain_contours?: TerrainContour[];
  volume_options?: VolumeOptions;
  report_aliases?: Record<string, string>;
  word_template?: string;
  cad_options?: {
    scale: number;
    paper: "A4" | "A3" | "A2";
    layers?: Record<string, boolean>;
  };
  saved_cut_results?: {
    horizontal: SavedCutResult[];
    vertical: SavedCutResult[];
  };
}

export interface SourceContribution {
  source_id: string;
  field_v_m: number;
  power_density_w_m2: number;
  icnirp_limit: number;
  ratio_contribution: number;
  distance_m: number;
}

export interface ExposureResult {
  total_field: number;
  total_power_density: number;
  exposure_ratio: number;
  compliant: boolean;
  per_source: SourceContribution[];
}

export interface GridPoint {
  lat: number;
  lng: number;
  field_v_m: number;
  power_density: number;
  exposure_ratio: number;
}

export interface VerticalCutPoint {
  distance_m: number;
  height_m: number;
  field_v_m: number;
  power_density: number;
  exposure_ratio: number;
}

export interface ControlPoint {
  id: string;
  name: string;
  lat: number;
  lng: number;
  x_m: number;
  y_m: number;
  height_m: number;
  distance_m: number;
  azimuth_deg: number;
  attenuation_db: number;
  measured_e_field_v_m: number | null;
  calculated_e_field_v_m: number | null;
  exposure_ratio: number | null;
  compliant: boolean | null;
  group?: string;
  location?: string;
  heights?: PointHeight[];
  description?: string;
}

export interface PointHeight {
  id: string;
  height_m: number;
  attenuation_db: number;
  measured: number;
  description: string;
}
export interface Building {
  id: string;
  name: string;
  height_m: number;
  foundation_m: number;
  attenuation_db: number;
  points: [number, number][];
  active: boolean;
  visible?: boolean;
}
export interface TerrainPoint {
  id: string;
  x: number;
  y: number;
  elevation: number;
  wm_x?: number;
  wm_y?: number;
}
export interface DrawingLine {
  id: string;
  name: string;
  kind: "road" | "barrier" | "label";
  points: [number, number][];
  color: string;
  width: number;
  icon?: string;
  border_width?: number;
  fence_type?: number;
  font_name?: string;
  font_size?: number;
  angle_deg?: number;
  show_point?: boolean;
}
export interface HorizontalCut {
  id: string;
  active: boolean;
  height_m: number;
  color: string;
  line_style?: string;
}
export interface CutDimension {
  id: string;
  x: number;
  y1: number;
  y2: number;
}
export interface VerticalCut {
  geo_position?: [number, number];
  id: string;
  active: boolean;
  azimuth_deg: number;
  x: number;
  y: number;
  backward_m: number;
  forward_m: number;
  max_height_m: number;
  dimensions?: CutDimension[];
}
export interface LimitRow {
  frequency: number;
  value: number;
  exponent: number;
}
export interface LimitSet {
  id: string;
  name: string;
  color: string;
  rows: LimitRow[];
  use_alpha24: boolean;
  use_ftdc: boolean;
  use_fpr: boolean;
  active: boolean;
}
export interface CalculationSettings {
  site_coordinates?: [number, number];
  calculation_area_wm?: [[number, number], [number, number]];
  horizontal_grid_divisions?: [number, number];
  show_calculation_area?: boolean;
  show_origin?: boolean;
  reflection: number;
  radius_m: number;
  grid_step_m: number;
  fine_step_m: number;
  consider_terrain: boolean;
  buildings_mode: "ignore" | "screen" | "attenuate";
  building_attenuation_db: number;
  selected_limit: number;
  map_provider: string;
  show_basemap: boolean;
  terrain_interpolation?: "original" | "delaunay";
  coordinate_format?: "decimal" | "dms" | "dm";
  contour_width?: number;
  contour_opacity?: number;
  language?: string;
  antenna_folder?: string;
  cache_enabled?: boolean;
  proxy_url?: string;
  proxy?: { host: string; port: number; user?: string };
  include_charts_in_word?: boolean;
  report_aliases?: Record<string, string>;
  word_template_name?: string;
  calculation_area_m?: {
    x_min: number;
    x_max: number;
    y_min: number;
    y_max: number;
  };
  display_options?: Partial<{
    grid: boolean;
    buildings: boolean;
    terrain: boolean;
    lines: boolean;
    sources: boolean;
    points: boolean;
    contours: boolean;
    labels: boolean;
    azimuths: boolean;
  }>;
  building_labels?: { name: boolean; height: boolean; foundation: boolean };
  line_visibility?: { roads: boolean; fences: boolean; labels: boolean };
  control_point_labels?: { prefix: string; groups: boolean; heights: boolean };
}
export interface StoredAntenna {
  id: string;
  name: string;
  gain: number;
  frequency?: number;
  horizontal: number[];
  vertical: number[];
}
export interface CustomMap {
  name: string;
  url: string;
  attribution: string;
}
export interface TerrainContour {
  id: string;
  name: string;
  elevation: number;
  closed: boolean;
  points: [number, number][];
}
export interface VolumeOptions {
  center_x: number;
  center_y: number;
  radius_m: number;
  bottom_m: number;
  top_m: number;
  step_m: number;
}
export interface ExposureVolume {
  positions: Float32Array;
  nodes: number;
  triangles: number;
  step_m: number;
  clipped: boolean;
  limit_index: number;
  color: string;
}
export interface BackgroundImage {
  data_url: string;
  west: number;
  south: number;
  east: number;
  north: number;
  opacity: number;
}
export interface PlannerProject extends ProjectData {
  origin: [number, number];
  buildings: Building[];
  terrain_points: TerrainPoint[];
  lines: DrawingLine[];
  horizontal_cuts: HorizontalCut[];
  vertical_cuts: VerticalCut[];
  limits: LimitSet[];
  settings: CalculationSettings;
}
export type Section =
  | "sources"
  | "limits"
  | "origin"
  | "antennas"
  | "buildings"
  | "terrain"
  | "lines"
  | "points"
  | "horizontal"
  | "vertical"
  | "tables"
  | "report"
  | "cad"
  | "3d"
  | "calibrate"
  | "settings";
export interface PointResult extends ExposureResult {
  point_id: string;
  name: string;
  height_m: number;
  x: number;
  y: number;
  measured: number;
  elevation: number;
}
export interface FieldGrid {
  width: number;
  height: number;
  x0: number;
  y0: number;
  dx: number;
  dy: number;
  values: number[];
  fields: number[];
  height_m: number;
}
export interface Contour {
  level: number;
  color: string;
  segments: [number, number][][];
  height_m: number;
  line_style?: string;
}

// Serialized contours contain geometry, not the underlying sampled field values.
export interface SavedCutResult {
  id: string;
  cut_id?: string;
  limit_index?: number;
  grid: FieldGrid;
  contours: Contour;
  contours_only: true;
}

export interface FeederType {
  name: string;
  diameter: string;
  data_points: { frequency_mhz: number; attenuation_db_per_100m: number }[];
}

export interface MapTileProvider {
  name: string;
  url: string;
  attribution: string;
  maxZoom: number;
}
