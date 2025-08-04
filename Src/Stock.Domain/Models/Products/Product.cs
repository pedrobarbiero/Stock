using Framework.Domain.CustomTypes;
using Framework.Domain.Models;

namespace Stock.Domain.Models.Products;

public class Product : AggregateRoot
{
    public required string Name
    {
        get => _name;
        init => _name = value;
    }

    public string? Sku
    {
        get => _sku;
        init => _sku = value;
    }

    public required PositiveDecimal Price
    {
        get => _price;
        init => _price = value;
    }

    public Guid SupplierId
    {
        get => _supplierId;
        init => _supplierId = value;
    }

    private string _name = null!;
    private string? _sku;
    private PositiveDecimal _price;
    private Guid _supplierId;

    public void Update(string name, PositiveDecimal price, Guid supplierId, string? sku)
    {
        _name = name;
        _price = price;
        _supplierId = supplierId;
        _sku = sku;
    }
}