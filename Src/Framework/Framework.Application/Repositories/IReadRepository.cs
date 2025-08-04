using Framework.Domain.Models;

namespace Framework.Application.Repositories;

public interface IReadRepository<T> where T : AggregateRoot
{
    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<T?> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken);
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken);
    Task<bool> ExistsAllAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken);
}