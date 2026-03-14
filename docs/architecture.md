# OpenMES Architecture

## Overview

OpenMES is an open-source backend platform for building **Manufacturing Execution Systems (MES)**.

The goal of the project is to provide a **modular, extensible core platform** that connects machines, production systems, and enterprise software.

Most MES systems today are proprietary, expensive, and difficult to integrate with modern software stacks. OpenMES aims to provide a **modern, developer-friendly alternative**.

The platform focuses on:

- production tracking
- machine states
- production orders
- traceability
- machine data ingestion
- industrial integrations

OpenMES is designed to act as the **core backend for factory systems**.

Possible integrations include:

- PLC systems
- SCADA systems
- ERP systems
- IoT platforms
- machine data collectors
- dashboards
- data historians

---

# Project Goals

OpenMES is designed to:

- provide a clean MES backend architecture
- enable factories to build custom MES solutions
- support industrial automation integration
- provide modern APIs for manufacturing systems
- enable developers to extend MES capabilities

The system is designed to be:

- modular
- scalable
- open source
- easy to extend
- deployable on-premise or in the cloud

---

# High-Level Architecture

OpenMES follows the **Clean Architecture pattern**.

The system is divided into multiple layers to ensure separation of concerns.

Client Applications
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

Each layer has a clearly defined responsibility.

---

# Repository Structure

openmes/
├── src/
│ ├── OpenMES.Domain/
│ ├── OpenMES.Application/
│ ├── OpenMES.Infrastructure/
│ └── OpenMES.Api/
│
├── tests/
│ ├── OpenMES.Domain.Tests/
│ └── OpenMES.Application.Tests/
│
├── docs/
│ └── architecture.md
│
├── docker/
│ └── docker-compose.yml
│
├── .github/
│ └── workflows/
│ └── build.yml
│
├── OpenMES.sln
├── README.md
└── LICENSE



---

# Layer Responsibilities

## Domain Layer

The **Domain layer** contains the core business logic of the MES system.

This layer must be completely independent from:

- databases
- APIs
- frameworks
- infrastructure

The domain contains:

- Entities
- Value Objects
- Domain Services
- Business Rules

Example entities:

- Machine
- ProductionOrder
- WorkCenter
- Operation
- MaterialLot
- MachineState
- Operator

Example responsibilities:

- machine state transitions
- production order lifecycle
- traceability rules
- validation logic

The domain layer represents the **core manufacturing logic**.

---

## Application Layer

The **Application layer** contains the use cases of the system.

This layer orchestrates domain logic and coordinates operations.

Responsibilities include:

- application services
- use cases
- DTOs
- command handling
- query handling

Examples:

- StartProductionOrder
- CompleteProductionOrder
- RecordMachineState
- RegisterMaterialConsumption

The application layer acts as the **service layer of the system**.

---

## Infrastructure Layer

The **Infrastructure layer** provides technical implementations.

This includes:

- database access
- repositories
- external integrations
- messaging
- file storage

Examples:

- PostgreSQL repositories
- OPC-UA client integration
- MQTT communication
- caching
- logging

The infrastructure layer depends on the application and domain layers.

---

## API Layer

The **API layer** exposes the functionality of OpenMES through HTTP APIs.

This layer is implemented using **:contentReference[oaicite:0]{index=0}**.

Responsibilities include:

- REST API endpoints
- authentication
- request validation
- API documentation
- client communication

Example endpoints:

GET /machines
GET /production-orders
POST /production-orders/start
POST /machines/state

This layer allows external systems to interact with OpenMES.

---

# Testing Strategy

Testing is critical for maintaining reliability in industrial systems.

The project includes separate test projects:

tests/
├ OpenMES.Domain.Tests
└ OpenMES.Application.Tests


Domain tests validate:

- business rules
- machine state logic
- production flow

Application tests validate:

- use cases
- service logic
- command handling

Testing frameworks may include:

- xUnit
- FluentAssertions
- Moq

---

# Continuous Integration

The repository includes CI using **:contentReference[oaicite:1]{index=1}**.

The pipeline performs:

1. dependency restore
2. project build
3. automated tests

CI runs on:

- every push
- every pull request

This ensures that all contributions maintain system integrity.

---

# Containerized Development

OpenMES supports containerized development using **:contentReference[oaicite:2]{index=2}**.

The project includes a docker compose setup.

Example services:

- API service
- database
- optional messaging systems

Database used:

- **:contentReference[oaicite:3]{index=3}**

This allows developers to quickly start the environment locally.

---

# Future System Modules

OpenMES is designed to grow with additional modules.

Planned capabilities may include:

### Machine Integration

- OPC-UA client
- Modbus communication
- MQTT device messaging

### Production Management

- production orders
- job tracking
- scheduling

### Traceability

- material genealogy
- batch tracking
- product history

### Machine Monitoring

- machine state tracking
- downtime analysis
- OEE calculation

### Data Historian

- time series data
- machine telemetry
- production metrics

---

# Long-Term Vision

The long-term goal of OpenMES is to provide an open platform for industrial software development.

Factories should be able to:

- build custom MES solutions
- integrate with existing systems
- extend functionality with plugins
- deploy on-premise or in the cloud

OpenMES aims to become a **foundation for open industrial software**.

---

# Contributing

Contributions are welcome.

Areas where help is valuable:

- industrial protocol integration
- production modeling
- API design
- testing
- documentation

See `CONTRIBUTING.md` for contribution guidelines.

---

# License

OpenMES is licensed under the MIT License.

This allows both commercial and non-commercial use of the platform.