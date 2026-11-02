namespace App.Lab3;

public class User
{
    private string phone = string.Empty;

    public Guid Id { get; init; }
    public DateTime RegisterDate { get; init; }
    public string Login { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string Inn { get; set; } = string.Empty;

    public string Phone
    {
        get => phone;
        set => TryUpdatePhone(value);
    }

    public User()
    {
        Id = Guid.NewGuid();
        RegisterDate = DateTime.Now;
    }

    public User(string login, string passwordHash, string name,
        string surname, string inn, string phone) : this()
    {
        Login = login;
        PasswordHash = passwordHash;
        Name = name;
        Surname = surname;
        Inn = inn;
        Phone = phone;
    }

    public string GetUserFullName() => $"{Name} {Surname}";

    private bool TryUpdatePhone(string phone)
    {
        if (!IsPhoneValid(phone))
        {
            return false;
        }

        this.phone = phone;
        return true;
    }

    private static bool IsPhoneValid(string phone)
    {
        if (phone == null || phone.Length != 12 || !phone.StartsWith("+7"))
            return false;

        for (var i = 2; i < phone.Length; i++)
        {
            if (phone[i] < '0' || phone[i] > '9')
                return false;
        }

        return true;
    }
}