namespace Lab4.Task3.Models;

public class Seller : User
{
    public List<Product> Products { get; } = new();
    public List<Order> Orders { get; } = new();
}

