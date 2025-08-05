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
        [Service] ISupplierService supplierService,
        CancellationToken cancellationToken)
        => supplierService.CreateAsync(input, cancellationToken);

    public Task<RequestResult<SupplierResponse>> UpdateSupplier(
        Guid id,
        UpdateSupplierRequest input,
        [Service] ISupplierService supplierService,
        CancellationToken cancellationToken)
        => supplierService.UpdateAsync(input, cancellationToken);


    public Task<RequestResult<bool>> DeleteSupplier(
        Guid id,
        [Service] ISupplierService supplierService,
        CancellationToken cancellationToken)
        => supplierService.DeleteAsync(id, cancellationToken);
}