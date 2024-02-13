#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base

ENV WorkerBaseOptions__Worker=WorkerOne

USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
# Install clang/zlib1g-dev dependencies for publishing to native
RUN apt-get update \
    && apt-get install -y --no-install-recommends \
    clang zlib1g-dev
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["NetCoreSamples.Worker/NetCoreSamples.Worker.csproj", "NetCoreSamples.Worker/"]
COPY ["NetCoreSamples.Worker.Lib/NetCoreSamples.Worker.Lib.csproj", "NetCoreSamples.Worker.Lib/"]
RUN dotnet restore "./NetCoreSamples.Worker/./NetCoreSamples.Worker.csproj"
COPY . .
WORKDIR "/src/NetCoreSamples.Worker"
RUN dotnet build "./NetCoreSamples.Worker.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./NetCoreSamples.Worker.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=true

FROM mcr.microsoft.com/dotnet/runtime-deps:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["./NetCoreSamples.Worker"]