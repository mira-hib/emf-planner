use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, Serialize, Deserialize)]
pub struct AntennaPattern {
    pub name: String,
    pub horizontal: Vec<PatternPoint>,
    pub vertical: Vec<PatternPoint>,
}

#[derive(Debug, Clone, Serialize, Deserialize)]
pub struct PatternPoint {
    pub angle_deg: f64,
    pub gain_db: f64,
}

impl AntennaPattern {
    pub fn isotropic() -> Self {
        let points: Vec<PatternPoint> = (0..360)
            .map(|a| PatternPoint { angle_deg: a as f64, gain_db: 0.0 })
            .collect();
        Self {
            name: "Isotropic".to_string(),
            horizontal: points.clone(),
            vertical: points,
        }
    }

    pub fn gain_at(&self, azimuth_deg: f64, elevation_deg: f64) -> f64 {
        let h = interpolate_pattern(&self.horizontal, azimuth_deg.rem_euclid(360.0));
        let v = interpolate_pattern(&self.vertical, (elevation_deg + 180.0).rem_euclid(360.0));
        h + v
    }
}

fn interpolate_pattern(points: &[PatternPoint], angle: f64) -> f64 {
    if points.is_empty() {
        return 0.0;
    }
    let mut below = &points[0];
    let mut above = &points[0];
    for p in points {
        if p.angle_deg <= angle {
            below = p;
        }
        if p.angle_deg >= angle {
            above = p;
            break;
        }
    }
    if (above.angle_deg - below.angle_deg).abs() < 1e-9 {
        return below.gain_db;
    }
    let t = (angle - below.angle_deg) / (above.angle_deg - below.angle_deg);
    below.gain_db + t * (above.gain_db - below.gain_db)
}
