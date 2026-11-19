using Marketplace.Core;

namespace AppTests.Lab3;

public class ProductTests
{
    [Test]
    public void ConstructorWithValidArguments_ShouldPreserveValuesAndGenerateNonEmptyId()
    {
        var categories = ProductCategory.Electronics | ProductCategory.Books;
        var product = new Product("Электронная книга", 1000m, categories);

        Assert.Multiple(() =>
        {
            Assert.That(product.Id, Is.Not.EqualTo(Guid.Empty));
            Assert.That(product.Name, Is.EqualTo("Электронная книга"));
            Assert.That(product.Price, Is.EqualTo(1000m));
            Assert.That(product.Categories, Is.EqualTo(categories));
        });
    }

    [Test]
    public void Constructor_GeneratesUniqueIds()
    {
        var first = new Product("Первый", 100m, ProductCategory.None);
        var second = new Product("Второй", 100m, ProductCategory.None);

        Assert.That(first.Id, Is.Not.EqualTo(second.Id));
    }

    [Test]
    public void NegativePrice_ThrowsException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            new Product("Товар", -1m, ProductCategory.None));
    }

    [Test]
    public void ZeroPrice_IsAllowed()
    {
        var product = new Product("Подарок", 0m, ProductCategory.None);

        Assert.That(product.Price, Is.Zero);
    }
}