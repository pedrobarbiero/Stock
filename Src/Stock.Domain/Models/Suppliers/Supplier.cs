using Framework.Domain.Models;

namespace Stock.Domain.Models.Suppliers;

public class Supplier : AggregateRoot
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

    private string _name = null!;
    private string _email = null!;

    public Supplier Update(string name, string email)
    {
        _name = name;
        _email = email;
        return this;
    }
}