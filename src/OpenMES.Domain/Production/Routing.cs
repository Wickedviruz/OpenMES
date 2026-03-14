using OpenMES.Domain.Common;

namespace OpenMES.Domain.Production;

/// <summary>
/// Defines how a Product is manufactured — an ordered sequence of Operations.
/// A Product can have multiple Routing versions, but only one active at a time.
/// </summary>
public class Routing : AggregateRoot
{
    private readonly List<RoutingStep> _steps = [];

    public Guid Id { get; private set; }
    public Guid ProductId { get; private set; }
    public string Version { get; private set; } = default!;
    public string? Description { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public IReadOnlyList<RoutingStep> Steps => _steps.OrderBy(s => s.SequenceNumber).ToList().AsReadOnly();

    private Routing() { }

    public static Result<Routing> Create(Guid productId, string version, string? description = null)
    {
        if (string.IsNullOrWhiteSpace(version))
            return Result.Failure<Routing>("Routing version is required.");

        var routing = new Routing
        {
            Id = Guid.NewGuid(),
            ProductId = productId,
            Version = version.Trim(),
            Description = description?.Trim(),
            IsActive = false,
            CreatedAt = DateTime.UtcNow
        };

        return Result.Success(routing);
    }

    public Result AddStep(Guid operationId, int sequenceNumber)
    {
        if (_steps.Any(s => s.SequenceNumber == sequenceNumber))
            return Result.Failure($"A step with sequence number {sequenceNumber} already exists.");

        _steps.Add(new RoutingStep(Id, operationId, sequenceNumber));
        return Result.Success();
    }

    public void Activate() => IsActive = true;
    public void Deactivate() => IsActive = false;
}

/// <summary>
/// A single step in a Routing — links an Operation with its sequence position.
/// </summary>
public class RoutingStep
{
    public Guid RoutingId { get; private set; }
    public Guid OperationId { get; private set; }
    public int SequenceNumber { get; private set; }

    internal RoutingStep(Guid routingId, Guid operationId, int sequenceNumber)
    {
        RoutingId = routingId;
        OperationId = operationId;
        SequenceNumber = sequenceNumber;
    }

    private RoutingStep() { } // EF Core
}