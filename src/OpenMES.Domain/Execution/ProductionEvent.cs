namespace OpenMES.Domain.Execution;

/// <summary>
/// An immutable record of something that occurred during production execution.
/// Written once, never updated. Tied to a WorkOrder.
/// Full production history is reconstructed from these events.
/// </summary>
public class ProductionEvent
{
    public Guid Id { get; private set; }
    public Guid WorkOrderId { get; private set; }
    public Guid? MachineId { get; private set; }
    public Guid? OperatorId { get; private set; }
    public ProductionEventType EventType { get; private set; }
    public int? Quantity { get; private set; }
    public string? Payload { get; private set; } // JSON for extra context
    public DateTime OccurredAt { get; private set; }

    private ProductionEvent() { }

    public static ProductionEvent Create(
        Guid workOrderId,
        ProductionEventType eventType,
        Guid? machineId = null,
        Guid? operatorId = null,
        int? quantity = null,
        string? payload = null)
    {
        return new ProductionEvent
        {
            Id = Guid.NewGuid(),
            WorkOrderId = workOrderId,
            MachineId = machineId,
            OperatorId = operatorId,
            EventType = eventType,
            Quantity = quantity,
            Payload = payload,
            OccurredAt = DateTime.UtcNow
        };
    }
}

public enum ProductionEventType
{
    WorkOrderStarted,
    QuantityProduced,
    ScrapReported,
    WorkOrderCompleted,
    WorkOrderCancelled,
    OperationStarted,
    OperationCompleted
}