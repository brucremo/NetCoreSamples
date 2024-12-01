docker compose -f "$PSScriptRoot/docker-compose.yml" up --detach

dotnet tool update -g microsoft.sqlpackage

$MSBUILD = &"${env:ProgramFiles(x86)}\Microsoft Visual Studio\Installer\vswhere.exe" -latest -prerelease -products * -requires Microsoft.Component.MSBuild -find MSBuild\**\Bin\MSBuild.exe

& $MSBUILD "$PSScriptRoot\NetCoreSamples.Database.sqlproj" /P:Configuration=Release 

SqlPackage /Action:Publish /SourceFile:"$PSScriptRoot\bin\Release\NetCoreSamples.Database.dacpac" /TargetConnectionString:"Server=localhost;Initial Catalog=NetCoreSamples.Database;User ID=sa;Password=P@ssw0rd;TrustServerCertificate=True;Encrypt=True"