using Framework.Application.Mappers;
using Riok.Mapperly.Abstractions;
using Stock.Application.Features.Customers.Requests;
using Stock.Application.Features.Customers.Responses;
using Stock.Domain.Models.Customers;

namespace Stock.Application.Mappers.Mapperly.Domain.Customers;

[Mapper]
public partial class UpdateCustomerMapper : IMapper<Customer, CustomerResponse, UpdateCustomerRequest>
{
    public partial CustomerResponse ToResponse(Customer aggregateRoot);
    public partial Customer ToDomain(UpdateCustomerRequest request);

    [MapperIgnoreSource(nameof(CustomerAddress.CustomerId))]
    private partial CustomerAddressResponse ToCustomerAddressResponse(CustomerAddress address);

    [MapperIgnoreTarget(nameof(CustomerAddress.Id))]
    [MapperIgnoreTarget(nameof(CustomerAddress.CustomerId))]
    private partial CustomerAddress ToCustomerAddress(CustomerAddressRequest request);
}