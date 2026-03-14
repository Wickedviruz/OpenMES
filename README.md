# OpenMES

OpenMES is an open-source backend platform for building **Manufacturing Execution Systems (MES)**.

The goal of OpenMES is to provide a **modern, modular, developer-friendly MES core** that can connect machines, production systems, and enterprise software.

Most MES solutions today are closed, expensive, and difficult to extend. OpenMES aims to provide a **flexible open platform** for industrial software development.

---

## What is MES?

A **Manufacturing Execution System (MES)** sits between factory floor machines and enterprise systems like ERP.

Typical responsibilities include:

- Production order management
- Machine state tracking
- Traceability
- Material consumption tracking
- Operator interaction
- Production performance metrics
- Integration with PLC systems

OpenMES provides the **core backend services** required to build these capabilities.

---

## Project Goals

OpenMES is designed to:

- provide a clean MES backend architecture
- support industrial automation integrations
- enable developers to build custom MES solutions
- expose modern APIs for manufacturing systems
- be deployable on-premise or in the cloud

The system focuses on:

- reliability
- modularity
- scalability
- extensibility

---

## Architecture

OpenMES follows **Clean Architecture principles**.

```
Client Apps
     │
     ▼
 API Layer
     │
     ▼
Application Layer
     │
     ▼
 Domain Layer
     │
     ▼
Infrastructure Layer
```

Each layer has a specific responsibility.

See [`docs/architecture.md`](docs/architecture.md) for full architecture documentation.

---

## Repository Structure

```
openmes/
├── src/
│   ├── OpenMES.Domain
│   ├── OpenMES.Application
│   ├── OpenMES.Infrastructure
│   └── OpenMES.Api
│
├── tests/
│   ├── OpenMES.Domain.Tests
│   └── OpenMES.Application.Tests
│
├── docs/
│   └── architecture.md
│
├── docker/
│   └── docker-compose.yml
│
├── .github/
│   └── workflows/
│       └── build.yml
│
├── OpenMES.sln
└── README.md
```

---

## Technology Stack

Core technologies currently used:

- **.NET**
- **ASP.NET Core**
- **PostgreSQL**
- **Docker**

Future integrations may include:

- OPC-UA
- Modbus
- MQTT
- Industrial IoT protocols

---

## Getting Started

Clone the repository:

```bash
git clone https://github.com/YOURNAME/openmes.git
cd openmes
```

Build the solution:

```bash
dotnet build
```

Run the API:

```bash
dotnet run --project src/OpenMES.Api
```

### Development Environment

A containerized development setup is available. Run services using Docker:

```bash
docker-compose up
```

This will start required services such as:

- database
- API services

---

## Roadmap

### Phase 1 — Core MES Model

- Machine model
- Production orders
- Work centers
- Operators

### Phase 2 — Production Execution

- Job execution
- Material tracking
- Machine states

### Phase 3 — Industrial Integration

- OPC-UA connectors
- PLC integration
- Machine telemetry ingestion

### Phase 4 — Analytics

- OEE calculation
- Downtime analysis
- Production metrics

---

## Why OpenMES?

Factories increasingly need custom digital solutions. OpenMES aims to provide the foundation for:

- Custom MES platforms
- Factory digitalization
- Industrial data platforms
- Manufacturing analytics

---

## Contributing

Contributions are welcome. If you'd like to help improve OpenMES, please read [`CONTRIBUTING.md`](CONTRIBUTING.md).

This document explains the development workflow and guidelines.

---

## License

OpenMES is licensed under the **MIT License**.

This allows both commercial and non-commercial usage.

---

## Vision

The long-term goal is to build an open platform for industrial software development.

OpenMES should make it easier for developers, system integrators, and manufacturers to build modern factory software.