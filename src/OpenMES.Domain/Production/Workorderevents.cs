using OpenMES.Domain.Common;

namespace OpenMES.Domain.Production;

public record WorkOrderCreated(
    Guid WorkOrderId,
    string OrderNumber,
    Guid ProductId,
    Guid WorkCellId) : DomainEvent;

public record WorkOrderStarted(
    Guid WorkOrderId,
    DateTime StartedAt) : DomainEvent;

public record ProductionReported(
    Guid WorkOrderId,
    int Produced,
    int Scrap,
    int TotalProduced,
    int TotalScrap) : DomainEvent;

public record WorkOrderCompleted(
    Guid WorkOrderId,
    int FinalProducedQuantity,
    int FinalScrapQuantity,
    DateTime CompletedAt) : DomainEvent;

public record WorkOrderCancelled(
    Guid WorkOrderId,
    string Reason) : DomainEvent;