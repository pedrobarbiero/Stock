using Framework.Application.Mappers;
using Framework.Application.Services;
using Stock.Application.Features.Customers.Requests;
using Stock.Application.Features.Customers.Responses;
using Stock.Domain.Models.Customers;

namespace Stock.Application.Features.Customers.Services;

public class CustomerService(
    IServiceProvider serviceProvider,
    IMapper<Customer, CustomerResponse, CreateCustomerRequest> createMapper,
    IMapper<Customer, CustomerResponse, UpdateCustomerRequest> updateMapper)
    : BaseService<Customer, CreateCustomerRequest, UpdateCustomerRequest, CustomerResponse>(serviceProvider),
        ICustomerService
{
    protected override Customer ToDomain(CreateCustomerRequest request) => createMapper.ToDomain(request);

    protected override Customer UpdateDomain(UpdateCustomerRequest request, Customer existingAggregateRoot)
    {
        var updatedCustomer = updateMapper.ToDomain(request);
        existingAggregateRoot.Update(updatedCustomer.Name, updatedCustomer.Email, updatedCustomer.Addresses);
        return existingAggregateRoot;
    }
}