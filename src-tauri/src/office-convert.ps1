param([Parameter(Mandatory=$true)][string]$InputPath,
      [Parameter(Mandatory=$true)][string]$OutputPath,
      [ValidateSet('Word','Excel')][string]$Application)
$ErrorActionPreference = 'Stop'
$officeInstance = $null
$documentInstance = $null
$originalUpdateLinks = $null
try {
    # A private automation instance; never attach to the user's active documents.
    $officeInstance = New-Object -ComObject "$Application.Application"
    $officeInstance.Visible = $false
    $officeInstance.AutomationSecurity = 3 # msoAutomationSecurityForceDisable
    if ($Application -eq 'Excel') {
        $officeInstance.DisplayAlerts = $false
        $officeInstance.AskToUpdateLinks = $false
        $documentInstance = $officeInstance.Workbooks.Open($InputPath, 0, $true)
        $documentInstance.SaveAs($OutputPath, 51) # xlOpenXMLWorkbook, no macros
    } else {
        $officeInstance.DisplayAlerts = 0
        $originalUpdateLinks = $officeInstance.Options.UpdateLinksAtOpen
        $officeInstance.Options.UpdateLinksAtOpen = $false
        $documentInstance = $officeInstance.Documents.Open($InputPath, $false, $true, $false)
        $documentInstance.SaveAs2($OutputPath, 12) # wdFormatXMLDocument, no macros
    }
} finally {
    if ($documentInstance) {
        $documentInstance.Close($false)
        [void][Runtime.InteropServices.Marshal]::FinalReleaseComObject($documentInstance)
    }
    if ($officeInstance) {
        if ($Application -eq 'Word' -and $null -ne $originalUpdateLinks) {
            $officeInstance.Options.UpdateLinksAtOpen = $originalUpdateLinks
        }
        $officeInstance.Quit()
        [void][Runtime.InteropServices.Marshal]::FinalReleaseComObject($officeInstance)
    }
    [GC]::Collect()
    [GC]::WaitForPendingFinalizers()
}
