use std::{collections::HashMap, fs, path::PathBuf, sync::Mutex, time::Duration};
use serde::{Deserialize, Serialize};
use sha2::{Digest, Sha256};
use tauri::Manager;

#[derive(Default)]
pub struct NetworkState { client: Mutex<Option<reqwest::Client>>, directory: Mutex<Option<PathBuf>> }
#[derive(Deserialize)]
pub struct ProxyConfig { host: String, port: u16, user: Option<String>, password: Option<String> }
#[derive(Deserialize)]
pub struct NetworkRequest { url: String, method: Option<String>, headers: Option<HashMap<String, String>>, body: Option<String>, cache: Option<bool> }
#[derive(Serialize, Deserialize)]
pub struct NetworkResponse { status: u16, content_type: String, bytes: Vec<u8> }
#[derive(Serialize)]
pub struct CacheStats { pub count: usize, pub bytes: u64, pub available: bool, pub directory: String }

fn cache_directory(app: &tauri::AppHandle, state: &NetworkState) -> Result<PathBuf, String> {
    state.directory.lock().map_err(|e|e.to_string())?.clone()
        .map(Ok).unwrap_or_else(|| app.path().app_cache_dir().map(|p|p.join("tiles")).map_err(|e|e.to_string()))
}
fn cache_files(path: &PathBuf) -> Vec<fs::DirEntry> {
    fs::read_dir(path).into_iter().flatten().flatten().filter(|entry|
        entry.file_name().to_string_lossy().strip_prefix("emf-map-").and_then(|name|name.strip_suffix(".tile")).is_some_and(|stem|stem.len() == 64 && stem.chars().all(|c|c.is_ascii_hexdigit()))
        && entry.file_type().is_ok_and(|kind|kind.is_file())).collect()
}

#[tauri::command]
pub async fn configure_network(app: tauri::AppHandle, state: tauri::State<'_, NetworkState>, proxy: Option<ProxyConfig>, directory: Option<String>) -> Result<(), String> {
    let mut builder = reqwest::Client::builder().timeout(Duration::from_secs(30)).connect_timeout(Duration::from_secs(10))
        .user_agent("EMF-Planner/0.1 (+desktop cartography)").redirect(reqwest::redirect::Policy::limited(5));
    if let Some(settings) = proxy {
        if settings.port == 0 || settings.host.trim().is_empty() || settings.host.contains(['/', '@', '?', '#', ' ']) { return Err("Adresse ou port de proxy invalide.".into()); }
        let host = if settings.host.contains(':') && !settings.host.starts_with('[') { format!("[{}]", settings.host) } else { settings.host };
        let mut proxy = reqwest::Proxy::all(format!("http://{host}:{}", settings.port)).map_err(|_|"Adresse proxy invalide.".to_string())?;
        if let Some(user) = settings.user { proxy = proxy.basic_auth(&user, settings.password.as_deref().unwrap_or("")); }
        builder = builder.proxy(proxy);
    }
    let folder = match directory.filter(|value|!value.trim().is_empty()) {
        Some(value) => PathBuf::from(value), None => app.path().app_cache_dir().map_err(|e|e.to_string())?.join("tiles") };
    if !folder.is_absolute() { return Err("Le dossier du cache doit être un chemin absolu.".into()); }
    fs::create_dir_all(&folder).map_err(|e|format!("Dossier du cache inaccessible : {e}"))?;
    let probe = folder.join(format!(".emf-write-check-{}", uuid::Uuid::new_v4()));
    fs::write(&probe, b"cache write check").map_err(|e|format!("Le dossier du cache n’est pas inscriptible : {e}"))?;
    fs::remove_file(probe).map_err(|e|e.to_string())?;
    let client = builder.build().map_err(|_|"Configuration réseau invalide.".to_string())?;
    *state.client.lock().map_err(|e|e.to_string())? = Some(client);
    *state.directory.lock().map_err(|e|e.to_string())? = Some(folder);
    Ok(())
}

