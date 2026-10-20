@echo off
powershell.exe -NoExit -NoProfile -Command ". '%~dp0Use-ReaTools.ps1'; Set-Location -LiteralPath '%~dp0..'"
