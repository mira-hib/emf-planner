use serde::{Deserialize, Serialize};
use uuid::Uuid;

#[derive(Debug, Clone, Serialize, Deserialize)]
pub struct ControlPoint {
    pub id: String,
    pub name: String,
    pub lat: f64,
    pub lng: f64,
    pub x_m: f64,
    pub y_m: f64,
    pub height_m: f64,
    pub distance_m: f64,
    pub azimuth_deg: f64,
    pub attenuation_db: f64,
    pub measured_e_field_v_m: Option<f64>,
    pub calculated_e_field_v_m: Option<f64>,
    pub exposure_ratio: Option<f64>,
    pub compliant: Option<bool>,
}

impl ControlPoint {
    pub fn new(name: String, lat: f64, lng: f64, height_m: f64) -> Self {
        Self {
            id: Uuid::new_v4().to_string(),
            name,
            lat,
            lng,
            x_m: 0.0,
            y_m: 0.0,
            height_m,
            distance_m: 0.0,
            azimuth_deg: 0.0,
            attenuation_db: 0.0,
            measured_e_field_v_m: None,
            calculated_e_field_v_m: None,
            exposure_ratio: None,
            compliant: None,
        }
    }
}

use crate::emf::{self, RadiatingSource};

pub fn evaluate_point(
    point: &mut ControlPoint,
    sources: &[RadiatingSource],
) {
    let mut sum_e_sq = 0.0;
    let mut sum_ratio = 0.0;

    for source in sources {
        let dist = emf::haversine_distance(source.lat, source.lng, point.lat, point.lng);
        let dh = (source.height_m - point.height_m).abs();
        let dist_3d = (dist * dist + dh * dh).sqrt();
        let e = emf::electric_field_at_distance(source, dist_3d);
        let limit = emf::icnirp_limit(source.frequency_mhz);

        sum_e_sq += e * e;
        if limit > 0.0 {
            sum_ratio += (e / limit).powi(2);
        }
    }

    point.calculated_e_field_v_m = Some(sum_e_sq.sqrt());
    point.exposure_ratio = Some(sum_ratio);
    point.compliant = Some(sum_ratio < 1.0);
}
