namespace Marketplace.Core;

public class User
{
    private readonly Guid id;
    private readonly DateTime registerDate;
    private string login = "";
    private string passwordHash = "";
    private string name = "";
    private string surname = "";
    private string inn = "";
    private string phone = "";

    public Guid Id
    {
        get => id;
        init => id = value;
    }

    public DateTime RegisterDate
    {
        get => registerDate;
        init => registerDate = value;
    }

    public string Login
    {
        get => login;
        set => login = value;
    }

    public string PasswordHash
    {
        get => passwordHash;
        set => passwordHash = value;
    }

    public string Name
    {
        get => name;
        set => name = value;
    }

    public string Surname
    {
        get => surname;
        set => surname = value;
    }

    public string Inn
    {
        get => inn;
        set => inn = value;
    }

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

    public User(string login, string passwordHash, 
        string name, string surname, string inn, string phone) : this()
    {
        Login = login;
        PasswordHash = passwordHash;
        Name = name;
        Surname = surname;
        Inn = inn;
        Phone = phone;
    }

    public string GetUserFullName()
    {
        return $"{Name} {Surname}";
    }

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