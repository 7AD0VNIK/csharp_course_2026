using Lab4.Task3.Dtos;
using Lab4.Task3.Services;

namespace Lab4.Task3.Tests;

public class UsersServiceTests
{
    [Test]
    public void CreateUpdateDelete_WithValidData_PreservesValuesAndRemovesUser()
    {
        IUsersService service = new UsersService();
        var userId = service.CreateUser(new CreateUserDto
        {
            Login = "klim",
            Password = "123",
            Name = "Клим",
            Surname = "Садов",
            Inn = "123456789012",
            Phone = "+79131234567"
        });

        var user = service.GetUser(userId);
        Assert.Multiple(() =>
        {
            Assert.That(userId, Is.Not.EqualTo(Guid.Empty));
            Assert.That(user.Id, Is.EqualTo(userId));
            Assert.That(user.Login, Is.EqualTo("klim"));
            Assert.That(user.Name, Is.EqualTo("Клим"));
            Assert.That(user.Surname, Is.EqualTo("Садов"));
            Assert.That(user.Inn, Is.EqualTo("123456789012"));
            Assert.That(user.Phone, Is.EqualTo("+79131234567"));
        });

        service.UpdateUser(userId, new UpdateUserDto
        {
            Name = "Анна",
            Surname = "Иванова",
            Inn = "987654321012",
            Phone = "+79991234567"
        });

        var updatedUser = service.GetUser(userId);
        Assert.Multiple(() =>
        {
            Assert.That(updatedUser.Id, Is.EqualTo(userId));
            Assert.That(updatedUser.Login, Is.EqualTo("klim"));
            Assert.That(updatedUser.Name, Is.EqualTo("Анна"));
            Assert.That(updatedUser.Surname, Is.EqualTo("Иванова"));
            Assert.That(updatedUser.Inn, Is.EqualTo("987654321012"));
            Assert.That(updatedUser.Phone, Is.EqualTo("+79991234567"));
            Assert.That(service.LogIn("klim", "123"), Is.EqualTo(userId));
        });

        service.DeleteUser(userId);

        Assert.Multiple(() =>
        {
            Assert.That(() => service.GetUser(userId), Throws.TypeOf<Exception>());
            Assert.That(() => service.LogIn("klim", "123"), Throws.TypeOf<Exception>());
        });
    }

    [Test]
    public void ChangePassword_WithCorrectOldPassword_AllowsOnlyNewPassword()
    {
        var service = new UsersService();
        var userId = CreateUser(service);

        service.ChangePassword(userId, "123", "456");

        Assert.Multiple(() =>
        {
            Assert.That(() => service.LogIn("klim", "123"), Throws.TypeOf<Exception>());
            Assert.That(service.LogIn("klim", "456"), Is.EqualTo(userId));
        });
    }

    [Test]
    public void ChangePassword_WithWrongOldPassword_KeepsCurrentPassword()
    {
        var service = new UsersService();
        var userId = CreateUser(service);

        Assert.That(
            () => service.ChangePassword(userId, "wrong", "456"),
            Throws.TypeOf<Exception>().With.Message.EqualTo("Старый пароль неверен."));
        Assert.That(service.LogIn("klim", "123"), Is.EqualTo(userId));
    }

    [TestCase(null)]
    [TestCase("")]
    [TestCase(" ")]
    public void ChangePassword_WithMissingNewPassword_KeepsCurrentPassword(string? newPassword)
    {
        var service = new UsersService();
        var userId = CreateUser(service);

        Assert.That(
            () => service.ChangePassword(userId, "123", newPassword!),
            Throws.TypeOf<Exception>());
        Assert.That(service.LogIn("klim", "123"), Is.EqualTo(userId));
    }

    [TestCase(null, "123")]
    [TestCase("", "123")]
    [TestCase(" ", "123")]
    [TestCase("klim", null)]
    [TestCase("klim", "")]
    [TestCase("klim", " ")]
    public void CreateUser_ThrowsException(string? login, string? password)
    {
        var service = new UsersService();
        var data = new CreateUserDto { Login = login, Password = password };

        Assert.That(() => service.CreateUser(data), Throws.TypeOf<Exception>());
    }

