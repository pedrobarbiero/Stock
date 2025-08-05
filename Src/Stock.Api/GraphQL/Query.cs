using Stock.Application.Features.Suppliers.Repositories;
using Stock.Application.Mappers.Mapperly.Domain.Suppliers;
using Stock.Application.Features.Suppliers.Responses;
using Stock.Infrastructure.Pg.Ef;
using Stock.Domain.Models.Suppliers;

namespace Stock.Api.GraphQL;

public class Query
{
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Supplier> GetSuppliers([Service] StockDbContext context) =>
        context.Suppliers;

    public async Task<SupplierResponse?> GetSupplierById(
        Guid id,
        [Service] ISupplierRepository supplierRepository,
        [Service] CreateSupplierMapper mapper)
    {
        var supplier = await supplierRepository.GetByIdAsync(id, CancellationToken.None);
        return supplier != null ? mapper.ToResponse(supplier) : null;
    }
}