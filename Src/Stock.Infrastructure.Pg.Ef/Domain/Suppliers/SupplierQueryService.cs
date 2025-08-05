using Microsoft.EntityFrameworkCore;
using Stock.Application.Features.Suppliers.QueryResults;
using Stock.Application.Features.Suppliers.Services;

namespace Stock.Infrastructure.Pg.Ef.Domain.Suppliers;

public class SupplierQueryService(StockDbContext context) : ISupplierQueryService
{
    private IQueryable<SupplierQueryResult> BaseQuery =>
        context.Suppliers
            .Select(s => new SupplierQueryResult
            {
                Id = s.Id,
                Name = s.Name,
                Email = s.Email
            }).AsNoTracking();

    public IQueryable<SupplierQueryResult> GetSuppliers() => BaseQuery;

    public Task<SupplierQueryResult?> GetSupplierById(Guid id, CancellationToken cancellationToken) =>
        BaseQuery.Where(s => s.Id == id).SingleOrDefaultAsync(cancellationToken);
}