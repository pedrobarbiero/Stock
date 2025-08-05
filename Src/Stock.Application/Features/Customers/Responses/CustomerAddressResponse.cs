namespace Stock.Application.Features.Customers.Responses;

public record CustomerAddressResponse
{
    public required Guid Id { get; init; }
    public string? Street { get; init; }
    public string? City { get; init; }
    public string? PostalCode { get; init; }
}