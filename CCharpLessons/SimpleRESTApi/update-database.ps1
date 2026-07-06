if (-not (Get-Command dotnet-ef -ErrorAction SilentlyContinue)) {
	Write-Host "dotnet-ef not found. Install it with: dotnet tool install --global dotnet-ef"
	exit 1
}

Push-Location $PSScriptRoot
try {
	dotnet ef database update --project . --startup-project .
} finally {
	Pop-Location
}
