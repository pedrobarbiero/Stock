using Framework.Application.Repositories;
using Framework.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Stock.Infrastructure.Pg.Ef.Repositories;

public abstract class BaseRepository<T>(StockDbContext context) :
    IReadRepository<T>,
    ICreateRepository<T>,
    IUpdateRepository<T>,
    IDeleteRepository<T>
    where T : AggregateRoot
{
    protected readonly DbSet<T> DbSet = context.Set<T>();

    protected virtual IQueryable<T> DbSetWithIncludes() => DbSet;

    public virtual T Add(T aggregateRoot) => DbSet.Add(aggregateRoot).Entity;

    public virtual async Task<bool> DeleteAsync(Guid id,
        CancellationToken cancellationToken)
    {
        var entity = await GetByIdAsync(id, cancellationToken);
        if (entity is null) return false;

        Delete(entity);

        return true;
    }

    public virtual Task<T?> GetByIdAsync(Guid id,
        CancellationToken cancellationToken)
        => DbSetWithIncludes().SingleOrDefaultAsync(t => t.Id == id, cancellationToken);

    public async Task<IEnumerable<T>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
        => await DbSetWithIncludes().Where(t => ids.Contains(t.Id)).ToListAsync(cancellationToken);


    public void Delete(T entity)
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));

        DbSet.Remove(entity);
    }

    public virtual Task<bool> ExistsAsync(Guid id,
        CancellationToken cancellationToken) =>
        DbSet.AnyAsync(t => t.Id == id, cancellationToken);

    public async Task<bool> ExistsAllAsync(IEnumerable<Guid> ids,
        CancellationToken cancellationToken)
    {
        List<Guid> distinctIds = ids.Distinct().ToList();
        int count = await DbSet.Where(t => distinctIds.Contains(t.Id)).CountAsync(cancellationToken);
        return count == distinctIds.Count;
    }

    public virtual T Update(T aggregateRoot) => DbSet.Update(aggregateRoot).Entity;
}