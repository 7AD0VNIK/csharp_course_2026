using App.Lab3;

namespace AppTests.Lab3;

public class UserTests
{
    [Test]
    public void GetUserFullName_ReturnsNameAndSurname()
    {
        var user = CreateUser();

        Assert.That(user.GetUserFullName(), Is.EqualTo("Клим Садов"));
    }
    
    [TestCase("+78931319254", true)]
    [TestCase("123", false)]
    [TestCase("", false)]
    [TestCase(null, false)]
    [TestCase("+7913123456", false)]
    [TestCase("+791312345678", false)]
    [TestCase("+89131234567", false)]
    [TestCase("+7913123456a", false)]
    public void TryUpdatePhone_ReturnsValidationResult(string? phone, bool expected)
    {
        var user = CreateUser();

        var result = user.TryUpdatePhone(phone!);

        Assert.That(result, Is.EqualTo(expected));
    }

    private static User CreateUser()
    {
        return new User(
            "klim", "123", "Клим", "Садов",
            "123456789012", "+79131234567");
    }
}