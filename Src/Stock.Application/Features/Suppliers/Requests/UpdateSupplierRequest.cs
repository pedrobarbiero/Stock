using Framework.Application.Requests;

namespace Stock.Application.Features.Suppliers.Requests;

public record UpdateSupplierRequest : BaseSupplierRequest, IUpdateRequest
{
    public required Guid Id { get; init; }
}