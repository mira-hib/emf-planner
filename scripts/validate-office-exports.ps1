$ErrorActionPreference = 'Stop'
$taskRoot = Split-Path -Parent $PSScriptRoot
$taskOutput = Join-Path $taskRoot 'test validation/2026-10-25/office-exports'
[void](New-Item -ItemType Directory -Force $taskOutput)
$wordInput = Join-Path $taskOutput 'modele # accents.dot'
$wordOutput = Join-Path $taskOutput 'modele-converti.docx'
$excelInput = Join-Path $taskOutput 'points # test.xlt'
$excelOutput = Join-Path $taskOutput 'points-convertis.xlsx'
$officeInstance = $null
$documentInstance = $null
try {
    Write-Output 'Word COM initialization started'
    $officeInstance = New-Object -ComObject 'Word.Application'
    Write-Output 'Word COM initialized'
    $officeInstance.Visible = $false
    $officeInstance.DisplayAlerts = 0
    $officeInstance.AutomationSecurity = 3
    $documentInstance = $officeInstance.Documents.Add()
    Write-Output 'Word document created'
    $documentInstance.Content.Text = 'Native template {ProjectName} & <text> "quotes"'
    $documentInstance.SaveAs2($wordInput, 1)
} finally {
    if ($documentInstance) { $documentInstance.Close($false); [void][Runtime.InteropServices.Marshal]::FinalReleaseComObject($documentInstance) }
    if ($officeInstance) { $officeInstance.Quit(); [void][Runtime.InteropServices.Marshal]::FinalReleaseComObject($officeInstance) }
}
& (Join-Path $taskRoot 'src-tauri/src/office-convert.ps1') -InputPath $wordInput -OutputPath $wordOutput -Application Word
$officeInstance = $null
$documentInstance = $null
try {
    $officeInstance = New-Object -ComObject 'Excel.Application'
    $officeInstance.Visible = $false
    $officeInstance.DisplayAlerts = $false
    $officeInstance.AutomationSecurity = 3
    $documentInstance = $officeInstance.Workbooks.Add()
    $sheet = $documentInstance.Worksheets.Item(1)
    $sheet.Cells.Item(1,1) = 'Name'
    $sheet.Cells.Item(1,2) = 'Power W'
    $sheet.Cells.Item(2,1) = 'Example & <Unicode>'
    $sheet.Cells.Item(2,2) = 12.05
    $documentInstance.SaveAs($excelInput, 17)
    [void][Runtime.InteropServices.Marshal]::FinalReleaseComObject($sheet)
} finally {
    if ($documentInstance) { $documentInstance.Close($false); [void][Runtime.InteropServices.Marshal]::FinalReleaseComObject($documentInstance) }
    if ($officeInstance) { $officeInstance.Quit(); [void][Runtime.InteropServices.Marshal]::FinalReleaseComObject($officeInstance) }
}
& (Join-Path $taskRoot 'src-tauri/src/office-convert.ps1') -InputPath $excelInput -OutputPath $excelOutput -Application Excel
Get-Item -LiteralPath $wordInput,$wordOutput,$excelInput,$excelOutput | Select-Object Name,Length | ConvertTo-Json | Set-Content -LiteralPath (Join-Path $taskOutput 'conversion-files.json') -Encoding utf8
Write-Output 'Word .dot and Excel .xlt conversions completed. Inspect OOXML values separately.'
