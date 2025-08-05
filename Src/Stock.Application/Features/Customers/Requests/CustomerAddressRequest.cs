namespace Stock.Application.Features.Customers.Requests;

public record CustomerAddressRequest
{
    public string? Street { get; init; }
    public string? City { get; init; }
    public string? PostalCode { get; init; }
}