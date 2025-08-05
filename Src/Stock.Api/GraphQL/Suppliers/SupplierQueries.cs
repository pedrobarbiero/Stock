using Microsoft.EntityFrameworkCore;
using Stock.Infrastructure.Pg.Ef;
using Stock.Domain.Models.Suppliers;

namespace Stock.Api.GraphQL.Suppliers;

[ExtendObjectType("Query")]
public class SupplierQueries
{
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Supplier> GetSuppliers([Service] StockDbContext context) =>
        context.Suppliers;

    public Task<Supplier?> GetSupplierById(
        Guid id,
        [Service] StockDbContext context)
        => context.Suppliers.SingleOrDefaultAsync(s => s.Id == id);
}