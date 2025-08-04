namespace Stock.Application.Features.Suppliers.Requests;

public abstract record BaseSupplierRequest
{
    public required string Name { get; init; }
    public required string Email { get; init; }
}