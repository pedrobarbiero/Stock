using Framework.Application.Events;

using Microsoft.Extensions.Logging;

using Stock.Domain.Models.Customers.DomainEvents;

namespace Stock.Infrastructure.Pg.Ef.Events;

public class CustomerUpdatedEventHandler(ILogger<CustomerUpdatedEventHandler> logger)
    : IDomainEventHandler<CustomerUpdatedEvent>
{
    public async Task HandleAsync(CustomerUpdatedEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Customer {CustomerId} updated at {OccurredOn}",
            domainEvent.CustomerId, domainEvent.OccurredOn);

        await Task.CompletedTask;
    }
}