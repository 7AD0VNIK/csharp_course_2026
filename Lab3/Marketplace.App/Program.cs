using Marketplace.Core;

namespace Marketplace.App;

public static class Program
{
    public static void Main()
    {
        var user = UserCreator.CreateUser(
            "klim", "123", "Клим", "Садов",
            "123456789012", "+79131234567");

        Console.WriteLine(user.GetUserFullName());

        var headphones = new Product("Наушники", 1000m, ProductCategory.Electronics);
        var shirt = new Product("Футболка", 2000m, ProductCategory.Clothing);
        var bread = new Product("Хлеб", 100m, ProductCategory.Food);
        var cart = new ShoppingCart();

        cart.AddProduct(headphones);
        cart.AddProduct(shirt);
        cart.AddProduct(bread);

        Console.WriteLine(cart[0].Name);  // Наушники
        Console.WriteLine($"Сумма: {cart.TotalPrice}"); // 2847

        var marketing = new MarketingService();
        marketing.ApplySecretDiscount(cart, 200m);

        Console.WriteLine($"Со скидкой: {cart.TotalPrice}"); // 2647

        var products = cart.GetAllProducts();
        products[0] = new Product("Другой товар", 1m, ProductCategory.None);

        Console.WriteLine(cart[0].Name); // По-прежнему Наушники

        marketing.ApplySecretDiscount(cart, 10000m);
        Console.WriteLine(cart.TotalPrice); // 0

        cart.Clear();
        Console.WriteLine(cart.GetAllProducts().Length); // 0

        cart.AddProduct(bread);
        Console.WriteLine(cart.TotalPrice); // 97: Clear сбросил скидку

        // для проверки ограничений:
        // headphones.Price = 1m;
        // cart[0] = bread;
        // cart.TotalPrice = 1m;
        // cart.SetDiscount(100m);
    }
}