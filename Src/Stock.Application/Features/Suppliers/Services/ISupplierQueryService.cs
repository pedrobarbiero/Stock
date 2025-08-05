using Stock.Application.Features.Suppliers.QueryResults;

namespace Stock.Application.Features.Suppliers.Services;

public interface ISupplierQueryService
{
    IQueryable<SupplierQueryResult> GetSuppliers();
    Task<SupplierQueryResult?> GetSupplierById(Guid id, CancellationToken cancellationToken);
}