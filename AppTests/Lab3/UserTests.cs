using App.Lab3;

namespace AppTests.Lab3;

public class UserTests
{
    [Test]
    public void InitializesProperties()
    {
        var user = CreateUser();

        Assert.Multiple(() =>
        {
            Assert.That(user.Login, Is.EqualTo("klim"));
            Assert.That(user.PasswordHash, Is.EqualTo("hash"));
            Assert.That(user.Name, Is.EqualTo("Клим"));
            Assert.That(user.Surname, Is.EqualTo("Садов"));
            Assert.That(user.Inn, Is.EqualTo("123456789012"));
            Assert.That(user.Phone, Is.EqualTo("+79131234567"));
        });
    }

    [Test]
    public void GeneratesUniqueIdsAndRegistrationDate()
    {
        var before = DateTime.Now;
        var first = new User();
        var second = new User();
        var after = DateTime.Now;

        Assert.Multiple(() =>
        {
            Assert.That(first.Id, Is.Not.EqualTo(Guid.Empty));
            Assert.That(second.Id, Is.Not.EqualTo(first.Id));
            Assert.That(first.RegisterDate, Is.InRange(before, after));
        });
    }

    [Test]
    public void SetsProperties()
    {
        var user = new User
        {
            Login = "anna",
            Name = "Анна",
            Surname = "Иванова",
            Phone = "+79991234567"
        };

        Assert.Multiple(() =>
        {
            Assert.That(user.Login, Is.EqualTo("anna"));
            Assert.That(user.GetUserFullName(), Is.EqualTo("Анна Иванова"));
            Assert.That(user.Phone, Is.EqualTo("+79991234567"));
        });
    }

    [Test]
    public void ValidPhone_ReplacesPreviousPhone()
    {
        var user = CreateUser();

        user.Phone = "+79991234567";

        Assert.That(user.Phone, Is.EqualTo("+79991234567"));
    }

    [TestCase(null)]
    [TestCase("")]
    [TestCase("ошибка")]
    [TestCase("+7913123456")]
    [TestCase("+791312345678")]
    [TestCase("+89131234567")]
    [TestCase("+7913123456a")]
    public void InvalidPhone_PreservesPreviousPhone(string? phone)
    {
        var user = CreateUser();
        var previousPhone = user.Phone;

        user.Phone = phone!;

        Assert.That(user.Phone, Is.EqualTo(previousPhone));
    }

    [Test]
    public void ConstructorWithInvalidPhone_LeavesPhoneEmpty()
    {
        var user = new User(
            "klim", "hash", "Клим", "Садов",
            "123456789012", "ошибка");

        Assert.That(user.Phone, Is.Empty);
    }

    private static User CreateUser()
    {
        return new User(
            "klim", "hash", "Клим", "Садов",
            "123456789012", "+79131234567");
    }
}