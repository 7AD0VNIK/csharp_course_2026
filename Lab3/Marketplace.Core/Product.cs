namespace Marketplace.Core;

public class Product
{
    public Guid Id { get; }
    public string Name { get; }
    public decimal Price { get; }
    public ProductCategory Categories { get; }

    public Product(string name, decimal price, ProductCategory categories)
    {
        if (price < 0)
            throw new ArgumentOutOfRangeException(nameof(price));

        Id = Guid.NewGuid();
        Name = name;
        Price = price;
        Categories = categories;
    }
}