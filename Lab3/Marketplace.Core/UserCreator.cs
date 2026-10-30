using System.Security.Cryptography;
using System.Text;

namespace Marketplace.Core;

public static class UserCreator
{
    public static User CreateUser(string login, string password, string name,
        string surname, string inn, string phone)
    {
        var passwordHash = GetPasswordHash(password);
        return new User(login, passwordHash, name, surname, inn, phone);
    }

    private static string GetPasswordHash(string password)
    {
        var bytes = Encoding.UTF8.GetBytes(password);
        var hash = MD5.HashData(bytes);

        return Convert.ToHexString(hash);
    }
}