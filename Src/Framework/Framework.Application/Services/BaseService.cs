using Framework.Application.Mappers;
using Framework.Application.Repositories;
using Framework.Application.Requests;
using Framework.Application.Validation;
using Framework.Domain.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.Application.Services;

public abstract class BaseService<TAggregateRoot, TCreateRequest, TUpdateRequest, TResponse>(
    IServiceProvider serviceProvider)
    : IBaseService<TAggregateRoot, TCreateRequest, TUpdateRequest, TResponse>
    where TAggregateRoot : AggregateRoot
    where TUpdateRequest : IUpdateRequest
    where TResponse : class
{
    protected readonly IMapper<TAggregateRoot, TResponse, TCreateRequest> Mapper =
        serviceProvider.GetRequiredService<IMapper<TAggregateRoot, TResponse, TCreateRequest>>();

    protected readonly IServiceProvider ServiceProvider = serviceProvider;

    protected virtual TResponse ToResponse(TAggregateRoot entity) => Mapper.ToResponse(entity);

    # region Create

    public virtual async Task<RequestResult<TResponse>> CreateAsync(TCreateRequest request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));

        var createRepository = ServiceProvider.GetRequiredService<ICreateRepository<TAggregateRoot>>();

        var validationResult = await ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return RequestResult<TResponse>.Failure(validationResult);

        var aggregateRoot = ToDomain(request);
        var added = createRepository.Add(aggregateRoot);

        return RequestResult<TResponse>.Success(ToResponse(added));
    }

    protected virtual Task<ValidationResult> ValidateAsync(TCreateRequest request, CancellationToken cancellationToken)
    {
        var createValidator = ServiceProvider.GetRequiredService<IRequestValidator<TCreateRequest>>();
        return createValidator.ValidateAsync(request, cancellationToken);
    }

    protected virtual TAggregateRoot ToDomain(TCreateRequest request) => Mapper.ToDomain(request);

    # endregion


    # region Update

    public virtual async Task<RequestResult<TResponse>> UpdateAsync(TUpdateRequest request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));
        ArgumentOutOfRangeException.ThrowIfEqual(request.Id, Guid.Empty);

        var readRepository = ServiceProvider.GetRequiredService<IReadRepository<TAggregateRoot>>();
        var updateRepository = ServiceProvider.GetRequiredService<IUpdateRepository<TAggregateRoot>>();

        var validationResult = await ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return RequestResult<TResponse>.Failure(validationResult);

        var existingAggregateRoot = await readRepository.GetByIdAsync(request.Id, cancellationToken);
        if (existingAggregateRoot is null)
            return RequestResult<TResponse>.NotFound(typeof(TAggregateRoot).Name, request.Id);

        var aggregateRoot = UpdateDomain(request, existingAggregateRoot);
        var updated = updateRepository.Update(aggregateRoot);

        return RequestResult<TResponse>.Success(ToResponse(updated));
    }

    protected virtual Task<ValidationResult> ValidateAsync(TUpdateRequest request, CancellationToken cancellationToken)
    {
        var updateValidator = ServiceProvider.GetRequiredService<IRequestValidator<TUpdateRequest>>();
        return updateValidator.ValidateAsync(request, cancellationToken);
    }

    protected abstract TAggregateRoot UpdateDomain(TUpdateRequest request,
        TAggregateRoot existingAggregateRoot);

    # endregion

    # region Delete

    public virtual async Task<RequestResult<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        ArgumentOutOfRangeException.ThrowIfEqual(id, Guid.Empty);
        var deleteRepository = ServiceProvider.GetRequiredService<IDeleteRepository<TAggregateRoot>>();
        var readRepository = ServiceProvider.GetRequiredService<IReadRepository<TAggregateRoot>>();

        var existing = await readRepository.GetByIdAsync(id, cancellationToken);
        if (existing is null)
            return RequestResult<bool>.NotFound(typeof(TAggregateRoot).Name, id);

        var validationResult = await ValidateDeletionAsync(id);
        if (!validationResult.IsValid)
            return RequestResult<bool>.Failure(validationResult);

        deleteRepository.Delete(existing);

        return RequestResult<bool>.Success();
    }

    protected virtual Task<ValidationResult> ValidateDeletionAsync(Guid id) =>
        Task.FromResult(ValidationResult.Success());

    # endregion
}