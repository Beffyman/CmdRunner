# Completely removes all temp files within the solution, please close VS before running this.
##

# Make so the script will stop when it hits an error.
$ErrorActionPreference = "Stop"

# RunAsAdministrator check.
If (-NOT ([Security.Principal.WindowsPrincipal][Security.Principal.WindowsIdentity]::GetCurrent()).IsInRole([Security.Principal.WindowsBuiltInRole] "Administrator")) {
	echo "This script needs to be RunAsAdministrator."
	break
}

# Get the executing directory and set it to the current directory.
$scriptBin = ""
Try { $scriptBin = "$(Split-Path -Parent $MyInvocation.MyCommand.Definition)" } Catch {}
If ([string]::IsNullOrEmpty($scriptBin)) { $scriptBin = $pwd }
Set-Location $scriptBin


#############################################################################

Write-Host "->Nuke" -ForegroundColor Magenta



& "./01_Initialize.ps1" | Out-Host
& "./10_Clean.ps1" | Out-Host

try{Get-ChildItem -Path $rootDirectory -Include packages -Recurse -Force | Remove-Item -Force -Recurse | Out-Host;}catch{}
Write-Host ">>Folder Wildcard **\packages have been deleted" -ForegroundColor Magenta;

try{Get-ChildItem -Path $rootDirectory -Include .vs -Recurse -Force | Remove-Item -Force -Recurse | Out-Host;}catch{}
Write-Host ">>Folder Wildcard **\.vs have been deleted" -ForegroundColor Magenta;

try{Get-ChildItem -Path $rootDirectory -Include *.csproj.user -Recurse -Force | Remove-Item -Force -Recurse | Out-Host;}catch{}
Write-Host ">>Folder Wildcard **\*.csproj.user have been deleted" -ForegroundColor Magenta;

try{Get-ChildItem -Path $rootDirectory -Include "lib" -Recurse -Force | Where-Object {$_.Parent.Name -eq "wwwroot"} | Remove-Item -Force -Recurse | Out-Host;}catch{}
Write-Host ">>Folder Wildcard **\wwwroot\lib have been deleted" -ForegroundColor Magenta;

try{Get-ChildItem -Path $rootDirectory -Include "min" -Recurse -Force | Where-Object {$_.Parent.Name -eq "wwwroot"} | Remove-Item -Force -Recurse | Out-Host;}catch{}
Write-Host ">>Folder Wildcard **\wwwroot\min have been deleted" -ForegroundColor Magenta;



Write-Host "~Nuke" -ForegroundColor Magenta