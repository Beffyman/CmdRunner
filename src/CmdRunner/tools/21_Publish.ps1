# Publishes the electron app into the obj/desktop folder
##

Param(
	[string]$platform = "win"
)

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

Write-Host ">Publish" -ForegroundColor Magenta;


& "./01_Initialize.ps1" | Out-Host;
& "./20_Build.ps1" | Out-Host;

Push-Location $projectDirectory -StackName "Publish";
dotnet electronize build $platform | Out-Host;
Pop-Location -StackName "Publish";


Write-Host "~Publish" -ForegroundColor Magenta;