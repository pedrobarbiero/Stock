using Stock.Application.Features.Suppliers.QueryResults;
using Stock.Application.Features.Suppliers.Services;

namespace Stock.Api.GraphQL.Suppliers;

[ExtendObjectType("Query")]
public class SupplierQueries
{
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<SupplierQueryResult> GetSuppliers([Service] ISupplierQueryService queryService) =>
        queryService.GetSuppliers();

    public Task<SupplierQueryResult?> GetSupplierById(
        Guid id,
        [Service] ISupplierQueryService queryService,
        CancellationToken cancellationToken)
        => queryService.GetSupplierById(id, cancellationToken);
}