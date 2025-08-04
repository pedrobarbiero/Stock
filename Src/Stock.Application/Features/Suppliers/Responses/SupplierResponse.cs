namespace Stock.Application.Features.Suppliers.Responses;

public record SupplierResponse
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public string? Email { get; init; }
}