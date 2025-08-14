using Framework.Application.Events;
using Framework.Domain.Models.DomainEvents;

using Microsoft.Extensions.Logging;

namespace Stock.Infrastructure.Pg.Ef.Interceptors;

public class DomainEventBackgroundJob(ILogger<DomainEventBackgroundJob> logger) : IDomainEventHandler
{
    private readonly ILogger<DomainEventBackgroundJob> _logger =
        logger ?? throw new ArgumentNullException(nameof(logger));

    public async Task HandleAsync(IDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(domainEvent);

        _logger.LogInformation(
            "Processing domain event {EventType} for aggregate {AggregateId} occurred at {OccurredOn}",
            domainEvent.GetType().Name, domainEvent.AggregateId, domainEvent.OccurredOn);

        await Task.CompletedTask;
    }
}