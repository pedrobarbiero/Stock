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

    public Task<CustomerQueryResult?> GetCustomerById(
        Guid id,
        CancellationToken cancellationToken,
        [Service] ICustomerQueryService customerQueryService)
        => customerQueryService.GetCustomerById(id, cancellationToken);
}