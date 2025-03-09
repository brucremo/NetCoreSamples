#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

ENV Serilog__MinimumLevel__Default=Debug
ENV Serilog__WriteTo__0__Name=Console
ENV Serilog__Using__0=Serilog.Sinks.Console

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["NetCoreSamples.Caching.Client/NetCoreSamples.Caching.Client.csproj", "NetCoreSamples.Caching.Client/"]
RUN dotnet restore "./NetCoreSamples.Caching.Client/./NetCoreSamples.Caching.Client.csproj"
COPY . .
WORKDIR "/src/NetCoreSamples.Caching.Client"
RUN dotnet build "./NetCoreSamples.Caching.Client.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./NetCoreSamples.Caching.Client.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NetCoreSamples.Caching.Client.dll"]