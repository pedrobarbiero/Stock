using Framework.Domain.Models;

using Stock.Domain.Models.Customers.DomainEvents;

namespace Stock.Domain.Models.Customers;

public class Customer : AggregateRoot
{
    public required string Name { get => _name; init => _name = value; }
    private string _name = null!;

    public required string Email { get => _email; init => _email = value; }
    private string _email = null!;

    public IEnumerable<CustomerAddress> Addresses { get => _addresses; init => _addresses = value.ToList(); }
    private List<CustomerAddress> _addresses = [];

    public void Update(string name, string email, IEnumerable<CustomerAddress> addresses)
    {
        _name = name;
        _email = email;
        _addresses = addresses.ToList();
        AddDomainEvent(new CustomerUpdatedEvent(Id));
    }
}