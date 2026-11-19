using Lab4.Task3.Models;

namespace Lab4.Task3.Tests;

public class UserRolesTests
{
    [Test]
    public void Roles_WithNewInstances_InheritUserAndInitializeCollections()
    {
        var employee = new Employee { Inn = "1234567890" };
        var customer = new Customer();
        var admin = new Admin();
        var seller = new Seller { Inn = "9876543210" };

        Assert.Multiple(() =>
        {
            Assert.That(employee, Is.InstanceOf<User>());
            Assert.That(customer, Is.InstanceOf<User>());
            Assert.That(admin, Is.InstanceOf<User>());
            Assert.That(seller, Is.InstanceOf<User>());
            Assert.That(employee.Inn, Is.EqualTo("1234567890"));
            Assert.That(seller.Inn, Is.EqualTo("9876543210"));
            Assert.That(employee.Orders, Is.Empty);
            Assert.That(customer.Cart, Is.Not.Null);
            Assert.That(customer.Orders, Is.Empty);
            Assert.That(admin.Employees, Is.Empty);
            Assert.That(seller.Products, Is.Empty);
            Assert.That(seller.Orders, Is.Empty);
        });
    }

    [Test]
    public void Collections_WithDifferentUsers_StoreIndependentContents()
    {
        var employee = new Employee();
        var customer = new Customer();
        var admin = new Admin();
        var seller = new Seller();
        var secondSeller = new Seller();

        employee.Orders.Add(new Order());
        customer.Orders.Add(new Order());
        admin.Employees.Add(employee);
        seller.Products.Add(new Product());
        seller.Orders.Add(new Order());

        Assert.Multiple(() =>
        {
            Assert.That(employee.Orders, Has.Count.EqualTo(1));
            Assert.That(customer.Orders, Has.Count.EqualTo(1));
            Assert.That(admin.Employees, Does.Contain(employee));
            Assert.That(seller.Products, Has.Count.EqualTo(1));
            Assert.That(seller.Orders, Has.Count.EqualTo(1));
            Assert.That(secondSeller.Products, Is.Empty);
            Assert.That(secondSeller.Orders, Is.Empty);
            Assert.That(new Customer().Cart, Is.Not.SameAs(customer.Cart));
        });
    }
}
