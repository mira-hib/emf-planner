use serde::{Deserialize, Serialize};
use uuid::Uuid;

#[derive(Debug, Clone, Serialize, Deserialize)]
pub struct RadiatingSource {
    pub id: String,
    pub name: String,
    pub frequency_mhz: f64,
    pub power_w: f64,
    pub gain_dbi: f64,
    pub lat: f64,
    pub lng: f64,
    pub height_m: f64,
    pub azimuth_deg: f64,
    pub tilt_deg: f64,
    #[serde(default = "default_one")]
    pub duty_cycle: f64,
    #[serde(default = "default_one")]
    pub power_reduction: f64,
    #[serde(default = "default_one")]
    pub alpha24: f64,
    #[serde(default)]
    pub ground_reflection_k: f64,
    #[serde(default)]
    pub service_type: String,
    #[serde(default = "default_one_u32")]
    pub num_tx: u32,
    #[serde(default)]
    pub total_attenuation_db: f64,
    #[serde(default)]
    pub antenna_model: String,
    #[serde(default)]
    pub antenna_size_m: f64,
    #[serde(default)]
    pub mechanical_downtilt_deg: f64,
    #[serde(default)]
    pub electrical_downtilt_deg: f64,
    #[serde(default)]
    pub beam_width_h_deg: f64,
    #[serde(default)]
    pub beam_width_v_deg: f64,
    #[serde(default)]
    pub location: String,
    #[serde(default)]
    pub owner: String,
    #[serde(default)]
    pub sector_id: String,
}

fn default_one() -> f64 { 1.0 }
fn default_one_u32() -> u32 { 1 }

impl RadiatingSource {
    pub fn new(name: String, frequency_mhz: f64, power_w: f64, lat: f64, lng: f64) -> Self {
        Self {
            id: Uuid::new_v4().to_string(),
            name,
            frequency_mhz,
            power_w,
            gain_dbi: 0.0,
            lat,
            lng,
            height_m: 10.0,
            azimuth_deg: 0.0,
            tilt_deg: 0.0,
            duty_cycle: 1.0,
            power_reduction: 1.0,
            alpha24: 1.0,
            ground_reflection_k: 1.0,
            service_type: String::new(),
            num_tx: 1,
            total_attenuation_db: 0.0,
            antenna_model: String::new(),
            antenna_size_m: 0.0,
            mechanical_downtilt_deg: 0.0,
            electrical_downtilt_deg: 0.0,
            beam_width_h_deg: 65.0,
            beam_width_v_deg: 7.0,
            location: String::new(),
            owner: String::new(),
            sector_id: String::new(),
        }
    }

    pub fn eirp_w(&self) -> f64 {
        let p_total = self.power_w * self.num_tx as f64;
        let attenuation_linear = 10_f64.powf(-self.total_attenuation_db / 10.0);
        p_total * attenuation_linear * 10_f64.powf(self.gain_dbi / 10.0)
    }

    // PTXAM = P_TX * num_TX * attenuation * F_TDC * F_PP * alpha24 (eq.1 from manual)
    pub fn ptxam_w(&self) -> f64 {
        let p_total = self.power_w * self.num_tx as f64;
        let attenuation_linear = 10_f64.powf(-self.total_attenuation_db / 10.0);
        p_total * attenuation_linear * self.duty_cycle * self.power_reduction * self.alpha24
    }

    pub fn eirp_attenuated_w(&self) -> f64 {
        self.ptxam_w() * 10_f64.powf(self.gain_dbi / 10.0)
    }
}

pub fn electric_field_at_distance(source: &RadiatingSource, distance_m: f64) -> f64 {
    if distance_m <= 0.0 {
        return f64::INFINITY;
    }
    let eirp = source.eirp_attenuated_w();
    // E = sqrt(30 * EIRP) / d  (ITU-T K.70)
    let e = (30.0 * eirp).sqrt() / distance_m;
    let k = if source.ground_reflection_k > 0.0 { source.ground_reflection_k } else { 1.0 };
    e * k
}

