using Framework.Application.Requests;

namespace Stock.Application.Features.Customers.Requests;

public record UpdateCustomerRequest : BaseCustomerRequest, IUpdateRequest
{
    public required Guid Id { get; init; }
}