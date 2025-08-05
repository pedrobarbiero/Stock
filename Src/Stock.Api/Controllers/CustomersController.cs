using Framework.Application.Requests;
using Microsoft.AspNetCore.Mvc;
using Stock.Application.Features.Customers.Requests;
using Stock.Application.Features.Customers.Responses;
using Stock.Application.Features.Customers.Services;

namespace Stock.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    [HttpPost]
    public Task<RequestResult<CustomerResponse>> Create(
        [FromServices] ICustomerService customerService,
        [FromBody] CreateCustomerRequest request,
        CancellationToken cancellationToken)
        => customerService.CreateAsync(request, cancellationToken);

    [HttpPut]
    public Task<RequestResult<CustomerResponse>> Update(
        [FromServices] ICustomerService customerService,
        [FromBody] UpdateCustomerRequest request, CancellationToken cancellationToken)
        => customerService.UpdateAsync(request, cancellationToken);

    [HttpDelete("{id:guid}")]
    public Task<RequestResult<bool>> Delete(
        [FromServices] ICustomerService customerService,
        [FromRoute] Guid id, CancellationToken cancellationToken)
        => customerService.DeleteAsync(id, cancellationToken);
}