namespace Lab4.Task3.Models;

public class Customer : User
{
    public Cart Cart { get; } = new();
    public List<Order> Orders { get; } = new();
}
