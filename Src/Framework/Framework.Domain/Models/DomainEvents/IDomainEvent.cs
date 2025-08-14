namespace Framework.Domain.Models.DomainEvents;

public interface IDomainEvent
{
    public Guid AggregateId { get; }
    public DateTimeOffset OccurredOn { get; }
}