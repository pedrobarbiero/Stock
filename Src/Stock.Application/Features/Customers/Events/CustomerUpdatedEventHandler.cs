using Framework.Application.Events;

using Stock.Domain.Models.Customers.DomainEvents;

namespace Stock.Application.Features.Customers.Events;

public class CustomerUpdatedEventHandler
    : IDomainEventHandler<CustomerUpdatedEvent>
{
    public async Task HandleAsync(CustomerUpdatedEvent domainEvent, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
    }
}