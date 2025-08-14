using Framework.Application.Events;
using Framework.Domain.Models;

using Microsoft.EntityFrameworkCore;

using Stock.Domain.Models.Customers;
using Stock.Domain.Models.Suppliers;

namespace Stock.Infrastructure.Pg.Ef;

public sealed class StockDbContext(
    DbContextOptions<StockDbContext> options,
    IDomainEventDispatcher domainEventDispatcher) : DbContext(options)
{
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<Customer> Customers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(StockDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var result = await base.SaveChangesAsync(cancellationToken);
        await DispatchDomainEventsAsync(this, cancellationToken);
        return result;
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
            await domainEventDispatcher.DispatchAsync(domainEvents, cancellationToken);
        }
    }
}