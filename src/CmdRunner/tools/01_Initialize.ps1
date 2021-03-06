# Configures the default variables that every script uses, only runs once per script batch
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


Write-Host ">Initialize" -ForegroundColor Magenta

$Global:initialized = $false;

if($Global:initialized -eq $false){
	$Global:initialized = $true;

	#Clean
	$Global:rootDirectory = "./../..";

	#Build
	$Global:configuration = "Debug"


	#Publish
	$Global:projectName = "CmdRunner";
	$Global:projectDirectory = "./..";
	$Global:projectPath = "$projectDirectory/$projectName.csproj";

	#Start
	$Global:publishFolder = "./../obj/desktop/";

}

Write-Host "~Initialize" -ForegroundColor Magenta