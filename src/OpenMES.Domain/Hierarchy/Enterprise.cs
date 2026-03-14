using OpenMES.Domain.Common;

namespace OpenMES.Domain.Hierarchy;

/// <summary>
/// Represents the top-level organization (company).
/// ISA-95 Level 4 — Enterprise.
/// </summary>
public class Enterprise : AggregateRoot
{
    private readonly List<Site> _sites = [];

    public Guid Id { get; private set; }
    public string Name { get; private set; } = default!;
    public string? Description { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public IReadOnlyList<Site> Sites => _sites.AsReadOnly();

    private Enterprise() { }

    public static Result<Enterprise> Create(string name, string? description = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure<Enterprise>("Enterprise name is required.");

        var enterprise = new Enterprise
        {
            Id = Guid.NewGuid(),
            Name = name.Trim(),
            Description = description?.Trim(),
            CreatedAt = DateTime.UtcNow
        };

        return Result.Success(enterprise);
    }

    public Result AddSite(Site site)
    {
        if (_sites.Any(s => s.Code == site.Code))
            return Result.Failure($"A site with code '{site.Code}' already exists.");

        _sites.Add(site);
        return Result.Success();
    }
}