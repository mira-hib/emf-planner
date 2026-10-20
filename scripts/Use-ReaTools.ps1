# Dot-source this file to use the portable toolchain in the current PowerShell.
$reaProjectRoot = Split-Path -Parent $PSScriptRoot
$reaToolsRoot = Join-Path $reaProjectRoot '.tools'
$reaManifestPath = Join-Path $reaToolsRoot 'manifest.json'
if (-not (Test-Path -LiteralPath $reaManifestPath)) {
    throw 'Toolchain missing. Run scripts/install_rea_tools.py first.'
}
$reaManifest = Get-Content -Raw -LiteralPath $reaManifestPath | ConvertFrom-Json
if (-not $reaManifest.paths) {
    throw 'Toolchain installation has not completed yet.'
}
$env:JAVA_HOME = $reaManifest.paths.java
$env:GHIDRA_INSTALL_DIR = $reaManifest.paths.ghidra
$env:DOTNET_ROOT = $reaManifest.paths.dotnet
$env:DOTNET_CLI_HOME = Join-Path $reaToolsRoot 'state\dotnet'
$env:DOTNET_CLI_TELEMETRY_OPTOUT = '1'
$env:DOTNET_SKIP_FIRST_TIME_EXPERIENCE = '1'
$env:DOTNET_GENERATE_ASPNET_CERTIFICATE = 'false'
$env:REA_ILSPY_CMD_PATH = Join-Path $reaToolsRoot 'dotnet-tools\ilspycmd.exe'
$env:NUGET_PACKAGES = Join-Path $reaToolsRoot 'cache\nuget'
$reaPathEntries = @(
    $reaManifest.paths.node
    (Join-Path $reaToolsRoot 'rea\node_modules\.bin')
    $reaManifest.paths.dotnet
    (Join-Path $reaToolsRoot 'dotnet-tools')
    (Split-Path -Parent $reaManifest.paths.python)
    (Join-Path $reaManifest.paths.java 'bin')
    (Join-Path $reaToolsRoot 'innoextract')
    'C:\Program Files\WinRAR'
)
$env:PATH = ($reaPathEntries + @($env:PATH)) -join ';'
