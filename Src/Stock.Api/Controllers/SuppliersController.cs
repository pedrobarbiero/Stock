using Framework.Application.Requests;
using Microsoft.AspNetCore.Mvc;
using Stock.Application.Features.Suppliers.Requests;
using Stock.Application.Features.Suppliers.Responses;
using Stock.Application.Features.Suppliers.Services;

namespace Stock.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SuppliersController : ControllerBase
{
    [HttpPost]
    public Task<RequestResult<SupplierResponse>> Create(
        [FromServices] ISupplierService supplierService,
        [FromBody] CreateSupplierRequest request,
        CancellationToken cancellationToken)
        => supplierService.CreateAsync(request, cancellationToken);

    [HttpPut]
    public Task<RequestResult<SupplierResponse>> Update(
        [FromServices] ISupplierService supplierService,
        [FromBody] UpdateSupplierRequest request, CancellationToken cancellationToken)
        => supplierService.UpdateAsync(request, cancellationToken);

    [HttpDelete("{id:guid}")]
    public Task<RequestResult<bool>> Delete(
        [FromServices] ISupplierService supplierService,
        [FromRoute] Guid id, CancellationToken cancellationToken)
        => supplierService.DeleteAsync(id, cancellationToken);
}