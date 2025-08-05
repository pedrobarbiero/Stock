using Stock.Application.Features.Suppliers.Services;
using Stock.Application.Features.Suppliers.Requests;
using Stock.Application.Features.Suppliers.Responses;

namespace Stock.Api.GraphQL.Suppliers;

[ExtendObjectType("Mutation")]
public class SupplierMutations
{
    public async Task<SupplierResponse?> CreateSupplier(
        CreateSupplierRequest input,
        [Service] ISupplierService supplierService)
    {
        var result = await supplierService.CreateAsync(input, CancellationToken.None);
        return result.IsValid ? result.Data : null;
    }

    public async Task<SupplierResponse?> UpdateSupplier(
        Guid id,
        UpdateSupplierRequest input,
        [Service] ISupplierService supplierService)
    {
        var result = await supplierService.UpdateAsync(input, CancellationToken.None);
        return result.IsValid ? result.Data : null;
    }

    public async Task<bool> DeleteSupplier(
        Guid id,
        [Service] ISupplierService supplierService)
    {
        var result = await supplierService.DeleteAsync(id, CancellationToken.None);
        return result.IsValid;
    }
}