use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, Serialize, Deserialize)]
pub enum PropagationModel {
    FreeSpace,
    #[allow(non_camel_case_types)]
    ITU_R_P1546,
    CostHata,
}

pub fn path_loss_db(model: &PropagationModel, frequency_mhz: f64, distance_m: f64, height_tx: f64, height_rx: f64) -> f64 {
    match model {
        PropagationModel::FreeSpace => free_space_loss(frequency_mhz, distance_m),
        PropagationModel::ITU_R_P1546 => itu_r_p1546(frequency_mhz, distance_m, height_tx),
        PropagationModel::CostHata => cost_hata(frequency_mhz, distance_m, height_tx, height_rx),
    }
}

fn free_space_loss(frequency_mhz: f64, distance_m: f64) -> f64 {
    if distance_m <= 0.0 || frequency_mhz <= 0.0 {
        return 0.0;
    }
    let d_km = distance_m / 1000.0;
    32.44 + 20.0 * d_km.log10() + 20.0 * frequency_mhz.log10()
}

fn cost_hata(frequency_mhz: f64, distance_m: f64, h_b: f64, h_m: f64) -> f64 {
    let d_km = distance_m / 1000.0;
    if d_km < 1.0 || frequency_mhz < 150.0 || frequency_mhz > 2000.0 {
        return free_space_loss(frequency_mhz, distance_m);
    }
    let a_hm = (1.1 * frequency_mhz.log10() - 0.7) * h_m - (1.56 * frequency_mhz.log10() - 0.8);
    46.3 + 33.9 * frequency_mhz.log10() - 13.82 * h_b.log10()
        - a_hm
        + (44.9 - 6.55 * h_b.log10()) * d_km.log10()
}

fn itu_r_p1546(frequency_mhz: f64, distance_m: f64, h_tx: f64) -> f64 {
    // Simplified ITU-R P.1546 approximation
    let d_km = distance_m / 1000.0;
    if d_km <= 0.0 {
        return 0.0;
    }
    let base_loss = free_space_loss(frequency_mhz, distance_m);
    let height_correction = if h_tx > 10.0 { -6.0 * (h_tx / 10.0).log10() } else { 0.0 };
    base_loss + height_correction
}
