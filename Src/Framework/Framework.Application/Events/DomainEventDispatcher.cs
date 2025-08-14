using Framework.Application.BackgroundJobs;
using Framework.Domain.Models.DomainEvents;

namespace Framework.Application.Events;

public class DomainEventDispatcher(IBackgroundJobScheduler jobScheduler) : IDomainEventDispatcher
{
    private readonly IBackgroundJobScheduler _jobScheduler =
        jobScheduler ?? throw new ArgumentNullException(nameof(jobScheduler));

    public Task DispatchAsync(IDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(domainEvent);

        _jobScheduler.Enqueue<DomainEventBackgroundJob>(job => job.ProcessAsync(domainEvent, cancellationToken));

        return Task.CompletedTask;
    }

    public Task DispatchAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(domainEvents);

        foreach (var domainEvent in domainEvents)
        {
            _jobScheduler.Enqueue<DomainEventBackgroundJob>(job => job.ProcessAsync(domainEvent, cancellationToken));
        }

        return Task.CompletedTask;
    }
}