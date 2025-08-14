using Framework.Application.BackgroundJobs;
using Framework.Domain.Models.DomainEvents;

namespace Framework.Application.Events;

public class DomainEventDispatcher(IBackgroundJobScheduler jobScheduler) : IDomainEventDispatcher
{
    public Task DispatchAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(domainEvents);

        foreach (var domainEvent in domainEvents)
        {
            jobScheduler.Enqueue<IDomainEventProcessor>(processor =>
                processor.ProcessAsync(domainEvent, cancellationToken));
        }

        return Task.CompletedTask;
    }
}