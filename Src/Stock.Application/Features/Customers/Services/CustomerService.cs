using Framework.Application.Services;
using Stock.Application.Features.Customers.Requests;
using Stock.Application.Features.Customers.Responses;
using Stock.Domain.Models.Customers;

namespace Stock.Application.Features.Customers.Services;

public class CustomerService(IServiceProvider serviceProvider)
    : BaseService<Customer, CreateCustomerRequest, UpdateCustomerRequest, CustomerResponse>(serviceProvider),
        ICustomerService
{
    protected override Customer ToDomain(CreateCustomerRequest request)
    {
        var addresses = request.Addresses.Select(a => new CustomerAddress
        {
            Street = a.Street,
            City = a.City,
            PostalCode = a.PostalCode
        });

        return new Customer
        {
            Name = request.Name,
            Email = request.Email,
            Addresses = addresses
        };
    }

    protected override Customer UpdateDomain(UpdateCustomerRequest request, Customer existingAggregateRoot)
    {
        var addresses = request.Addresses.Select(a => new CustomerAddress
        {
            Street = a.Street,
            City = a.City,
            PostalCode = a.PostalCode
        });

        existingAggregateRoot.Update(request.Name, request.Email, addresses);
        return existingAggregateRoot;
    }
}