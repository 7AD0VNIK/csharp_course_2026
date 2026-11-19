namespace Lab4.Task3.Models;

public class Employee : User
{
    public List<Order> Orders { get; } = new();
}

