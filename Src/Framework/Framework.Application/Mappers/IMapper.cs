using Framework.Domain.Models;

namespace Framework.Application.Mappers;

public interface IMapper<TAggregateRoot, out TResponse, in TCreateAggregateRootUseCase>
    where TAggregateRoot : AggregateRoot
    where TResponse : class
{
    TResponse ToResponse(TAggregateRoot aggregateRoot);
    TAggregateRoot ToDomain(TCreateAggregateRootUseCase response);
}