#[tauri::command]
pub async fn network_request(app: tauri::AppHandle, state: tauri::State<'_, NetworkState>, request: NetworkRequest) -> Result<NetworkResponse, String> {
    let url = reqwest::Url::parse(&request.url).map_err(|_|"URL réseau invalide.".to_string())?;
    if !["http", "https"].contains(&url.scheme()) { return Err("Seules les URL HTTP et HTTPS sont acceptées.".into()); }
    let method = request.method.as_deref().unwrap_or("GET");
    if !["GET", "POST"].contains(&method) { return Err("Méthode réseau non prise en charge.".into()); }
    let use_cache = request.cache.unwrap_or(false) && method == "GET";
    let folder = cache_directory(&app, &state)?;
    let digest = format!("{:x}", Sha256::digest(request.url.as_bytes()));
    let cached = folder.join(format!("emf-map-{digest}.tile"));
    let metadata = folder.join(format!("emf-map-{digest}.json"));
    if use_cache && fs::metadata(&cached).is_ok_and(|info|info.len() <= 32 * 1024 * 1024) {
        if let (Ok(bytes), Ok(kind)) = (fs::read(&cached), fs::read_to_string(&metadata)) {
            if kind.starts_with("image/") { return Ok(NetworkResponse { status: 200, content_type: kind, bytes }); }
        }
    }
    let configured = state.client.lock().map_err(|e|e.to_string())?.clone();
    let client = match configured { Some(client) => client, None => reqwest::Client::builder().timeout(Duration::from_secs(30)).connect_timeout(Duration::from_secs(10)).build().map_err(|e|e.to_string())? };
    let mut outgoing = if method == "POST" { client.post(url) } else { client.get(url) };
    if let Some(headers) = request.headers {
        for (key, value) in headers {
            if !["content-type", "accept"].contains(&key.to_ascii_lowercase().as_str()) { return Err("En-tête réseau non pris en charge.".into()); }
            outgoing = outgoing.header(key, value);
        }
    }
    if let Some(body) = request.body { if body.len() > 4 * 1024 * 1024 { return Err("Requête réseau trop volumineuse.".into()); } outgoing = outgoing.body(body); }
    let mut response = outgoing.send().await.map_err(|_|"Le serveur ou le proxy est inaccessible (délai maximum 30 s).".to_string())?;
    let status = response.status().as_u16();
    let content_type = response.headers().get("content-type").and_then(|v|v.to_str().ok()).unwrap_or("application/octet-stream").to_string();
    let mut bytes = Vec::new();
    while let Some(chunk) = response.chunk().await.map_err(|_|"La réponse réseau a été interrompue.".to_string())? {
        if bytes.len() + chunk.len() > 64 * 1024 * 1024 { return Err("La réponse réseau dépasse 64 Mo.".into()); } bytes.extend_from_slice(&chunk);
    }
    if use_cache && status == 200 && content_type.starts_with("image/") && bytes.len() <= 32 * 1024 * 1024 {
        if fs::create_dir_all(&folder).is_ok() && fs::write(&cached, &bytes).is_ok() {
            let _ = fs::write(&metadata, &content_type);
            let mut entries = cache_files(&folder); entries.sort_by_key(|entry|entry.metadata().ok().and_then(|m|m.modified().ok()));
            let excess = entries.len().saturating_sub(1500);
            for entry in entries.into_iter().take(excess) { let _ = fs::remove_file(entry.path().with_extension("json")); let _ = fs::remove_file(entry.path()); }
        }
    }
    Ok(NetworkResponse { status, content_type, bytes })
}

#[tauri::command]
pub fn native_cache_stats(app: tauri::AppHandle, state: tauri::State<'_, NetworkState>) -> Result<CacheStats, String> {
    let path = cache_directory(&app, &state)?;
    fs::create_dir_all(&path).map_err(|e|e.to_string())?;
    let entries = cache_files(&path);
    Ok(CacheStats { count: entries.len(), bytes: entries.iter().filter_map(|entry|entry.metadata().ok()).map(|m|m.len()).sum(), available: true, directory: path.to_string_lossy().into_owned() })
}
#[tauri::command]
pub fn clear_native_cache(app: tauri::AppHandle, state: tauri::State<'_, NetworkState>) -> Result<(), String> {
    let path = cache_directory(&app, &state)?;
    // Delete only files generated by our cache, never recurse through a user's chosen directory.
    for entry in cache_files(&path) {
        if entry.file_name().to_string_lossy().strip_prefix("emf-map-").and_then(|name|name.strip_suffix(".tile")).is_some_and(|stem|stem.len() == 64 && stem.chars().all(|c|c.is_ascii_hexdigit())) {
            fs::remove_file(entry.path()).map_err(|e|e.to_string())?;
            let _ = fs::remove_file(entry.path().with_extension("json"));
        }
    }
    Ok(())
}
