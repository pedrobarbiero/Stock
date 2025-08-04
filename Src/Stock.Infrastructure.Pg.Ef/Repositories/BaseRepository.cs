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
    private readonly DbSet<T> _dbSet = context.Set<T>();

    public virtual T Add(T aggregateRoot) => _dbSet.Add(aggregateRoot).Entity;

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
        => _dbSet.SingleOrDefaultAsync(t => t.Id == id, cancellationToken);

    public async Task<IEnumerable<T>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
        => await _dbSet.Where(t => ids.Contains(t.Id)).ToListAsync(cancellationToken);


    public void Delete(T entity)
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));

        _dbSet.Remove(entity);
    }

    public virtual Task<bool> ExistsAsync(Guid id,
        CancellationToken cancellationToken) =>
        _dbSet.AnyAsync(t => t.Id == id, cancellationToken);

    public async Task<bool> ExistsAllAsync(IEnumerable<Guid> ids,
        CancellationToken cancellationToken)
    {
        List<Guid> distinctIds = ids.Distinct().ToList();
        int count = await _dbSet.Where(t => distinctIds.Contains(t.Id)).CountAsync(cancellationToken);
        return count == distinctIds.Count;
    }

    public virtual T Update(T aggregateRoot) => _dbSet.Update(aggregateRoot).Entity;
}