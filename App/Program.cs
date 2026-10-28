namespace App;

public static class Program
{
    public static void Main()
    {
        var user = Lab3.UserCreator.CreateUser("klim", "123", "Клим", "Садов",
            "123456789012", "+79131234567");

        Console.WriteLine(user.GetUserFullName());
        Console.WriteLine(user.PasswordHash);

        user.Phone = "+79991234567";
        Console.WriteLine(user.Phone);

        user.Phone = "ошибка";
        Console.WriteLine(user.Phone);
        
        var secondUser = new Lab3.User
        {
            Login = "anna",
            Name = "Анна",
            Surname = "Иванова",
            Inn = "123456789012",
            Phone = "+79131234567"
        };

        Console.WriteLine(secondUser.GetUserFullName());
        Console.WriteLine(secondUser.Id);
        Console.WriteLine(secondUser.RegisterDate);
        
        // secondUser.Id = Guid.NewGuid(); // Нельзя: у Id аксессор init.
        
        Console.WriteLine(user.GetUserFullName() == "Клим Садов");
        Console.WriteLine(user.Id != Guid.Empty);

        Console.WriteLine(
            user.PasswordHash == "202CB962AC59075B964B07152D234B70");

        var savedPhone = user.Phone;

        var invalidPhones = new string[]
        {
            "ошибка", "", null, "+7913123456",
            "+791312345678", "+89131234567", "+7913123456a"
        };

        foreach (var invalidPhone in invalidPhones)
        {
            user.Phone = invalidPhone;
            Console.WriteLine(user.Phone == savedPhone);
        }
    }
}