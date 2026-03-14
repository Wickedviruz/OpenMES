using OpenMES.Domain.Common;

namespace OpenMES.Domain.Hierarchy;

/// <summary>
/// A physical plant or facility belonging to an Enterprise.
/// ISA-95 Level 3 — Site.
/// </summary>
public class Site : AggregateRoot
{
    private readonly List<Area> _areas = [];

    public Guid Id { get; private set; }
    public Guid EnterpriseId { get; private set; }
    public string Code { get; private set; } = default!;
    public string Name { get; private set; } = default!;
    public string? Location { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public IReadOnlyList<Area> Areas => _areas.AsReadOnly();

    private Site() { }

    public static Result<Site> Create(Guid enterpriseId, string code, string name, string? location = null)
    {
        if (string.IsNullOrWhiteSpace(code))
            return Result.Failure<Site>("Site code is required.");

        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure<Site>("Site name is required.");

        var site = new Site
        {
            Id = Guid.NewGuid(),
            EnterpriseId = enterpriseId,
            Code = code.Trim().ToUpper(),
            Name = name.Trim(),
            Location = location?.Trim(),
            CreatedAt = DateTime.UtcNow
        };

        return Result.Success(site);
    }

    public Result AddArea(Area area)
    {
        if (_areas.Any(a => a.Code == area.Code))
            return Result.Failure($"An area with code '{area.Code}' already exists.");

        _areas.Add(area);
        return Result.Success();
    }
}