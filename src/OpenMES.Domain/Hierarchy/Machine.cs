using OpenMES.Domain.Common;

namespace OpenMES.Domain.Hierarchy;

/// <summary>
/// An individual piece of equipment within a WorkCell.
/// ISA-95 Level 1 — Equipment.
/// Emits MachineEvents. Has observable status.
/// </summary>
public class Machine : AggregateRoot
{
    public Guid Id { get; private set; }
    public Guid WorkCellId { get; private set; }
    public string Code { get; private set; } = default!;
    public string Name { get; private set; } = default!;
    public string? Description { get; private set; }
    public MachineStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? LastStatusChangedAt { get; private set; }

    private Machine() { }

    public static Result<Machine> Create(Guid workCellId, string code, string name, string? description = null)
    {
        if (string.IsNullOrWhiteSpace(code))
            return Result.Failure<Machine>("Machine code is required.");

        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure<Machine>("Machine name is required.");

        var machine = new Machine
        {
            Id = Guid.NewGuid(),
            WorkCellId = workCellId,
            Code = code.Trim().ToUpper(),
            Name = name.Trim(),
            Description = description?.Trim(),
            Status = MachineStatus.Unknown,
            CreatedAt = DateTime.UtcNow
        };

        return Result.Success(machine);
    }

    public Result ChangeStatus(MachineStatus newStatus)
    {
        if (Status == newStatus)
            return Result.Failure($"Machine is already in '{newStatus}' status.");

        var previous = Status;
        Status = newStatus;
        LastStatusChangedAt = DateTime.UtcNow;

        AddDomainEvent(new MachineStatusChanged(Id, previous, newStatus, LastStatusChangedAt.Value));
        return Result.Success();
    }
}

public enum MachineStatus
{
    Unknown,
    Idle,
    Running,
    Down,
    Maintenance
}

// Domain event emitted when machine status changes
public record MachineStatusChanged(
    Guid MachineId,
    MachineStatus Previous,
    MachineStatus Current,
    DateTime ChangedAt) : DomainEvent;