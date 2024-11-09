docker compose -f ../docker-compose.yml up netcoresamples.caching.db --detach

dotnet tool update -g microsoft.sqlpackage

$MSBUILD = &"${env:ProgramFiles(x86)}\Microsoft Visual Studio\Installer\vswhere.exe" -latest -prerelease -products * -requires Microsoft.Component.MSBuild -find MSBuild\**\Bin\MSBuild.exe

& $MSBUILD .\NetCoreSamples.Caching.Database.sqlproj /P:Configuration=Release 

SqlPackage /Action:Publish /SourceFile:"bin\Release\NetCoreSamples.Caching.Database.dacpac" /TargetConnectionString:"Server=localhost;Initial Catalog=NetCoreSamples.Caching.Database;User ID=sa;Password=P@ssw0rd;TrustServerCertificate=True;Encrypt=True"

docker compose -f ../docker-compose.yml down