using Framework.Application.Requests;
using Stock.Application.Features.Customers.Services;
using Stock.Application.Features.Customers.Requests;
using Stock.Application.Features.Customers.Responses;

namespace Stock.Api.GraphQL.Customers;

[ExtendObjectType("Mutation")]
public class CustomerMutations
{
    public Task<RequestResult<CustomerResponse>> CreateCustomer(
        CreateCustomerRequest input,
        [Service] ICustomerService customerService)
    {
        return customerService.CreateAsync(input, CancellationToken.None);
    }

    public Task<RequestResult<CustomerResponse>> UpdateCustomer(
        UpdateCustomerRequest input,
        [Service] ICustomerService customerService)
    {
        return customerService.UpdateAsync(input, CancellationToken.None);
    }

    public Task<RequestResult<bool>> DeleteCustomer(
        Guid id,
        [Service] ICustomerService customerService)
        => customerService.DeleteAsync(id, CancellationToken.None);
}