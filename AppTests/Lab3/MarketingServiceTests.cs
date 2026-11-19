using Marketplace.Core;

namespace AppTests.Lab3;

public class MarketingServiceTests
{
    [Test]
    public void SecretDiscount_IsSubtractedAfterCategoryDiscount()
    {
        var cart = new ShoppingCart();
        cart.AddProduct(new Product("Наушники", 1000m, ProductCategory.Electronics));
        var marketing = new MarketingService();

        marketing.ApplySecretDiscount(cart, 200m);

        Assert.That(cart.TotalPrice, Is.EqualTo(750m));
    }

    [Test]
    public void RepeatedDiscount_ReplacesPreviousDiscount()
    {
        var cart = new ShoppingCart();
        cart.AddProduct(new Product("Товар", 1000m, ProductCategory.None));
        var marketing = new MarketingService();

        marketing.ApplySecretDiscount(cart, 200m);
        marketing.ApplySecretDiscount(cart, 100m);

        Assert.That(cart.TotalPrice, Is.EqualTo(900m));
    }

    [Test]
    public void ExcessiveDiscount_DoesNotMakePriceNegative()
    {
        var cart = new ShoppingCart();
        cart.AddProduct(new Product("Товар", 100m, ProductCategory.None));
        var marketing = new MarketingService();

        marketing.ApplySecretDiscount(cart, 1000m);

        Assert.That(cart.TotalPrice, Is.Zero);
    }

    [Test]
    public void NegativeDiscount_ThrowsExceptionAndPreservesPrice()
    {
        var cart = new ShoppingCart();
        cart.AddProduct(new Product("Товар", 100m, ProductCategory.None));
        var marketing = new MarketingService();

        Assert.Throws<ArgumentOutOfRangeException>(() =>
            marketing.ApplySecretDiscount(cart, -1m));

        Assert.That(cart.TotalPrice, Is.EqualTo(100m));
    }

    [Test]
    public void NullCart_ThrowsException()
    {
        var marketing = new MarketingService();

        Assert.Throws<ArgumentNullException>(() =>
            marketing.ApplySecretDiscount(null!, 100m));
    }
}