using Framework.Domain.Models.DomainEvents;

namespace Framework.Application.Events;

public interface IDomainEventProcessor
{
    Task ProcessAsync(IDomainEvent domainEvent, CancellationToken cancellationToken);
}