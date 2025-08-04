using Framework.Application.Mappers;
using Riok.Mapperly.Abstractions;
using Stock.Application.Features.Suppliers.Requests;
using Stock.Application.Features.Suppliers.Responses;
using Stock.Domain.Models.Suppliers;

namespace Stock.Application.Mappers.Mapperly.Domain.Suppliers;

[Mapper]
public partial class
    CreateSupplierMapper : IMapper<Supplier, SupplierResponse, CreateSupplierRequest>
{
    public partial SupplierResponse ToResponse(Supplier aggregateRoot);

    [MapperIgnoreTarget(nameof(Supplier.Id))]
    public partial Supplier ToDomain(CreateSupplierRequest response);
}