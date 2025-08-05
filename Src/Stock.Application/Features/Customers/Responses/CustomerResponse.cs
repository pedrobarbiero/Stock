namespace Stock.Application.Features.Customers.Responses;

public record CustomerResponse
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public string? Email { get; init; }
    public IEnumerable<CustomerAddressResponse> Addresses { get; init; } = [];
}