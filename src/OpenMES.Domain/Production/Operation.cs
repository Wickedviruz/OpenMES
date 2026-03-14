using OpenMES.Domain.Common;

namespace OpenMES.Domain.Production;

/// <summary>
/// A single step in a production process.
/// Belongs to a Routing. Performed at a specific WorkCell.
/// </summary>
public class Operation : AggregateRoot
{
    public Guid Id { get; private set; }
    public string Code { get; private set; } = default!;
    public string Name { get; private set; } = default!;
    public string? Description { get; private set; }

    /// <summary>The WorkCell where this operation is performed.</summary>
    public Guid WorkCellId { get; private set; }

    /// <summary>Expected cycle time in seconds.</summary>
    public int? CycleTimeSeconds { get; private set; }

    public DateTime CreatedAt { get; private set; }

    private Operation() { }

    public static Result<Operation> Create(
        string code,
        string name,
        Guid workCellId,
        int? cycleTimeSeconds = null,
        string? description = null)
    {
        if (string.IsNullOrWhiteSpace(code))
            return Result.Failure<Operation>("Operation code is required.");

        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure<Operation>("Operation name is required.");

        if (cycleTimeSeconds.HasValue && cycleTimeSeconds.Value <= 0)
            return Result.Failure<Operation>("Cycle time must be greater than zero.");

        var operation = new Operation
        {
            Id = Guid.NewGuid(),
            Code = code.Trim().ToUpper(),
            Name = name.Trim(),
            Description = description?.Trim(),
            WorkCellId = workCellId,
            CycleTimeSeconds = cycleTimeSeconds,
            CreatedAt = DateTime.UtcNow
        };

        return Result.Success(operation);
    }
}