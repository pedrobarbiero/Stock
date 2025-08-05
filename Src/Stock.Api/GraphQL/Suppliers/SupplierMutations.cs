using Framework.Application.Requests;
using Stock.Application.Features.Suppliers.Services;
using Stock.Application.Features.Suppliers.Requests;
using Stock.Application.Features.Suppliers.Responses;

namespace Stock.Api.GraphQL.Suppliers;

[ExtendObjectType("Mutation")]
public class SupplierMutations
{
    public Task<RequestResult<SupplierResponse>> CreateSupplier(
        CreateSupplierRequest input,
        [Service] ISupplierService supplierService)
        => supplierService.CreateAsync(input, CancellationToken.None);

    public Task<RequestResult<SupplierResponse>> UpdateSupplier(
        Guid id,
        UpdateSupplierRequest input,
        [Service] ISupplierService supplierService)
        => supplierService.UpdateAsync(input, CancellationToken.None);


    public Task<RequestResult<bool>> DeleteSupplier(
        Guid id,
        [Service] ISupplierService supplierService)
        => supplierService.DeleteAsync(id, CancellationToken.None);
}