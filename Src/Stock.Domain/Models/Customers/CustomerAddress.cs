using Framework.Domain.Models;

namespace Stock.Domain.Models.Customers;

public class CustomerAddress : Entity
{
    public Guid CustomerId { get; init; }
    public string? Street { get; init; }
    public string? City { get; init; }
    public string? PostalCode { get; init; }
}