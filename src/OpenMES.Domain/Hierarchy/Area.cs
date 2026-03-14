using OpenMES.Domain.Common;

namespace OpenMES.Domain.Hierarchy;

/// <summary>
/// A functional zone within a Site. E.g. "Welding", "Assembly", "Packaging".
/// ISA-95 Level 3 — Area.
/// </summary>
public class Area : AggregateRoot
{
    private readonly List<Line> _lines = [];

    public Guid Id { get; private set; }
    public Guid SiteId { get; private set; }
    public string Code { get; private set; } = default!;
    public string Name { get; private set; } = default!;
    public string? Description { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public IReadOnlyList<Line> Lines => _lines.AsReadOnly();

    private Area() { }

    public static Result<Area> Create(Guid siteId, string code, string name, string? description = null)
    {
        if (string.IsNullOrWhiteSpace(code))
            return Result.Failure<Area>("Area code is required.");

        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure<Area>("Area name is required.");

        var area = new Area
        {
            Id = Guid.NewGuid(),
            SiteId = siteId,
            Code = code.Trim().ToUpper(),
            Name = name.Trim(),
            Description = description?.Trim(),
            CreatedAt = DateTime.UtcNow
        };

        return Result.Success(area);
    }

    public Result AddLine(Line line)
    {
        if (_lines.Any(l => l.Code == line.Code))
            return Result.Failure($"A line with code '{line.Code}' already exists.");

        _lines.Add(line);
        return Result.Success();
    }
}