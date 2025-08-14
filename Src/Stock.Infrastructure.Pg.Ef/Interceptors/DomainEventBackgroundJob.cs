using Framework.Application.Events;
using Framework.Domain.Models.DomainEvents;

using Microsoft.Extensions.Logging;

namespace Stock.Infrastructure.Pg.Ef.Interceptors;

public class DomainEventBackgroundJob(
    IDomainEventHandlerRegistry handlerRegistry,
    ILogger<DomainEventBackgroundJob> logger)
    : IDomainEventProcessor
{
    public async Task ProcessAsync(IDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(domainEvent);

        logger.LogInformation(
            "Processing domain event {EventType} for aggregate {AggregateId} occurred at {OccurredOn}",
            domainEvent.GetType().Name, domainEvent.AggregateId, domainEvent.OccurredOn);

        await handlerRegistry.HandleAsync(domainEvent, cancellationToken);
    }
}