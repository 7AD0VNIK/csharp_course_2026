using Marketplace.Core;

namespace AppTests.Lab3;

public class ShoppingCartTests
{
    [Test]
    public void EmptyCart_HasZeroPriceAndNoProducts()
    {
        var cart = new ShoppingCart();

        Assert.Multiple(() =>
        {
            Assert.That(cart.TotalPrice, Is.Zero);
            Assert.That(cart.GetAllProducts(), Is.Empty);
        });
    }

    [Test]
    public void AddProduct_MakesProductAvailableByIndex()
    {
        var cart = new ShoppingCart();
        var product = new Product("Мяч", 1000m, ProductCategory.None);

        cart.AddProduct(product);

        Assert.Multiple(() =>
        {
            Assert.That(cart[0], Is.SameAs(product));
            Assert.That(cart.GetAllProducts().Length, Is.EqualTo(1));
            Assert.That(cart.TotalPrice, Is.EqualTo(1000m));
        });
    }

    [TestCase(ProductCategory.None, 1000)]
    [TestCase(ProductCategory.Electronics, 950)]
    [TestCase(ProductCategory.Clothing, 900)]
    [TestCase(ProductCategory.Food, 970)]
    [TestCase(ProductCategory.Books, 930)]
    [TestCase(ProductCategory.Electronics | ProductCategory.Clothing, 900)]
    public void CategoryDiscount_AffectsTotalPrice(ProductCategory categories, int expectedPrice)
    {
        var cart = new ShoppingCart();
        cart.AddProduct(new Product("Товар", 1000m, categories));

        Assert.That(cart.TotalPrice, Is.EqualTo((decimal)expectedPrice));
    }

    [Test]
    public void MultipleProducts_TotalPriceIncludesEveryProduct()
    {
        var cart = new ShoppingCart();
        cart.AddProduct(new Product("Наушники", 1000m, ProductCategory.Electronics));
        cart.AddProduct(new Product("Футболка", 2000m, ProductCategory.Clothing));
        cart.AddProduct(new Product("Хлеб", 100m, ProductCategory.Food));

        Assert.That(cart.TotalPrice, Is.EqualTo(2847m));
    }

    [Test]
    public void GetAllProducts_ReturnsIndependentArray()
    {
        var cart = new ShoppingCart();
        var original = new Product("Первый", 100m, ProductCategory.None);
        cart.AddProduct(original);

        var products = cart.GetAllProducts();
        products[0] = new Product("Другой", 1m, ProductCategory.None);

        Assert.Multiple(() =>
        {
            Assert.That(cart[0], Is.SameAs(original));
            Assert.That(cart.TotalPrice, Is.EqualTo(100m));
        });
    }

    [Test]
    public void GetAllProducts_ReturnsSnapshot()
    {
        var cart = new ShoppingCart();
        cart.AddProduct(new Product("Первый", 100m, ProductCategory.None));

        var products = cart.GetAllProducts();
        cart.AddProduct(new Product("Второй", 200m, ProductCategory.None));

        Assert.Multiple(() =>
        {
            Assert.That(products.Length, Is.EqualTo(1));
            Assert.That(cart.GetAllProducts().Length, Is.EqualTo(2));
        });
    }

    [Test]
    public void Clear_RemovesProductsAndResetsDiscount()
    {
        var cart = new ShoppingCart();
        var marketing = new MarketingService();
        cart.AddProduct(new Product("Первый", 100m, ProductCategory.None));
        marketing.ApplySecretDiscount(cart, 50m);

        cart.Clear();

        Assert.Multiple(() =>
        {
            Assert.That(cart.GetAllProducts(), Is.Empty);
            Assert.That(cart.TotalPrice, Is.Zero);
        });

        cart.AddProduct(new Product("Второй", 100m, ProductCategory.None));

        Assert.That(cart.TotalPrice, Is.EqualTo(100m));
    }

    [Test]
    public void AddNullProduct_ThrowsException()
    {
        var cart = new ShoppingCart();

        Assert.Throws<ArgumentNullException>(() => cart.AddProduct(null!));
    }
}