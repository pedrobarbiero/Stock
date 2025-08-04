using Framework.Application.Requests;
using Framework.Domain.Models;

namespace Framework.Application.Services;

public interface IBaseService<TAggregateRoot, in TCreateRequest, in TUpdateRequest, TResponse> :
    ICreateService<TAggregateRoot, TCreateRequest, TResponse>,
    IUpdateService<TAggregateRoot, TUpdateRequest, TResponse>,
    IDeleteService<TAggregateRoot>
    where TAggregateRoot : AggregateRoot
    where TResponse : class
    where TUpdateRequest : IUpdateRequest;