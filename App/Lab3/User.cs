namespace App.Lab3;

public class User
{
    private readonly Guid id;
    private readonly DateTime registerDate;
    private string login;
    private string password;
    private string name;
    private string surname;
    private string inn;
    private string phone = "";

    public User(string login, string password, string name, string surname, string inn, string phone)
    {
        this.login = login;
        this.password = password;
        this.name = name;
        this.surname = surname;
        this.inn = inn;
        this.phone = phone;
        
        id = Guid.NewGuid();
        registerDate = DateTime.Now;
        
        TryUpdatePhone(phone);
    }

    public string GetUserFullName()
    {
        return $"{name} {surname}";
    }
    public bool TryUpdatePhone(string phone)
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
        {
            return false;
        }

        for (int i = 2; i < phone.Length; i++)
        {
            if (phone[i] < '0' || phone[i] > '9')
            {
                return false;
            }
        }
        
        return true;
    }
}