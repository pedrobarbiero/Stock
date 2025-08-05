using Stock.Application.Features.Customers.QueryResults;
using Stock.Application.Features.Customers.Services;

namespace Stock.Infrastructure.Pg.Ef.Domain.Customers;

public class CustomerQueryService(StockDbContext context) : ICustomerQueryService
{
    private IQueryable<CustomerQueryResult> BaseQuery =>
        context.Customers
            .Select(c => new CustomerQueryResult
            {
                Id = c.Id,
                Name = c.Name,
                Email = c.Email,
                Addresses = c.Addresses.Select(a => new CustomerAddressQueryResult
                {
                    Id = a.Id,
                    Street = a.Street,
                    City = a.City,
                })
            });

    public IQueryable<CustomerQueryResult> GetCustomers() => BaseQuery;

    public IQueryable<CustomerQueryResult> GetCustomerById(Guid id) => BaseQuery.Where(c => c.Id == id);
}