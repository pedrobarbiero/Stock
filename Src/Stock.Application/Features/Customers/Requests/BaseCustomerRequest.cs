namespace Stock.Application.Features.Customers.Requests;

public abstract record BaseCustomerRequest
{
    public required string Name { get; init; }
    public required string Email { get; init; }
    public IEnumerable<CustomerAddressRequest> Addresses { get; init; } = [];
}