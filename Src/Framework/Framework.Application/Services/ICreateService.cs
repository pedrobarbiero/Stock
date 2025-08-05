using Framework.Application.Requests;

namespace Framework.Application.Services;

public interface ICreateService<TAggregateRoot, in TRequest, TResponse>
    where TAggregateRoot : class
    where TResponse : class
{
    Task<RequestResult<TResponse>> CreateAsync(TRequest request, CancellationToken cancellationToken);
}