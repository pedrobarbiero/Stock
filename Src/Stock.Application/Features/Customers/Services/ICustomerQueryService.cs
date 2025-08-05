using Stock.Application.Features.Customers.QueryResults;

namespace Stock.Application.Features.Customers.Services;

public interface ICustomerQueryService
{
    IQueryable<CustomerQueryResult> GetCustomers();
    Task<CustomerQueryResult?> GetCustomerById(Guid id, CancellationToken cancellationToken);
}