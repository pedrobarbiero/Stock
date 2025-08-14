using Framework.Domain.Models.DomainEvents;

namespace Framework.Application.Events;

public interface IDomainEventHandlerRegistry
{
    Task HandleAsync(IDomainEvent domainEvent, CancellationToken cancellationToken);
}