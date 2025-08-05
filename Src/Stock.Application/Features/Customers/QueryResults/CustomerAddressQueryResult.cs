namespace Stock.Application.Features.Customers.QueryResults;

public class CustomerAddressQueryResult
{
    public Guid Id { get; init; }
    public string? Street { get; init; }
    public string? City { get; init; }
    public string? PostalCode { get; init; }
}