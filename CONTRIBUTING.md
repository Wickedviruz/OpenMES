# Contributing to OpenMES

Thank you for your interest in contributing to OpenMES.

---

## Getting Started

Restore dependencies:

```bash
dotnet restore
```

Build the solution:

```bash
dotnet build
```

Run tests:

```bash
dotnet test
```

---

## Project Architecture

OpenMES follows **Clean Architecture**. The codebase is divided into the following layers:

### Domain

Contains the core business logic and manufacturing models.

Examples:

- `Machine`
- `ProductionOrder`
- `WorkCenter`
- `Operator`
- `MachineState`

The domain layer must **not** depend on infrastructure or frameworks.

### Application

Contains application use cases.

Examples:

- `StartProductionOrder`
- `RecordMachineState`
- `RegisterMaterialConsumption`

This layer orchestrates domain logic.

### Infrastructure

Contains technical implementations such as:

- database repositories
- external system integrations
- messaging systems
- file storage

### API

Exposes system functionality via HTTP APIs. Responsibilities include:

- controllers
- request validation
- authentication
- API documentation

---

## Coding Guidelines

General principles:

- follow SOLID principles
- keep domain logic inside the Domain layer
- avoid leaking infrastructure concerns into domain code
- write clear, maintainable code

Naming conventions:

- `PascalCase` for classes
- `camelCase` for variables
- meaningful method names

---

## Testing

All business logic should be covered by tests. Test projects are located in:

```
tests/
```

Testing types include:

- unit tests for domain logic
- application service tests
- integration tests (future)

---

## Pull Request Process

1. Fork the repository
2. Create a new branch
3. Implement your changes
4. Write or update tests
5. Ensure the project builds
6. Submit a pull request

Branch naming examples:

```
feature/machine-model
feature/opcua-integration
fix/machine-state-bug
```

---

## Code Review

Pull requests will be reviewed to ensure:

- code quality
- architecture consistency
- test coverage
- documentation updates

Constructive discussion is encouraged.

---

## Issue Guidelines

When creating issues, please include:

- clear description
- reproduction steps (if bug)
- expected behavior
- actual behavior

If proposing a feature, explain:

- problem being solved
- proposed approach
- possible alternatives

---

## Industrial Integrations

Future contributions may include integrations with:

- OPC-UA
- Modbus
- MQTT
- PLC communication libraries

Industrial integrations should be implemented in the **Infrastructure** layer.

---

## Communication

Open discussion and collaboration are encouraged.

If you are unsure how to approach a change, open an issue first to discuss the design.

---

## License

By contributing to OpenMES, you agree that your contributions will be licensed under the project's **MIT License**.