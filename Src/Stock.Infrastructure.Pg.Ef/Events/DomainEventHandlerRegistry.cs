using Framework.Application.Events;
using Framework.Domain.Models.DomainEvents;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Stock.Domain.Models.Customers.DomainEvents;

namespace Stock.Infrastructure.Pg.Ef.Events;

public class DomainEventHandlerRegistry(IServiceProvider serviceProvider, ILogger<DomainEventHandlerRegistry> logger)
    : IDomainEventHandlerRegistry
{
    public async Task HandleAsync(IDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        switch (domainEvent)
        {
            case CustomerUpdatedEvent customerUpdatedEvent:
                var customerHandler = serviceProvider.GetRequiredService<CustomerUpdatedEventHandler>();
                await customerHandler.HandleAsync(customerUpdatedEvent, cancellationToken);
                break;

            default:
                logger.LogWarning("No handler found for domain event type {EventType}", domainEvent.GetType().Name);
                break;
        }
    }
}