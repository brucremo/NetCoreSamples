# Check if .NET 8.0.15 Runtime is installed for sqlpackage
$dotnet8Installed = & dotnet --list-runtimes | Select-String "Microsoft\.NETCore\.App 8\.0\.15"
if (-not $dotnet8Installed) {
    Write-Host ".NET 8.0.15 Runtime not found. Installing via winget..."
    winget install --id Microsoft.DotNet.Runtime.8 --version 8.0.15
} else {
    Write-Host ".NET 8.0.15 Runtime is already installed."
}

# Check if SqlPackage tool is installed and up to date
$toolList = & dotnet tool list -g | Select-String "microsoft.sqlpackage"
if ($toolList) {
    Write-Host "SqlPackage tool is already installed. Attempting to update..."
    dotnet tool update -g microsoft.sqlpackage
    if ($LASTEXITCODE -ne 0) {
        Write-Error "Failed to update SqlPackage tool."
        exit 1
    }
} else {
    Write-Host "SqlPackage tool not found. Installing..."
    dotnet tool install -g microsoft.sqlpackage
    if ($LASTEXITCODE -ne 0) {
        Write-Error "Could not install SqlPackage. Check your .NET installation."
        exit 1
    }
}

# Start SQL Server container
docker compose -f "$PSScriptRoot/docker-compose.yml" up --detach

# Find MSBuild path
$MSBUILD = &"${env:ProgramFiles(x86)}\Microsoft Visual Studio\Installer\vswhere.exe" -latest -prerelease -products * -requires Microsoft.Component.MSBuild -find MSBuild\**\Bin\MSBuild.exe
Write-Host "Using MSBuild at: $MSBUILD"

# Build project
& $MSBUILD "$PSScriptRoot\NetCoreSamples.Database.sqlproj" /P:Configuration=Release 
if ($LASTEXITCODE -ne 0) {
    Write-Error "Build failed."
    exit 1
}

# Ensure dotnet tools path is in PATH
$env:PATH += ";$env:USERPROFILE\.dotnet\tools"

# Use full path to SqlPackage
$SqlPackagePath = "$env:USERPROFILE\.dotnet\tools\sqlpackage.exe"
& $SqlPackagePath /Action:Publish /SourceFile:"$PSScriptRoot\bin\Release\NetCoreSamples.Database.dacpac" /TargetConnectionString:"Server=localhost;Initial Catalog=NetCoreSamples.Database;User ID=sa;Password=P@ssw0rd;TrustServerCertificate=True;Encrypt=True"
if ($LASTEXITCODE -ne 0) {
    Write-Error "SqlPackage publish failed."
    exit 1
}
