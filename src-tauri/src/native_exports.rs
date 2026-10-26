//! External format encoders run in isolated temporary directories. No shell text
//! contains document data or file names.
use std::{fs, path::{Path, PathBuf}, process::{Command, Stdio}, time::{Duration, Instant}};
use tauri::Manager;

struct Workspace(PathBuf);
impl Workspace {
    fn new() -> Result<Self, String> {
        let token = std::time::SystemTime::now().duration_since(std::time::UNIX_EPOCH).map_err(|e|e.to_string())?.as_nanos();
        let path = std::env::temp_dir().join(format!("emf-export-{}-{token}", std::process::id()));
        fs::create_dir(&path).map_err(|e|e.to_string())?;
        Ok(Self(path))
    }
}
impl Drop for Workspace { fn drop(&mut self) { let _ = fs::remove_dir_all(&self.0); } }

fn run(command: &mut Command, directory: &Path) -> Result<(), String> {
    command.current_dir(directory).stdin(Stdio::null());
    #[cfg(windows)] {
        use std::os::windows::process::CommandExt;
        command.creation_flags(0x08000000);
    }
    // Redirect to files so a verbose converter cannot fill a pipe and deadlock.
    let log = directory.join("converter.log");
    let err = directory.join("converter-error.log");
    command.stdout(fs::File::create(&log).map_err(|e|e.to_string())?);
    command.stderr(fs::File::create(&err).map_err(|e|e.to_string())?);
    let mut child = command.spawn().map_err(|e|format!("Démarrage du convertisseur impossible : {e}"))?;
    let started = Instant::now();
    loop {
        match child.try_wait().map_err(|e|e.to_string())? {
            Some(status) if status.success() => return Ok(()),
            Some(status) => {
                let message = fs::read_to_string(err).unwrap_or_default();
                return Err(format!("Échec du convertisseur ({status}) : {}", message.chars().take(2000).collect::<String>()));
            }
            None if started.elapsed() > Duration::from_secs(120) => {
                let _ = child.kill(); let _ = child.wait();
                return Err("Le convertisseur a dépassé le délai de 120 secondes.".into());
            }
            None => std::thread::sleep(Duration::from_millis(30)),
        }
    }
}

fn tool(app: &tauri::AppHandle, name: &str) -> Result<PathBuf, String> {
    let executable = if cfg!(windows) { format!("{name}.exe") } else { name.to_owned() };
    let mut directories = vec![app.path().resource_dir().map_err(|e|e.to_string())?.join("native")];
    if let Some(parent) = std::env::current_exe().ok().as_deref().and_then(Path::parent) {
        directories.push(parent.join("native"));
    }
    if cfg!(debug_assertions) { directories.push(PathBuf::from(env!("CARGO_MANIFEST_DIR")).join("native")); }
    for base in directories {
        let candidate = base.join(&executable);
        if candidate.is_file() { return Ok(candidate); }
    }
    Err(format!("Le convertisseur {executable} est absent de cette installation. Réinstallez le paquet complet."))
}

fn read_output(path: &Path) -> Result<Vec<u8>, String> {
    let size = fs::metadata(path).map_err(|_|"Le convertisseur n’a produit aucun fichier.".to_string())?.len();
    if size == 0 || size > 100 * 1024 * 1024 { return Err("La taille du fichier produit est invalide (maximum 100 Mo).".into()); }
    fs::read(path).map_err(|e|e.to_string())
}

#[tauri::command]
pub async fn convert_idtf_u3d(app: tauri::AppHandle, idtf: String) -> Result<Vec<u8>, String> {
    tauri::async_runtime::spawn_blocking(move || {
        if idtf.len() > 80 * 1024 * 1024 || !idtf.starts_with("FILE_FORMAT") { return Err("Scène IDTF invalide ou trop volumineuse.".into()); }
        let workspace = Workspace::new()?;
        let input = workspace.0.join("scene.idtf"); let output = workspace.0.join("scene.u3d");
        fs::write(&input, idtf).map_err(|e|e.to_string())?;
        let converter = tool(&app, "IDTFConverter")?;
        let mut command = Command::new(&converter);
        // Intel's plug-in loader locates IFXCore relative to its working directory.
        command.arg("-input").arg(&input).arg("-output").arg(&output);
        if let Some(directory) = converter.parent() {
            command.env("U3D_LIBDIR", directory);
            let mut paths = vec![directory.to_path_buf()];
            paths.extend(std::env::split_paths(&std::env::var_os("PATH").unwrap_or_default()));
            command.env("PATH", std::env::join_paths(paths).map_err(|e|e.to_string())?);
        }
        run(&mut command, &workspace.0)?;
        let bytes = read_output(&output)?;
        if !bytes.starts_with(b"U3D\0") { return Err("Le convertisseur n’a pas produit un flux U3D valide.".into()); }
        Ok(bytes)
    }).await.map_err(|e|e.to_string())?
}

