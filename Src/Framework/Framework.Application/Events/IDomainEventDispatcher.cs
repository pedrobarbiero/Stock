using Framework.Domain.Models.DomainEvents;

namespace Framework.Application.Events;

public interface IDomainEventDispatcher
{
    Task DispatchAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken);
}