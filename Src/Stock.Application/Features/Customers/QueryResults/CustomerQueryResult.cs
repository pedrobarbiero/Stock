namespace Stock.Application.Features.Customers.QueryResults;

public class CustomerQueryResult
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Email { get; init; }
    public required IEnumerable<CustomerAddressQueryResult> Addresses { get; init; } = [];
}