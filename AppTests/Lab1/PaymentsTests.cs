using App;
using App.Lab1;

namespace AppTests.Lab1;

public class PaymentsTests
{
    [TestCase(PaymentsPlan.Annuity, 7, 3, 10000, 10116.89)]
    [TestCase(PaymentsPlan.Differentiated, 3, 5, 200000, 201500)]

    // один месяц
    [TestCase(PaymentsPlan.Annuity, 12, 1, 10000, 10100)]
    [TestCase(PaymentsPlan.Differentiated, 12, 1, 10000, 10100)]

    // 12 месяцев
    [TestCase(PaymentsPlan.Annuity, 12, 12, 120000, 127942.26)]
    [TestCase(PaymentsPlan.Differentiated, 12, 12, 120000, 127800)]

    // небольшая ставка
    [TestCase(PaymentsPlan.Annuity, 1, 12, 100000, 100542.49)]
    [TestCase(PaymentsPlan.Differentiated, 1, 12, 100000, 100541.67)]

    // больший срок
    [TestCase(PaymentsPlan.Annuity, 10, 24, 500000, 553739.12)]
    [TestCase(PaymentsPlan.Differentiated, 10, 24, 500000, 552083.33)]
    
    [TestCase(PaymentsPlan.Annuity, 0, 12, 120000, 120000)]
    [TestCase(PaymentsPlan.Differentiated, 0, 12, 120000, 120000)]

    public void TestPasses_When_Result_Correct(
        PaymentsPlan plan,
        decimal rate,
        int monthsCount,
        decimal amount,
        decimal expected)
    {
        var actual = Payments.CalculateTotalPayments(
            plan,
            rate,
            monthsCount,
            amount);

        Assert.That(actual, Is.EqualTo(expected));
    }
}