using Lab4.Task3.Dtos;
using Lab4.Task3.Models;

namespace Lab4.Task3.Services;

public class UsersService : IUsersService
{
    private readonly Dictionary<Guid, User> users = new();

    public User GetUser(Guid userId)
    {
        if (!users.TryGetValue(userId, out var user))
            throw new Exception("Пользователь не найден.");

        return user;
    }

    public Guid CreateUser(CreateUserDto createUserDto)
    {
        if (createUserDto is null)
            throw new Exception("Данные пользователя не переданы.");

        if (string.IsNullOrWhiteSpace(createUserDto.Login))
            throw new Exception("Логин обязателен.");

        if (string.IsNullOrWhiteSpace(createUserDto.Password))
            throw new Exception("Пароль обязателен.");

        if (FindUserByLogin(createUserDto.Login) is not null)
            throw new Exception("Этот логин уже занят.");

        var user = new User
        {
            Login = createUserDto.Login,
            Password = createUserDto.Password,
            Name = createUserDto.Name ?? string.Empty,
            Surname = createUserDto.Surname ?? string.Empty,
            Inn = createUserDto.Inn ?? string.Empty,
            Phone = createUserDto.Phone ?? string.Empty
        };

        users.Add(user.Id, user);
        return user.Id;
    }

    public void DeleteUser(Guid userId)
    {
        if (!users.Remove(userId))
            throw new Exception("Пользователь не найден.");
    }

    public void ChangePassword(Guid userId, string oldPassword, string newPassword)
    {
        var user = GetUser(userId);

        if (user.Password != oldPassword)
            throw new Exception("Старый пароль неверен.");

        if (string.IsNullOrWhiteSpace(newPassword))
            throw new Exception("Новый пароль обязателен.");

        user.Password = newPassword;
    }

    public void UpdateUser(Guid userId, UpdateUserDto updateUserDto)
    {
        var user = GetUser(userId);

        if (updateUserDto is null)
            throw new Exception("Данные для обновления не переданы.");

        user.Name = updateUserDto.Name ?? user.Name;
        user.Surname = updateUserDto.Surname ?? user.Surname;
        user.Inn = updateUserDto.Inn ?? user.Inn;
        user.Phone = updateUserDto.Phone ?? user.Phone;
    }

    public Guid LogIn(string login, string password)
    {
        var user = FindUserByLogin(login);

        if (user is null || user.Password != password)
            throw new Exception("Неверный логин или пароль.");

        return user.Id;
    }

    private User? FindUserByLogin(string login)
    {
        foreach (var user in users.Values)
        {
            if (string.Equals(user.Login, login, StringComparison.Ordinal))
                return user;
        }

        return null;
    }
}
