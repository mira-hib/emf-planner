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
    $plannerCandidates = @(
        'src-tauri\target\release\emf-planner.exe'
        'src-tauri\target\x86_64-pc-windows-msvc\release\emf-planner.exe'
    ) | ForEach-Object {
        $plannerCandidate = Join-Path $plannerRoot $_
        if (Test-Path -LiteralPath $plannerCandidate -PathType Leaf) {
            Get-Item -LiteralPath $plannerCandidate
        }
    }
    $plannerExecutable = $plannerCandidates | Sort-Object LastWriteTimeUtc -Descending | Select-Object -First 1
    if (-not $plannerExecutable) {
        throw 'Exécutable absent. Compiler avec npm run tauri build -- --no-bundle.'
    }
    Start-Process -FilePath $plannerExecutable.FullName -WorkingDirectory $plannerRoot -WindowStyle Hidden
}
