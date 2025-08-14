namespace Framework.Domain.Models.DomainEvents;

public record DomainEvent(Guid AggregateId) : IDomainEvent
{
    public DateTimeOffset OccurredOn { get; init; } = TimeProvider.System.GetUtcNow();
}