using Framework.Application.Events;
using Framework.Domain.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Stock.Infrastructure.Pg.Ef.Interceptors;

public class DomainEventInterceptor(IDomainEventDispatcher domainEventDispatcher) : ISaveChangesInterceptor
{
    private readonly IDomainEventDispatcher _domainEventDispatcher =
        domainEventDispatcher ?? throw new ArgumentNullException(nameof(domainEventDispatcher));

    public InterceptionResult SavedChanges(SaveChangesCompletedEventData eventData, int result)
    {
        if (eventData.Context is not null)
        {
            DispatchDomainEventsAsync(eventData.Context, CancellationToken.None).GetAwaiter().GetResult();
        }

        return InterceptionResult.Suppress();
    }

    public async ValueTask<InterceptionResult> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result,
        CancellationToken cancellationToken)
    {
        if (eventData.Context is not null)
        {
            await DispatchDomainEventsAsync(eventData.Context, cancellationToken);
        }

        return await ValueTask.FromResult(InterceptionResult.Suppress());
    }

    private async Task DispatchDomainEventsAsync(DbContext context, CancellationToken cancellationToken)
    {
        var aggregateRoots = context.ChangeTracker.Entries<AggregateRoot>()
            .Where(x => x.Entity.DomainEvents.Count > 0)
            .Select(x => x.Entity)
            .ToList();

        var domainEvents = aggregateRoots
            .SelectMany(x => x.DomainEvents)
            .ToList();

        aggregateRoots.ForEach(entity => entity.ClearDomainEvents());

        if (domainEvents.Count > 0)
        {
            await _domainEventDispatcher.DispatchAsync(domainEvents, cancellationToken);
        }
    }
}