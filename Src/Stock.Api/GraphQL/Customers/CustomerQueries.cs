using Stock.Application.Features.Customers.Repositories;
using Stock.Application.Mappers.Mapperly.Domain.Customers;
using Stock.Application.Features.Customers.Responses;
using Stock.Infrastructure.Pg.Ef;
using Stock.Domain.Models.Customers;

namespace Stock.Api.GraphQL.Customers;

[ExtendObjectType("Query")]
public class CustomerQueries
{
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Customer> GetCustomers([Service] StockDbContext context) =>
        context.Customers;

    public async Task<CustomerResponse?> GetCustomerById(
        Guid id,
        [Service] ICustomerRepository customerRepository,
        [Service] CreateCustomerMapper mapper)
    {
        var customer = await customerRepository.GetByIdAsync(id, CancellationToken.None);
        return customer != null ? mapper.ToResponse(customer) : null;
    }
}