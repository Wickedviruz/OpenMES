using OpenMES.Domain.Production;

namespace OpenMES.Domain.Tests.Production;

public class WorkOrderTests
{
    private static WorkOrder CreateValidOrder() =>
        WorkOrder.Create("WO-001", Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), 100).Value!;

    [Fact]
    public void Create_WithValidData_ShouldSucceed()
    {
        var result = WorkOrder.Create("WO-001", Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), 100);

        Assert.True(result.IsSuccess);
        Assert.Equal("WO-001", result.Value!.OrderNumber);
        Assert.Equal(WorkOrderStatus.Released, result.Value.Status);
        Assert.Equal(100, result.Value.PlannedQuantity);
    }

    [Fact]
    public void Create_WithEmptyOrderNumber_ShouldFail()
    {
        var result = WorkOrder.Create("", Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), 100);

        Assert.True(result.IsFailure);
        Assert.NotNull(result.Error);
    }

    [Fact]
    public void Create_WithZeroQuantity_ShouldFail()
    {
        var result = WorkOrder.Create("WO-001", Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), 0);

        Assert.True(result.IsFailure);
    }

    [Fact]
    public void Start_FromReleased_ShouldSucceed()
    {
        var order = CreateValidOrder();

        var result = order.Start();

        Assert.True(result.IsSuccess);
        Assert.Equal(WorkOrderStatus.InProgress, order.Status);
        Assert.NotNull(order.StartedAt);
    }

    [Fact]
    public void Start_FromInProgress_ShouldFail()
    {
        var order = CreateValidOrder();
        order.Start();

        var result = order.Start();

        Assert.True(result.IsFailure);
    }

    [Fact]
    public void ReportProduction_WhenInProgress_ShouldAccumulate()
    {
        var order = CreateValidOrder();
        order.Start();

        order.ReportProduction(50, 2);
        order.ReportProduction(30, 1);

        Assert.Equal(80, order.ProducedQuantity);
        Assert.Equal(3, order.ScrapQuantity);
    }

    [Fact]
    public void ReportProduction_WhenScrapExceedsProduced_ShouldFail()
    {
        var order = CreateValidOrder();
        order.Start();

        var result = order.ReportProduction(10, 20);

        Assert.True(result.IsFailure);
    }

    [Fact]
    public void Complete_WhenInProgress_ShouldSucceed()
    {
        var order = CreateValidOrder();
        order.Start();
        order.ReportProduction(100, 0);

        var result = order.Complete();

        Assert.True(result.IsSuccess);
        Assert.Equal(WorkOrderStatus.Completed, order.Status);
        Assert.NotNull(order.CompletedAt);
    }

    [Fact]
    public void Cancel_WhenCompleted_ShouldFail()
    {
        var order = CreateValidOrder();
        order.Start();
        order.Complete();

        var result = order.Cancel("test");

        Assert.True(result.IsFailure);
    }

    [Fact]
    public void Create_ShouldRaiseWorkOrderCreatedEvent()
    {
        var result = WorkOrder.Create("WO-001", Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), 100);

        Assert.Single(result.Value!.DomainEvents);
        Assert.IsType<WorkOrderCreated>(result.Value.DomainEvents[0]);
    }
}