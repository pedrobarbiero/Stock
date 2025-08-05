using Framework.Application.Mappers;
using Riok.Mapperly.Abstractions;
using Stock.Application.Features.Customers.Requests;
using Stock.Application.Features.Customers.Responses;
using Stock.Domain.Models.Customers;

namespace Stock.Application.Mappers.Mapperly.Domain.Customers;

[Mapper]
public partial class CreateCustomerMapper : IMapper<Customer, CustomerResponse, CreateCustomerRequest>
{
    public partial CustomerResponse ToResponse(Customer aggregateRoot);

    [MapperIgnoreTarget(nameof(Customer.Id))]
    public partial Customer ToDomain(CreateCustomerRequest request);

    [MapperIgnoreSource(nameof(CustomerAddress.CustomerId))]
    private partial CustomerAddressResponse ToCustomerAddressResponse(CustomerAddress address);

    [MapperIgnoreTarget(nameof(CustomerAddress.Id))]
    [MapperIgnoreTarget(nameof(CustomerAddress.CustomerId))]
    private partial CustomerAddress ToCustomerAddress(CustomerAddressRequest request);
}