    [Test]
    public void CreateUser_WithDuplicateLogin_ThrowsException()
    {
        var service = new UsersService();
        var userId = CreateUser(service);
        var duplicate = new CreateUserDto { Login = "klim", Password = "456" };

        Assert.That(
            () => service.CreateUser(duplicate),
            Throws.TypeOf<Exception>().With.Message.EqualTo("Этот логин уже занят."));
        Assert.That(service.LogIn("klim", "123"), Is.EqualTo(userId));
    }

    [Test]
    public void CreateUser_UsesEmptyOptionalValues()
    {
        var service = new UsersService();
        var userId = CreateUser(service);
        var user = service.GetUser(userId);

        Assert.Multiple(() =>
        {
            Assert.That(user.Name, Is.Empty);
            Assert.That(user.Surname, Is.Empty);
            Assert.That(user.Inn, Is.Empty);
            Assert.That(user.Phone, Is.Empty);
        });
    }

    [Test]
    public void UpdateUser_WithNullFields_PreservesPreviousValues()
    {
        var service = new UsersService();
        var userId = service.CreateUser(new CreateUserDto
        {
            Login = "klim",
            Password = "123",
            Name = "Клим",
            Surname = "Садов",
            Inn = "123456789012",
            Phone = "+79131234567"
        });

        service.UpdateUser(userId, new UpdateUserDto { Name = "Алексей" });
        service.UpdateUser(userId, new UpdateUserDto());

        var user = service.GetUser(userId);
        Assert.Multiple(() =>
        {
            Assert.That(user.Name, Is.EqualTo("Алексей"));
            Assert.That(user.Surname, Is.EqualTo("Садов"));
            Assert.That(user.Inn, Is.EqualTo("123456789012"));
            Assert.That(user.Phone, Is.EqualTo("+79131234567"));
        });
    }

    [Test]
    public void UpdateUser_WithEmptyString_ClearsField()
    {
        var service = new UsersService();
        var userId = service.CreateUser(new CreateUserDto
        {
            Login = "klim",
            Password = "123",
            Name = "Клим"
        });

        service.UpdateUser(userId, new UpdateUserDto { Name = "" });

        Assert.That(service.GetUser(userId).Name, Is.Empty);
    }

    [Test]
    public void Operations_WithUnknownUser_ThrowExceptions()
    {
        var service = new UsersService();
        var userId = Guid.NewGuid();

        Assert.Multiple(() =>
        {
            Assert.That(() => service.GetUser(userId), Throws.TypeOf<Exception>());
            Assert.That(() => service.DeleteUser(userId), Throws.TypeOf<Exception>());
            Assert.That(
                () => service.UpdateUser(userId, new UpdateUserDto()),
                Throws.TypeOf<Exception>());
            Assert.That(
                () => service.ChangePassword(userId, "123", "456"),
                Throws.TypeOf<Exception>());
        });
    }

    [Test]
    public void CreateUser_WithDifferentLogins_AssignsDifferentIdsAndPasswords()
    {
        var service = new UsersService();
        var firstId = CreateUser(service);
        var secondId = service.CreateUser(new CreateUserDto { Login = "anna", Password = "456" });

        Assert.Multiple(() =>
        {
            Assert.That(secondId, Is.Not.EqualTo(firstId));
            Assert.That(service.LogIn("klim", "123"), Is.EqualTo(firstId));
            Assert.That(service.LogIn("anna", "456"), Is.EqualTo(secondId));
            Assert.That(() => service.LogIn("anna", "123"), Throws.TypeOf<Exception>());
        });
    }

    [Test]
    public void CreateUser_AfterDeletingSameLogin_AllowsReusingLogin()
    {
        var service = new UsersService();
        var oldId = CreateUser(service);
        service.DeleteUser(oldId);

        var newId = CreateUser(service);

        Assert.That(newId, Is.Not.EqualTo(oldId));
        Assert.That(service.LogIn("klim", "123"), Is.EqualTo(newId));
    }

    private static Guid CreateUser(IUsersService service)
    {
        return service.CreateUser(new CreateUserDto { Login = "klim", Password = "123" });
    }
}