use crate::propagation::{self, PropagationModel};

pub fn electric_field_with_propagation(
    source: &RadiatingSource,
    distance_m: f64,
    height_rx: f64,
    model: &PropagationModel,
) -> f64 {
    if distance_m <= 0.0 {
        return f64::INFINITY;
    }
    let eirp_dbm = 10.0 * source.eirp_attenuated_w().log10() + 30.0;
    let loss = propagation::path_loss_db(model, source.frequency_mhz, distance_m, source.height_m, height_rx);
    let p_rx_dbm = eirp_dbm - loss;
    let p_rx_w = 10_f64.powf((p_rx_dbm - 30.0) / 10.0);
    // E = sqrt(377 * S) where S = P_rx (assuming isotropic receive, free-space impedance)
    let e = (377.0 * p_rx_w.abs() / (4.0 * std::f64::consts::PI)).sqrt();
    let k = if source.ground_reflection_k > 0.0 { source.ground_reflection_k } else { 1.0 };
    e * k
}

// Power density S = E² / 377 (W/m²) — free-space impedance
pub fn power_density(e_field_v_m: f64) -> f64 {
    e_field_v_m * e_field_v_m / 377.0
}

pub fn calculate_total_field(sources: Vec<RadiatingSource>, lat: f64, lng: f64, height_m: f64) -> f64 {
    let mut sum_e_squared = 0.0;
    for source in &sources {
        let dist = haversine_distance(source.lat, source.lng, lat, lng);
        let dh = (source.height_m - height_m).abs();
        let dist_3d = (dist * dist + dh * dh).sqrt();
        let e = electric_field_at_distance(source, dist_3d);
        sum_e_squared += e * e;
    }
    sum_e_squared.sqrt()
}

pub fn safety_distance(source: &RadiatingSource, limit_v_m: f64) -> f64 {
    if limit_v_m <= 0.0 {
        return f64::INFINITY;
    }
    let eirp = source.eirp_attenuated_w();
    let k = if source.ground_reflection_k > 0.0 { source.ground_reflection_k } else { 1.0 };
    (30.0 * eirp).sqrt() * k / limit_v_m
}

// ICNIRP 2020 general public exposure limits (E-field, V/m)
pub fn icnirp_limit(frequency_mhz: f64) -> f64 {
    if frequency_mhz < 1.0 {
        87.0
    } else if frequency_mhz < 10.0 {
        83.0 / frequency_mhz.sqrt()
    } else if frequency_mhz < 400.0 {
        28.0
    } else if frequency_mhz < 2000.0 {
        1.375 * frequency_mhz.sqrt()
    } else {
        61.0
    }
}

// Simultaneous exposure ratio R = sum(Ei/Limiti)^2 — compliant if R < 1
pub fn simultaneous_exposure_ratio(sources: &[RadiatingSource], lat: f64, lng: f64, height_m: f64) -> f64 {
    let mut ratio = 0.0;
    for source in sources {
        let dist = haversine_distance(source.lat, source.lng, lat, lng);
        let dh = (source.height_m - height_m).abs();
        let dist_3d = (dist * dist + dh * dh).sqrt();
        let e = electric_field_at_distance(source, dist_3d);
        let limit = icnirp_limit(source.frequency_mhz);
        if limit > 0.0 {
            ratio += (e / limit).powi(2);
        }
    }
    ratio
}

pub fn haversine_distance(lat1: f64, lon1: f64, lat2: f64, lon2: f64) -> f64 {
    let r = 6_371_000.0;
    let d_lat = (lat2 - lat1).to_radians();
    let d_lon = (lon2 - lon1).to_radians();
    let a = (d_lat / 2.0).sin().powi(2)
        + lat1.to_radians().cos() * lat2.to_radians().cos() * (d_lon / 2.0).sin().powi(2);
    let c = 2.0 * a.sqrt().asin();
    r * c
}
