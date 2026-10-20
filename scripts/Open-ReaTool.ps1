param(
    [Parameter(Mandatory = $true)]
    [ValidateSet('ghidra', 'x64dbg', 'dnspy', 'extremedumper', 'pe-bear', 'die')]
    [string]$Tool
)
. (Join-Path $PSScriptRoot 'Use-ReaTools.ps1')
$reaExecutablePatterns = @{
    'x64dbg' = 'x64dbg.exe'
    'dnspy' = 'dnSpy.exe'
    'extremedumper' = 'ExtremeDumper.exe'
    'pe-bear' = 'PE-bear.exe'
    'die' = 'die.exe'
}
if ($Tool -eq 'ghidra') {
    $reaExecutable = Join-Path $reaManifest.paths.ghidra 'ghidraRun.bat'
} else {
    $reaToolFolder = Join-Path $reaToolsRoot $Tool
    $reaCandidates = @(Get-ChildItem -LiteralPath $reaToolFolder -Recurse -File -Filter $reaExecutablePatterns[$Tool])
    if ($reaCandidates.Count -ne 1) {
        throw "Expected one executable for $Tool; found $($reaCandidates.Count)."
    }
    $reaExecutable = $reaCandidates[0].FullName
}
Start-Process -FilePath $reaExecutable -WorkingDirectory (Split-Path -Parent $reaExecutable)
