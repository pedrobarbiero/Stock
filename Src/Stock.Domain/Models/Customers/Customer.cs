using Framework.Domain.Models;

namespace Stock.Domain.Models.Customers;

public class Customer : AggregateRoot
{
    public required string Name
    {
        get => _name;
        init => _name = value;
    }

    public required string Email
    {
        get => _email;
        init => _email = value;
    }

    public IEnumerable<CustomerAddress> Addresses
    {
        get => _addresses;
        init => _addresses = value.ToList();
    }

    private string _name = null!;
    private string _email = null!;
    private List<CustomerAddress> _addresses = [];

    public void Update(string name, string email, IEnumerable<CustomerAddress> addresses)
    {
        _name = name;
        _email = email;
        _addresses = addresses.ToList();
    }
}