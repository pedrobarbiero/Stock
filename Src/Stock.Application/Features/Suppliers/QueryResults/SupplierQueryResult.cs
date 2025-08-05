namespace Stock.Application.Features.Suppliers.QueryResults;

public class SupplierQueryResult
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Email { get; init; }
}