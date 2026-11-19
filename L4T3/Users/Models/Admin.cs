namespace Lab4.Task3.Models;

public class Admin : User
{
    public List<Employee> Employees { get; } = new();
}


