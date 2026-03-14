namespace OpenMES.Domain.Execution;

/// <summary>
/// An immutable record of something that occurred on a Machine.
/// Written once, never updated. Source of truth for machine history and OEE.
/// </summary>
public class MachineEvent
{
    public Guid Id { get; private set; }
    public Guid MachineId { get; private set; }
    public MachineEventType EventType { get; private set; }
    public string? Payload { get; private set; } // JSON for extra context (alarm codes, tags, etc.)
    public string? Source { get; private set; }  // e.g. "OPC-UA", "Manual", "SCADA"
    public DateTime OccurredAt { get; private set; }

    private MachineEvent() { }

    public static MachineEvent Create(
        Guid machineId,
        MachineEventType eventType,
        string? source = null,
        string? payload = null)
    {
        return new MachineEvent
        {
            Id = Guid.NewGuid(),
            MachineId = machineId,
            EventType = eventType,
            Source = source,
            Payload = payload,
            OccurredAt = DateTime.UtcNow
        };
    }
}

public enum MachineEventType
{
    Started,
    Stopped,
    Idle,
    Alarm,
    AlarmCleared,
    MaintenanceStarted,
    MaintenanceCompleted,
    StatusChanged
}