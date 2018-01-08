# Install all dependencies, you should only need to run this once unless there are changes.
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

Write-Host ">Install-Dependencies" -ForegroundColor Magenta

if((Get-Command choco -ErrorAction SilentlyContinue) -eq $null){
	Set-ExecutionPolicy Bypass -Scope Process -Force; iex ((New-Object System.Net.WebClient).DownloadString('https://chocolatey.org/install.ps1'))
}

refreshenv;

choco upgrade nodejs --version 8.6.0 --confirm;
choco upgrade dotnetcore-sdk --confirm;
choco upgrade dotnetcore-windowshosting --confirm;

Write-Host "~Install-Dependencies" -ForegroundColor Magenta