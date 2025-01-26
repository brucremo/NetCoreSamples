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
  - **Broker**: Broker services and related libraries using NATS as a broker.
  - **Caching**: Caching related services and configurations.
  - **Messaging**: Simple messaging services and related libraries using SignalR for live communication between applications. 
- **Database**: Database related projects and scripts for MSSQL.
- **Patterns**: Contains different design patterns and services.
- **Shared**: Shared libraries and domain models.

## Projects

### Broker

- **NetCoreSamples.Broker.Consumer**: Consumer application for broker messages.
- **NetCoreSamples.Broker.Publisher**: Publisher application for broker messages.
- **NetCoreSamples.Broker.Lib**: Library for broker services.

### Caching

- **NetCoreSamples.Caching.Client**: Client application for caching.
- **NetCoreSamples.Caching.Application**: Application layer for caching.
- **NetCoreSamples.Caching.Lib**: Library for caching services.
- **NetCoreSamples.Caching.Persistence**: Persistence layer for caching.

### Messaging

- **NetCoreSamples.Messaging.SignalR**: SignalR API for message transmission.
- **NetCoreSamples.Messaging.Worker**: Worker application to produce/consume messages.
- **NetCoreSamples.Messaging.Lib**: Library for messaging services.

### Database

- **NetCoreSamples.Database**: Database project and related scripts.

### Shared

- **NetCoreSamples.Domain**: Domain models. When this project is built, it will also setup the MSSQL DB as part of its pre-build events.
- **NetCoreSamples.Logging.Lib**: Logging library.

### Patterns

- **EventSourcing**: Event sourcing pattern implementation using [Marten](https://martendb.io/).
- **Worker**: Worker services and related libraries.

