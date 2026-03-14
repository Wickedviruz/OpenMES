namespace OpenMES.Domain.Common;

public abstract class AggregateRoot
{
    private readonly List<DomainEvent> _domainEvents = [];

    public IReadOnlyList<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    protected void AddDomainEvent(DomainEvent evt) => _domainEvents.Add(evt);

    public void ClearDomainEvents() => _domainEvents.Clear();
}