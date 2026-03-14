using OpenMES.Domain.Common;

namespace OpenMES.Domain.Production;

/// <summary>
/// A manufacturing order — produce N units of a Product following a Routing at a WorkCell.
/// This is the central aggregate of the MES execution layer.
/// </summary>
public class WorkOrder : AggregateRoot
{
    public Guid Id { get; private set; }
    public string OrderNumber { get; private set; } = default!;
    public Guid ProductId { get; private set; }
    public Guid RoutingId { get; private set; }
    public Guid WorkCellId { get; private set; }
    public int PlannedQuantity { get; private set; }
    public int ProducedQuantity { get; private set; }
    public int ScrapQuantity { get; private set; }
    public WorkOrderStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? StartedAt { get; private set; }
    public DateTime? CompletedAt { get; private set; }

    private WorkOrder() { }

    public static Result<WorkOrder> Create(
        string orderNumber,
        Guid productId,
        Guid routingId,
        Guid workCellId,
        int plannedQuantity)
    {
        if (string.IsNullOrWhiteSpace(orderNumber))
            return Result.Failure<WorkOrder>("Order number is required.");

        if (plannedQuantity <= 0)
            return Result.Failure<WorkOrder>("Planned quantity must be greater than zero.");

        var order = new WorkOrder
        {
            Id = Guid.NewGuid(),
            OrderNumber = orderNumber.Trim().ToUpper(),
            ProductId = productId,
            RoutingId = routingId,
            WorkCellId = workCellId,
            PlannedQuantity = plannedQuantity,
            Status = WorkOrderStatus.Released,
            CreatedAt = DateTime.UtcNow
        };

        order.AddDomainEvent(new WorkOrderCreated(order.Id, order.OrderNumber, productId, workCellId));
        return Result.Success(order);
    }

    public Result Start()
    {
        if (Status != WorkOrderStatus.Released)
            return Result.Failure($"Cannot start a work order with status '{Status}'.");

        Status = WorkOrderStatus.InProgress;
        StartedAt = DateTime.UtcNow;
        AddDomainEvent(new WorkOrderStarted(Id, StartedAt.Value));
        return Result.Success();
    }

    public Result ReportProduction(int produced, int scrap)
    {
        if (Status != WorkOrderStatus.InProgress)
            return Result.Failure("Can only report production on in-progress orders.");

        if (produced < 0 || scrap < 0)
            return Result.Failure("Quantities cannot be negative.");

        if (scrap > produced)
            return Result.Failure("Scrap cannot exceed produced quantity.");

        ProducedQuantity += produced;
        ScrapQuantity += scrap;
        AddDomainEvent(new ProductionReported(Id, produced, scrap, ProducedQuantity, ScrapQuantity));
        return Result.Success();
    }

    public Result Complete()
    {
        if (Status != WorkOrderStatus.InProgress)
            return Result.Failure($"Cannot complete a work order with status '{Status}'.");

        Status = WorkOrderStatus.Completed;
        CompletedAt = DateTime.UtcNow;
        AddDomainEvent(new WorkOrderCompleted(Id, ProducedQuantity, ScrapQuantity, CompletedAt.Value));
        return Result.Success();
    }

    public Result Cancel(string reason)
    {
        if (Status is WorkOrderStatus.Completed or WorkOrderStatus.Cancelled)
            return Result.Failure($"Cannot cancel a work order with status '{Status}'.");

        Status = WorkOrderStatus.Cancelled;
        AddDomainEvent(new WorkOrderCancelled(Id, reason));
        return Result.Success();
    }
}

public enum WorkOrderStatus
{
    Released,
    InProgress,
    Completed,
    Cancelled
}