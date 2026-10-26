mod emf;
mod antenna;
mod propagation;
mod project;
mod export;
mod feeder;
mod control_point;
mod native_exports;
mod network;

use emf::RadiatingSource;
use control_point::ControlPoint;
use project::ProjectData;
use serde::Serialize;

#[tauri::command]
fn create_source(name: String, frequency_mhz: f64, power_w: f64, lat: f64, lng: f64) -> RadiatingSource {
    RadiatingSource::new(name, frequency_mhz, power_w, lat, lng)
}

#[tauri::command]
fn calculate_field_strength(sources: Vec<RadiatingSource>, lat: f64, lng: f64, height_m: f64) -> f64 {
    emf::calculate_total_field(sources, lat, lng, height_m)
}

#[tauri::command]
fn calculate_safety_zone(source: RadiatingSource, limit_v_m: f64) -> f64 {
    emf::safety_distance(&source, limit_v_m)
}

#[derive(Serialize)]
struct ExposureResult {
    total_field: f64,
    total_power_density: f64,
    exposure_ratio: f64,
    compliant: bool,
    per_source: Vec<SourceContribution>,
}

#[derive(Serialize)]
struct SourceContribution {
    source_id: String,
    field_v_m: f64,
    power_density_w_m2: f64,
    icnirp_limit: f64,
    ratio_contribution: f64,
    distance_m: f64,
}

#[tauri::command]
fn calculate_exposure(sources: Vec<RadiatingSource>, lat: f64, lng: f64, height_m: f64) -> ExposureResult {
    let mut per_source = Vec::new();
    let mut sum_e_squared = 0.0;
    let mut sum_ratio = 0.0;

    for source in &sources {
        let dist = emf::haversine_distance(source.lat, source.lng, lat, lng);
        let dh = (source.height_m - height_m).abs();
        let dist_3d = (dist * dist + dh * dh).sqrt();
        let e = emf::electric_field_at_distance(source, dist_3d);
        let limit = emf::icnirp_limit(source.frequency_mhz);
        let ratio = if limit > 0.0 { (e / limit).powi(2) } else { 0.0 };

        sum_e_squared += e * e;
        sum_ratio += ratio;

        per_source.push(SourceContribution {
            source_id: source.id.clone(),
            field_v_m: e,
            power_density_w_m2: emf::power_density(e),
            icnirp_limit: limit,
            ratio_contribution: ratio,
            distance_m: dist_3d,
        });
    }

    let total_e = sum_e_squared.sqrt();
    ExposureResult {
        total_field: total_e,
        total_power_density: emf::power_density(total_e),
        exposure_ratio: sum_ratio,
        compliant: sum_ratio < 1.0,
        per_source,
    }
}

#[tauri::command]
fn get_icnirp_limit(frequency_mhz: f64) -> f64 {
    emf::icnirp_limit(frequency_mhz)
}

#[tauri::command]
fn get_feeders() -> Vec<feeder::FeederType> {
    feeder::builtin_feeders()
}

#[tauri::command]
fn calculate_feeder_loss(feeder_name: String, frequency_mhz: f64, length_m: f64) -> f64 {
    let feeders = feeder::builtin_feeders();
    feeders.iter()
        .find(|f| f.name == feeder_name)
        .map(|f| f.total_loss(frequency_mhz, length_m))
        .unwrap_or(0.0)
}

#[tauri::command]
fn create_control_point(name: String, lat: f64, lng: f64, height_m: f64) -> ControlPoint {
    ControlPoint::new(name, lat, lng, height_m)
}

#[tauri::command]
fn evaluate_control_point(mut point: ControlPoint, sources: Vec<RadiatingSource>) -> ControlPoint {
    control_point::evaluate_point(&mut point, &sources);
    point
}

#[tauri::command]
fn evaluate_all_control_points(mut points: Vec<ControlPoint>, sources: Vec<RadiatingSource>) -> Vec<ControlPoint> {
    for point in &mut points {
        control_point::evaluate_point(point, &sources);
    }
    points
}

#[derive(Serialize)]
struct GridPoint {
    lat: f64,
    lng: f64,
    field_v_m: f64,
    power_density: f64,
    exposure_ratio: f64,
}

