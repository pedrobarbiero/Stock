using Stock.Application.Features.Customers.Services;
using Stock.Application.Features.Customers.Requests;
using Stock.Application.Features.Customers.Responses;

namespace Stock.Api.GraphQL.Customers;

[ExtendObjectType("Mutation")]
public class CustomerMutations
{
    public async Task<CustomerResponse?> CreateCustomer(
        CreateCustomerRequest input,
        [Service] ICustomerService customerService)
    {
        var result = await customerService.CreateAsync(input, CancellationToken.None);
        return result.IsValid ? result.Data : null;
    }

    public async Task<CustomerResponse?> UpdateCustomer(
        UpdateCustomerRequest input,
        [Service] ICustomerService customerService)
    {
        var result = await customerService.UpdateAsync(input, CancellationToken.None);
        return result.IsValid ? result.Data : null;
    }

    public async Task<bool> DeleteCustomer(
        Guid id,
        [Service] ICustomerService customerService)
    {
        var result = await customerService.DeleteAsync(id, CancellationToken.None);
        return result.IsValid;
    }
}