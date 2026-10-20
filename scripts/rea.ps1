# Run REA with its supported Node.js and the local analysis dependencies.
. (Join-Path $PSScriptRoot 'Use-ReaTools.ps1')
& (Join-Path $reaManifest.paths.node 'node.exe') $reaManifest.paths.rea_entry @args
exit $LASTEXITCODE
