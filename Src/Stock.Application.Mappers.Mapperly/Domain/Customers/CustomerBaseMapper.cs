using Riok.Mapperly.Abstractions;
using Stock.Application.Features.Customers.Requests;
using Stock.Application.Features.Customers.Responses;
using Stock.Domain.Models.Customers;

namespace Stock.Application.Mappers.Mapperly.Domain.Customers;

[Mapper]
public abstract partial class CustomerBaseMapper
{
    public partial CustomerResponse ToResponse(Customer aggregateRoot);

    [MapperIgnoreSource(nameof(CustomerAddress.CustomerId))]
    protected partial CustomerAddressResponse ToCustomerAddressResponse(CustomerAddress address);

    [MapperIgnoreTarget(nameof(CustomerAddress.Id))]
    [MapperIgnoreTarget(nameof(CustomerAddress.CustomerId))]
    protected partial CustomerAddress ToCustomerAddress(CustomerAddressRequest request);
}