#[tauri::command]
fn calculate_field_grid(
    sources: Vec<RadiatingSource>,
    center_lat: f64,
    center_lng: f64,
    radius_m: f64,
    height_m: f64,
    resolution: u32,
) -> Vec<GridPoint> {
    let step = (radius_m * 2.0) / resolution as f64;
    let mut grid = Vec::with_capacity((resolution * resolution) as usize);

    for iy in 0..resolution {
        for ix in 0..resolution {
            let dx = -radius_m + ix as f64 * step;
            let dy = -radius_m + iy as f64 * step;
            let dlat = dy / 111_320.0;
            let dlng = dx / (111_320.0 * center_lat.to_radians().cos());
            let lat = center_lat + dlat;
            let lng = center_lng + dlng;

            let mut sum_e_sq = 0.0;
            let mut sum_ratio = 0.0;
            for source in &sources {
                let dist = emf::haversine_distance(source.lat, source.lng, lat, lng);
                let dh = (source.height_m - height_m).abs();
                let dist_3d = (dist * dist + dh * dh).sqrt();
                let e = emf::electric_field_at_distance(source, dist_3d);
                let limit = emf::icnirp_limit(source.frequency_mhz);
                sum_e_sq += e * e;
                if limit > 0.0 {
                    sum_ratio += (e / limit).powi(2);
                }
            }
            let total_e = sum_e_sq.sqrt();
            grid.push(GridPoint {
                lat,
                lng,
                field_v_m: total_e,
                power_density: emf::power_density(total_e),
                exposure_ratio: sum_ratio,
            });
        }
    }
    grid
}

#[derive(Serialize)]
struct VerticalCutPoint {
    distance_m: f64,
    height_m: f64,
    field_v_m: f64,
    power_density: f64,
    exposure_ratio: f64,
}

#[tauri::command]
fn calculate_vertical_cut(
    sources: Vec<RadiatingSource>,
    origin_lat: f64,
    origin_lng: f64,
    azimuth_deg: f64,
    max_distance_m: f64,
    max_height_m: f64,
    dist_steps: u32,
    height_steps: u32,
) -> Vec<VerticalCutPoint> {
    let az_rad = azimuth_deg.to_radians();
    let mut points = Vec::with_capacity((dist_steps * height_steps) as usize);

    for ih in 0..height_steps {
        let h = max_height_m * ih as f64 / height_steps as f64;
        for id in 0..dist_steps {
            let d = max_distance_m * (id + 1) as f64 / dist_steps as f64;
            let dlat = (d * az_rad.cos()) / 111_320.0;
            let dlng = (d * az_rad.sin()) / (111_320.0 * origin_lat.to_radians().cos());
            let lat = origin_lat + dlat;
            let lng = origin_lng + dlng;

            let mut sum_e_sq = 0.0;
            let mut sum_ratio = 0.0;
            for source in &sources {
                let dist = emf::haversine_distance(source.lat, source.lng, lat, lng);
                let dh = (source.height_m - h).abs();
                let dist_3d = (dist * dist + dh * dh).sqrt();
                let e = emf::electric_field_at_distance(source, dist_3d);
                let limit = emf::icnirp_limit(source.frequency_mhz);
                sum_e_sq += e * e;
                if limit > 0.0 {
                    sum_ratio += (e / limit).powi(2);
                }
            }
            let total_e = sum_e_sq.sqrt();
            points.push(VerticalCutPoint {
                distance_m: d,
                height_m: h,
                field_v_m: total_e,
                power_density: emf::power_density(total_e),
                exposure_ratio: sum_ratio,
            });
        }
    }
    points
}

#[tauri::command]
fn save_project(path: String, data: ProjectData) -> Result<(), String> {
    project::save(&path, &data).map_err(|e| e.to_string())
}

#[tauri::command]
fn load_project(path: String) -> Result<ProjectData, String> {
    project::load(&path).map_err(|e| e.to_string())
}

#[tauri::command]
fn export_dxf(path: String, data: ProjectData) -> Result<(), String> {
    export::to_dxf(&path, &data).map_err(|e| e.to_string())
}

#[cfg_attr(mobile, tauri::mobile_entry_point)]
pub fn run() {
    tauri::Builder::default()
        .manage(network::NetworkState::default())
        .plugin(tauri_plugin_opener::init())
        .plugin(tauri_plugin_dialog::init())
        .plugin(tauri_plugin_fs::init())
        .invoke_handler(tauri::generate_handler![
            create_source,
            calculate_field_strength,
            calculate_safety_zone,
            calculate_exposure,
            calculate_field_grid,
            calculate_vertical_cut,
            get_icnirp_limit,
            get_feeders,
            calculate_feeder_loss,
            create_control_point,
            evaluate_control_point,
            evaluate_all_control_points,
            save_project,
            load_project,
            export_dxf,
            native_exports::convert_idtf_u3d,
            native_exports::dxf_to_dwg,
            native_exports::dwg_to_dxf,
            native_exports::convert_word_template,
            native_exports::convert_excel_workbook,
            network::configure_network,
            network::network_request,
            network::native_cache_stats,
            network::clear_native_cache,
        ])
        .run(tauri::generate_context!())
        .expect("error while running tauri application");
}
