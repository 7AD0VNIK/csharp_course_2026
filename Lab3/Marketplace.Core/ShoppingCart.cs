namespace Marketplace.Core;

public class ShoppingCart
{
    private readonly List<Product> products = new();
    private decimal discount;

    public Product this[int index] => products[index];

    public decimal TotalPrice
    {
        get
        {
            var total = 0m;

            foreach (var product in products)
            {
                var percent = GetDiscountPercent(product.Categories);
                total += product.Price * (1 - percent / 100m);
            }

            return Math.Max(0m, total - discount);
        }
    }

    public void AddProduct(Product product)
    {
        if (product == null)
            throw new ArgumentNullException(nameof(product));

        products.Add(product);
    }

    public void Clear()
    {
        products.Clear();
        discount = 0;
    }

    internal void SetDiscount(decimal amount)
    {
        if (amount < 0)
            throw new ArgumentOutOfRangeException(nameof(amount));

        discount = amount;
    }

    public Product[] GetAllProducts()
    {
        var productsCopy = new Product[products.Count];

        for (var i = 0; i < products.Count; i++)
            productsCopy[i] = products[i];

        return productsCopy;
    }

    private static decimal GetDiscountPercent(ProductCategory categories)
    {
        var percent = 0m;

        if ((categories & ProductCategory.Electronics) != 0)
            percent = Math.Max(percent, 5m);

        if ((categories & ProductCategory.Clothing) != 0)
            percent = Math.Max(percent, 10m);

        if ((categories & ProductCategory.Food) != 0)
            percent = Math.Max(percent, 3m);

        if ((categories & ProductCategory.Books) != 0)
            percent = Math.Max(percent, 7m);

        return percent;
    }
}