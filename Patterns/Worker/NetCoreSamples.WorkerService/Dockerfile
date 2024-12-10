#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:9.0 AS base

ENV WorkerOptions__Name=WorkerTwo
ENV WorkerOptions__ServiceDelayInSeconds=2

ENV WorkerTwoOptions__TextToLog="I'm WorkerTwo"

ENV Serilog__MinimumLevel__Default=Debug
ENV Serilog__WriteTo__0__Name=Console
ENV Serilog__Using__0=Serilog.Sinks.Console

USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
# Install clang/zlib1g-dev dependencies for publishing to native
RUN apt-get update \
    && apt-get install -y --no-install-recommends \
    clang zlib1g-dev
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["NetCoreSamples.WorkerService/NetCoreSamples.WorkerService.csproj", "NetCoreSamples.WorkerService/"]
RUN dotnet restore "./NetCoreSamples.WorkerService/./NetCoreSamples.WorkerService.csproj"
COPY . .
WORKDIR "/src/NetCoreSamples.WorkerService"
RUN dotnet build "./NetCoreSamples.WorkerService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./NetCoreSamples.WorkerService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=true

FROM mcr.microsoft.com/dotnet/runtime-deps:9.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["./NetCoreSamples.WorkerService"]