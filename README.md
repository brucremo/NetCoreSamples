# NetCoreSamples

This repository contains various .NET Core samples demonstrating different architectural patterns and practices.

## Getting Started

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/get-started)

### Building the Project

To build the project, run the following command:

```sh
dotnet build NetCoreSamples.sln
```

## Project Structure

- **Architecture**: Contains different architectural patterns and services.
  - **Caching**: Caching related services and configurations.
  - **MetricsCollection**: Services related to metrics collection.
  - **Queueing**: Queueing related services.
  - **Sharding**: Sharding related services.
  - **Vault**: Vault related services and configurations.
- **Database**: Database related projects and scripts.
- **Patterns**: Contains different design patterns and services.
- **Shared**: Shared libraries and domain models.

## Projects

### Caching

- **NetCoreSamples.Caching.Client**: Client application for caching.
- **NetCoreSamples.Caching.Application**: Application layer for caching.
- **NetCoreSamples.Caching.Lib**: Library for caching.
- **NetCoreSamples.Caching.Persistence**: Persistence layer for caching.

### Vault

- **NetCoreSamples.Vault.API**: API for Vault services.
- **NetCoreSamples.Vault.Lib**: Library for Vault services.

### Database

- **NetCoreSamples.Database**: Database project and related scripts.

### Shared

- **NetCoreSamples.Domain**: Domain models. When this project is built, it will also setup the MSSQL DB as part of its pre-build events.
- **NetCoreSamples.Logging.Lib**: Logging library.

### Patterns

- **EventSourcing**: Event sourcing pattern implementation using [Marten](https://martendb.io/).
- **Worker**: Worker services and related libraries.

