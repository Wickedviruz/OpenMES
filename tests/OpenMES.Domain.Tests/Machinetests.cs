using OpenMES.Domain.Hierarchy;

namespace OpenMES.Domain.Tests.Hierarchy;

public class MachineTests
{
    [Fact]
    public void Create_WithValidData_ShouldSucceed()
    {
        var result = Machine.Create(Guid.NewGuid(), "MCH-01", "CNC Lathe");

        Assert.True(result.IsSuccess);
        Assert.Equal("MCH-01", result.Value!.Code);
        Assert.Equal(MachineStatus.Unknown, result.Value.Status);
    }

    [Fact]
    public void ChangeStatus_ToDifferentStatus_ShouldSucceed()
    {
        var machine = Machine.Create(Guid.NewGuid(), "MCH-01", "CNC Lathe").Value!;

        var result = machine.ChangeStatus(MachineStatus.Running);

        Assert.True(result.IsSuccess);
        Assert.Equal(MachineStatus.Running, machine.Status);
        Assert.NotNull(machine.LastStatusChangedAt);
    }

    [Fact]
    public void ChangeStatus_ToSameStatus_ShouldFail()
    {
        var machine = Machine.Create(Guid.NewGuid(), "MCH-01", "CNC Lathe").Value!;
        machine.ChangeStatus(MachineStatus.Running);

        var result = machine.ChangeStatus(MachineStatus.Running);

        Assert.True(result.IsFailure);
    }

    [Fact]
    public void ChangeStatus_ShouldRaiseMachineStatusChangedEvent()
    {
        var machine = Machine.Create(Guid.NewGuid(), "MCH-01", "CNC Lathe").Value!;

        machine.ChangeStatus(MachineStatus.Running);

        Assert.Single(machine.DomainEvents);
        Assert.IsType<MachineStatusChanged>(machine.DomainEvents[0]);
    }
}