using OpenMES.Domain.Common;

namespace OpenMES.Domain.Hierarchy;

/// <summary>
/// A work station or group of machines performing a specific operation.
/// ISA-95 Level 2 — Work Cell.
/// </summary>
public class WorkCell : AggregateRoot
{
    private readonly List<Machine> _machines = [];

    public Guid Id { get; private set; }
    public Guid LineId { get; private set; }
    public string Code { get; private set; } = default!;
    public string Name { get; private set; } = default!;
    public string? Description { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public IReadOnlyList<Machine> Machines => _machines.AsReadOnly();

    private WorkCell() { }

    public static Result<WorkCell> Create(Guid lineId, string code, string name, string? description = null)
    {
        if (string.IsNullOrWhiteSpace(code))
            return Result.Failure<WorkCell>("Work cell code is required.");

        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure<WorkCell>("Work cell name is required.");

        var workCell = new WorkCell
        {
            Id = Guid.NewGuid(),
            LineId = lineId,
            Code = code.Trim().ToUpper(),
            Name = name.Trim(),
            Description = description?.Trim(),
            CreatedAt = DateTime.UtcNow
        };

        return Result.Success(workCell);
    }

    public Result AddMachine(Machine machine)
    {
        if (_machines.Any(m => m.Code == machine.Code))
            return Result.Failure($"A machine with code '{machine.Code}' already exists.");

        _machines.Add(machine);
        return Result.Success();
    }
}