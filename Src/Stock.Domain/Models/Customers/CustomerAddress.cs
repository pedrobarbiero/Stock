using Framework.Domain.Models;

namespace Stock.Domain.Models.Customers;

public class CustomerAddress : Entity
{
    public string? Street
    {
        get => _street;
        init => _street = value;
    }

    public string? City
    {
        get => _city;
        init => _city = value;
    }

    public string? PostalCode
    {
        get => _postalCode;
        init => _postalCode = value;
    }

    private string? _street;
    private string? _city;
    private string? _postalCode;

    public void Update(string? street, string? city, string? postalCode)
    {
        _street = street;
        _city = city;
        _postalCode = postalCode;
    }
}