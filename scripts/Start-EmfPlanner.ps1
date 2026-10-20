param([switch]$Development)
$ErrorActionPreference = 'Stop'
$plannerRoot = Split-Path -Parent $PSScriptRoot
if ($Development) {
    . (Join-Path $PSScriptRoot 'Use-ReaTools.ps1')
    $env:RUSTUP_HOME = Join-Path $plannerRoot '.tools\rustup'
    $env:CARGO_HOME = Join-Path $plannerRoot '.tools\cargo'
    $env:Path = "$env:CARGO_HOME\bin;$env:Path"
    Push-Location -LiteralPath $plannerRoot
    try { npm.cmd run tauri dev } finally { Pop-Location }
} else {
    $plannerExe = Join-Path $plannerRoot 'src-tauri\target\release\emf-planner.exe'
    if (-not (Test-Path -LiteralPath $plannerExe)) {
        throw 'Exécutable absent. Compiler avec npm run tauri build -- --no-bundle.'
    }
    Start-Process -FilePath $plannerExe -WorkingDirectory $plannerRoot -WindowStyle Hidden
}
