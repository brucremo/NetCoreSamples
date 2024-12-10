# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/runtime:9.0 AS base
USER $APP_UID
WORKDIR /app

ENV Serilog__MinimumLevel__Default=Debug
ENV Serilog__WriteTo__0__Name=Console
ENV Serilog__Using__0=Serilog.Sinks.Console

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["NetCoreSamples.Messaging.Worker/NetCoreSamples.Messaging.Worker.csproj", "NetCoreSamples.Messaging.Worker/"]
RUN dotnet restore "./NetCoreSamples.Messaging.Worker/NetCoreSamples.Messaging.Worker.csproj"
COPY . .
WORKDIR "/src/NetCoreSamples.Messaging.Worker"
RUN dotnet build "./NetCoreSamples.Messaging.Worker.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./NetCoreSamples.Messaging.Worker.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NetCoreSamples.Messaging.Worker.dll"]