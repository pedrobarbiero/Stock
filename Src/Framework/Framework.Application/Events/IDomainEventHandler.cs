using Framework.Domain.Models.DomainEvents;

namespace Framework.Application.Events;

public interface IDomainEventHandler
{
    Task HandleAsync(IDomainEvent domainEvent, CancellationToken cancellationToken);
}