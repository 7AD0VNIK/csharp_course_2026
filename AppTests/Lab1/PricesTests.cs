using App.Lab1;

namespace AppTests.Lab1;

public class PricesTests
{
    //склонения
    [TestCase(1000052, "рубля")]
    [TestCase(25, "рублей")]
    [TestCase(3, "рубля")]
    [TestCase(20, "рублей")]
    [TestCase(23, "рубля")]
    [TestCase(22, "рубля")]
    [TestCase(27, "рублей")]
    [TestCase(122, "рубля")]
    [TestCase(28, "рублей")]
    [TestCase(7, "рублей")]
    [TestCase(126, "рублей")]
    [TestCase(24, "рубля")]
    [TestCase(1002, "рубля")]
    [TestCase(1, "рубль")]
    [TestCase(1000, "рублей")]
    [TestCase(6, "рублей")]
    [TestCase(1000055, "рублей")]
    [TestCase(100008, "рублей")]
    [TestCase(100009, "рублей")]
    [TestCase(26, "рублей")]
    [TestCase(1004, "рубля")]
    [TestCase(8, "рублей")]
    [TestCase(4, "рубля")]
    [TestCase(29, "рублей")]
    [TestCase(9, "рублей")]
    [TestCase(1005, "рублей")]
    [TestCase(2, "рубля")]
    [TestCase(5, "рублей")]
    [TestCase(111, "рублей")]
    public void TestPasses_When_Result_Correct(int price, string expected)
    {
        var actual = Prices.GetCurrencyAlias(price, false, false);

        Assert.That(actual, Is.EqualTo(expected));
    }

    // 11-14
    [TestCase(11, "рублей")]
    [TestCase(12, "рублей")]
    [TestCase(13, "рублей")]
    [TestCase(14, "рублей")]
    [TestCase(111, "рублей")]
    [TestCase(112, "рублей")]
    [TestCase(113, "рублей")]
    [TestCase(114, "рублей")]
    [TestCase(1011, "рублей")]
    public void TestPasses_When_11_To_14(int price, string expected)
    {
        var actual = Prices.GetCurrencyAlias(price, false, false);

        Assert.That(actual, Is.EqualTo(expected));
    }

    // загалвная буква
    [TestCase(1, "Рубль")]
    [TestCase(2, "Рубля")]
    [TestCase(5, "Рублей")]
    [TestCase(11, "Рублей")]
    [TestCase(22, "Рубля")]
    public void TestPasses_When_First_Capital(int price, string expected)
    {
        var actual = Prices.GetCurrencyAlias(price, false, true);

        Assert.That(actual, Is.EqualTo(expected));
    }

    // короткая нотация 
    [TestCase(1, "руб.")]
    [TestCase(2, "руб.")]
    [TestCase(5, "руб.")]
    [TestCase(11, "руб.")]
    [TestCase(122, "руб.")]
    public void TestPasses_When_Short_Notation(int price, string expected)
    {
        var actual = Prices.GetCurrencyAlias(price, true, false);

        Assert.That(actual, Is.EqualTo(expected));
    }

    // короткая нотация + заглавная буква
    [TestCase(1, "Руб.")]
    [TestCase(2, "Руб.")]
    [TestCase(11, "Руб.")]
    [TestCase(122, "Руб.")]
    public void TestPasses_When_Short_And_Capital(int price, string expected)
    {
        var actual = Prices.GetCurrencyAlias(price, true, true);

        Assert.That(actual, Is.EqualTo(expected));
    }
}