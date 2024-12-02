# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.
FROM mcr.microsoft.com/dotnet/runtime:9.0 AS base

ENV Serilog__MinimumLevel__Default=Information
ENV Serilog__MinimumLevel__Override__Microsoft=Information
ENV Serilog__MinimumLevel__Override__System=Information

ENV Serilog__Using__0=Serilog.Sinks.Console
ENV Serilog__WriteTo__0__Name=Console

USER $APP_UID
WORKDIR /app


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["NetCoreSamples.GenericWorker/NetCoreSamples.GenericWorker.csproj", "NetCoreSamples.GenericWorker/"]
RUN dotnet restore "./NetCoreSamples.GenericWorker/NetCoreSamples.GenericWorker.csproj"
COPY . .
WORKDIR "/src/NetCoreSamples.GenericWorker"
RUN dotnet build "./NetCoreSamples.GenericWorker.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./NetCoreSamples.GenericWorker.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NetCoreSamples.GenericWorker.dll"]