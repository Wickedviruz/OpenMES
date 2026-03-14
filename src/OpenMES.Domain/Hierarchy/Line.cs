using OpenMES.Domain.Common;

namespace OpenMES.Domain.Hierarchy;

/// <summary>
/// A production line within an Area. E.g. "Line 1", "Line 2".
/// ISA-95 Level 3 — Production Line.
/// </summary>
public class Line : AggregateRoot
{
    private readonly List<WorkCell> _workCells = [];

    public Guid Id { get; private set; }
    public Guid AreaId { get; private set; }
    public string Code { get; private set; } = default!;
    public string Name { get; private set; } = default!;
    public string? Description { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public IReadOnlyList<WorkCell> WorkCells => _workCells.AsReadOnly();

    private Line() { }

    public static Result<Line> Create(Guid areaId, string code, string name, string? description = null)
    {
        if (string.IsNullOrWhiteSpace(code))
            return Result.Failure<Line>("Line code is required.");

        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure<Line>("Line name is required.");

        var line = new Line
        {
            Id = Guid.NewGuid(),
            AreaId = areaId,
            Code = code.Trim().ToUpper(),
            Name = name.Trim(),
            Description = description?.Trim(),
            CreatedAt = DateTime.UtcNow
        };

        return Result.Success(line);
    }

    public Result AddWorkCell(WorkCell workCell)
    {
        if (_workCells.Any(w => w.Code == workCell.Code))
            return Result.Failure($"A work cell with code '{workCell.Code}' already exists.");

        _workCells.Add(workCell);
        return Result.Success();
    }
}