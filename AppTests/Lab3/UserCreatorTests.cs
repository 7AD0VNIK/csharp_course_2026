using App.Lab3;

namespace AppTests.Lab3;

public class UserCreatorTests
{
    [TestCase("123", "202CB962AC59075B964B07152D234B70")]
    [TestCase("", "D41D8CD98F00B204E9800998ECF8427E")]
    public void CreateUser_StoresExpectedPasswordHash(string password, string expectedHash)
    {
        var user = UserCreator.CreateUser(
            "klim", password, "Клим", "Садов",
            "123456789012", "+79131234567");

        Assert.That(user.PasswordHash, Is.EqualTo(expectedHash));
    }

    [Test]
    public void CreateUser_PreservesUserDetails()
    {
        var user = UserCreator.CreateUser(
            "klim", "123", "Клим", "Садов",
            "123456789012", "+79131234567");

        Assert.Multiple(() =>
        {
            Assert.That(user.Login, Is.EqualTo("klim"));
            Assert.That(user.GetUserFullName(), Is.EqualTo("Клим Садов"));
            Assert.That(user.Inn, Is.EqualTo("123456789012"));
            Assert.That(user.Phone, Is.EqualTo("+79131234567"));
        });
    }
}