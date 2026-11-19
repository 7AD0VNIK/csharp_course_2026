namespace Lab4.Task3.Models;

public class User
{
    public Guid Id { get; } = Guid.NewGuid();
    public string Login { get; init; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string Inn { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;

    internal string Password { get; set; } = string.Empty;
}
