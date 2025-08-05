using Stock.Application.Features.Customers.QueryResults;
using Stock.Application.Features.Customers.Services;

namespace Stock.Api.GraphQL.Customers;

[ExtendObjectType("Query")]
public class CustomerQueries
{
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<CustomerQueryResult> GetCustomers([Service] ICustomerQueryService customerQueryService) =>
        customerQueryService.GetCustomers();

    public IQueryable<CustomerQueryResult> GetCustomerById(
        Guid id,
        [Service] ICustomerQueryService customerQueryService)
        => customerQueryService.GetCustomerById(id);
}