#[tauri::command]
pub async fn dxf_to_dwg(app: tauri::AppHandle, dxf: String) -> Result<Vec<u8>, String> {
    tauri::async_runtime::spawn_blocking(move || {
        if dxf.len() > 80 * 1024 * 1024 || !dxf.contains("SECTION") { return Err("Dessin DXF invalide ou trop volumineux.".into()); }
        let workspace = Workspace::new()?; let input = workspace.0.join("drawing.dxf"); let output = workspace.0.join("drawing.dwg");
        fs::write(&input, dxf).map_err(|e|e.to_string())?;
        run(Command::new(tool(&app, "dxf2dwg")?).arg("--as").arg("r2000").arg("-o").arg(&output).arg(&input), &workspace.0)?;
        let bytes = read_output(&output)?;
        if !bytes.starts_with(b"AC1015") { return Err("Le convertisseur n’a pas produit un DWG R2000 valide.".into()); }
        Ok(bytes)
    }).await.map_err(|e|e.to_string())?
}

#[tauri::command]
pub async fn dwg_to_dxf(app: tauri::AppHandle, bytes: Vec<u8>) -> Result<Vec<u8>, String> {
    tauri::async_runtime::spawn_blocking(move || {
        if bytes.len() > 80 * 1024 * 1024 || !bytes.starts_with(b"AC10") { return Err("Fichier DWG invalide ou trop volumineux.".into()); }
        let workspace = Workspace::new()?; let input = workspace.0.join("drawing.dwg"); let output = workspace.0.join("drawing.dxf");
        fs::write(&input, bytes).map_err(|e|e.to_string())?;
        run(Command::new(tool(&app, "dwg2dxf")?).arg("-o").arg(&output).arg(&input), &workspace.0)?;
        read_output(&output)
    }).await.map_err(|e|e.to_string())?
}

#[cfg(windows)]
fn office_convert(path: String, excel: bool) -> Result<Vec<u8>, String> {
    let input = fs::canonicalize(&path).map_err(|e|e.to_string())?;
    let extension = input.extension().and_then(|s|s.to_str()).unwrap_or("").to_ascii_lowercase();
    if !(if excel { ["xls", "xlt"].contains(&extension.as_str()) } else { ["doc", "dot"].contains(&extension.as_str()) }) {
        return Err("Extension de document Office historique invalide.".into());
    }
    if fs::metadata(&input).map_err(|e|e.to_string())?.len() > 50 * 1024 * 1024 { return Err("Document supérieur à 50 Mo.".into()); }
    let workspace = Workspace::new()?;
    let script = workspace.0.join("convert.ps1"); let output = workspace.0.join(if excel { "converted.xlsx" } else { "converted.docx" });
    fs::write(&script, include_str!("office-convert.ps1")).map_err(|e|e.to_string())?;
    let powershell = PathBuf::from(std::env::var_os("SystemRoot").ok_or("SystemRoot absent")?).join("System32/WindowsPowerShell/v1.0/powershell.exe");
    run(Command::new(powershell).arg("-NoProfile").arg("-NonInteractive").arg("-ExecutionPolicy").arg("Bypass")
        .arg("-File").arg(&script).arg("-InputPath").arg(&input).arg("-OutputPath").arg(&output)
        .arg("-Application").arg(if excel { "Excel" } else { "Word" }), &workspace.0)?;
    read_output(&output)
}

#[tauri::command]
pub async fn convert_word_template(path: String) -> Result<Vec<u8>, String> {
    #[cfg(windows)] { tauri::async_runtime::spawn_blocking(move || office_convert(path, false)).await.map_err(|e|e.to_string())? }
    #[cfg(not(windows))] { let _ = path; Err("Les modèles .dot/.doc nécessitent Word sous Windows. Convertissez le modèle en .dotx ou .docx.".into()) }
}
#[tauri::command]
pub async fn convert_excel_workbook(path: String) -> Result<Vec<u8>, String> {
    #[cfg(windows)] { tauri::async_runtime::spawn_blocking(move || office_convert(path, true)).await.map_err(|e|e.to_string())? }
    #[cfg(not(windows))] { let _ = path; Err("Les classeurs .xls/.xlt nécessitent Excel sous Windows. Convertissez le classeur en .xlsx ou .xltx.".into()) }
}
