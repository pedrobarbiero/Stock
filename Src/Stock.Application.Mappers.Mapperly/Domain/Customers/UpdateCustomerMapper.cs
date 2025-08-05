using Framework.Application.Mappers;
using Riok.Mapperly.Abstractions;
using Stock.Application.Features.Customers.Requests;
using Stock.Application.Features.Customers.Responses;
using Stock.Domain.Models.Customers;

namespace Stock.Application.Mappers.Mapperly.Domain.Customers;

[Mapper]
public partial class UpdateCustomerMapper : CustomerBaseMapper, IMapper<Customer, CustomerResponse, UpdateCustomerRequest>
{
    public partial Customer ToDomain(UpdateCustomerRequest request);
}