using Framework.Application.Requests;

namespace Framework.Application.Services;

public interface IDeleteService<TAggregateRoot>
    where TAggregateRoot : class
{
    Task<RequestResult<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken);
}