use serde::{Deserialize, Serialize};
use crate::emf::RadiatingSource;
use crate::antenna::AntennaPattern;
use crate::propagation::PropagationModel;
use crate::control_point::ControlPoint;
use std::fs;
use std::io;

#[derive(Debug, Clone, Serialize, Deserialize)]
pub struct ProjectData {
    pub name: String,
    pub description: String,
    pub sources: Vec<RadiatingSource>,
    pub antennas: Vec<AntennaPattern>,
    #[serde(default)]
    pub control_points: Vec<ControlPoint>,
    pub propagation_model: PropagationModel,
    pub safety_limit_v_m: f64,
    pub map_center: [f64; 2],
    pub map_zoom: u8,
}

impl Default for ProjectData {
    fn default() -> Self {
        Self {
            name: "New Project".to_string(),
            description: String::new(),
            sources: Vec::new(),
            antennas: vec![AntennaPattern::isotropic()],
            control_points: Vec::new(),
            propagation_model: PropagationModel::FreeSpace,
            safety_limit_v_m: 28.0, // ICNIRP public limit at 900 MHz
            map_center: [5.345, -4.025], // Abidjan default
            map_zoom: 13,
        }
    }
}

pub fn save(path: &str, data: &ProjectData) -> io::Result<()> {
    let json = serde_json::to_string_pretty(data)
        .map_err(|e| io::Error::new(io::ErrorKind::Other, e))?;
    fs::write(path, json)
}

pub fn load(path: &str) -> io::Result<ProjectData> {
    let content = fs::read_to_string(path)?;
    serde_json::from_str(&content)
        .map_err(|e| io::Error::new(io::ErrorKind::InvalidData, e))
}
