export interface RadiatingSource {
  id: string;
  name: string;
  frequency_mhz: number;
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
