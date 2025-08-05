using Framework.Application.Mappers;
using Riok.Mapperly.Abstractions;
using Stock.Application.Features.Customers.Requests;
using Stock.Application.Features.Customers.Responses;
using Stock.Domain.Models.Customers;

namespace Stock.Application.Mappers.Mapperly.Domain.Customers;

[Mapper]
public partial class CreateCustomerMapper : CustomerBaseMapper, IMapper<Customer, CustomerResponse, CreateCustomerRequest>
{
    [MapperIgnoreTarget(nameof(Customer.Id))]
    public partial Customer ToDomain(CreateCustomerRequest request);
}