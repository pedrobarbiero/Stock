using Framework.Domain.Models.DomainEvents;

namespace Framework.Application.Events;

public interface IDomainEventHandler<in T> where T : IDomainEvent
{
    Task HandleAsync(T domainEvent, CancellationToken cancellationToken);
}
