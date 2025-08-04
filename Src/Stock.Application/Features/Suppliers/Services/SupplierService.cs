using Framework.Application.Services;
using Stock.Application.Features.Suppliers.Requests;
using Stock.Application.Features.Suppliers.Responses;
using Stock.Domain.Models.Suppliers;

namespace Stock.Application.Features.Suppliers.Services;

public class SupplierService(IServiceProvider serviceProvider)
    : BaseService<Supplier, CreateSupplierRequest, UpdateSupplierRequest, SupplierResponse>(serviceProvider),
        ISupplierService
{
    protected override Supplier UpdateDomain(UpdateSupplierRequest request, Supplier existingAggregateRoot)
        => existingAggregateRoot.Update(request.Name, request.Email);
}