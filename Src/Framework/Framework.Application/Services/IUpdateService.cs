using Framework.Application.Requests;

namespace Framework.Application.Services;

public interface IUpdateService<TAggregateRoot, in TRequest, TResponse>
    where TRequest : IUpdateRequest
    where TAggregateRoot : class
{
    Task<RequestResult<TResponse>> UpdateAsync(TRequest request, CancellationToken cancellationToken);
}