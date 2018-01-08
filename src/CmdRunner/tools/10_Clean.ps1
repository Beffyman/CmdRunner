# Deletes temp folders created by dot net like bin, obj, pkg.
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

Write-Host ">Clean" -ForegroundColor Magenta



& "./01_Initialize.ps1" | Out-Host

try{Get-ChildItem -Path $rootDirectory -Include bin -Recurse -Force | Remove-Item -Force -Recurse | Out-Host;}catch{}
Write-Host ">>Folder Wildcard **\bin have been deleted" -ForegroundColor Magenta;

try{Get-ChildItem -Path $rootDirectory -Include obj -Recurse -Force | Remove-Item -Force -Recurse | Out-Host;}catch{}
Write-Host ">>Folder Wildcard **\obj have been deleted" -ForegroundColor Magenta;

try{Get-ChildItem -Path $rootDirectory -Include pkg -Recurse -Force | Remove-Item -Force -Recurse | Out-Host;}catch{}
Write-Host ">>Folder Wildcard **\pkg have been deleted" -ForegroundColor Magenta;



Write-Host "~Clean" -ForegroundColor Magenta