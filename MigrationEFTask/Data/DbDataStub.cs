using MigrationEFTask.Model;

namespace MigrationEFTask.Data;

public class DbDataStub
{
    public List<Category> Categories { get; set; }

    public List<Order> Orders { get; set; } = new List<Order>();

    public List<Buyer> Buyers { get; set; }

    public List<Product> Products { get; set; }

    public DbDataStub()
    {
        Categories = new()
        {
            new() { Name = "Electronics" },
            new() { Name = "Home & Kitchen" },
            new() { Name = "Fashion & Accessories" },
            new() { Name = "Automotive" },
        };

        Products = new()
        {
            new() { Name = "Product1", Price = 100, Categories = [Categories[0], Categories[1]] },
            new() { Name = "Product2", Price = 200, Categories = [Categories[0], Categories[1]] },
            new() { Name = "Product3", Price = 300, Categories = [Categories[2], Categories[3]] },
            new() { Name = "Product4", Price = 400, Categories = [Categories[2]] },
            new() { Name = "Product5", Price = 500, Categories = [Categories[3]] }
        };

        Buyers = new()
        {
            new()
            {
                FirstName = "Ivan",
                LastName = "Petrov",
                MiddleName = "Sergeyevich",
                PhoneNumber = "+7 912 345-67-89",
                Email = "ivan.petrov@example.com"
            },
            new()
            {
                FirstName = "Anna",
                LastName = "Sidorova",
                MiddleName = "Vladimirovna",
                PhoneNumber = "+7 911 234-56-78",
                Email = "anna.sidorova@example.com"
            },
            new()
            {
                FirstName = "Dmitry",
                LastName = "Ivanov",
                MiddleName = "Alexandrovich",
                PhoneNumber = "+7 903 456-78-90",
                Email = "dmitry.ivanov@example.com"
            },
            new()
            {
                FirstName = "Elena",
                LastName = "Kuznetsova",
                MiddleName = "Mikhailovna",
                PhoneNumber = "+7 904 567-89-01",
                Email = "elena.kuznetsova@example.com"
            },
            new()
            {
                FirstName = "Sergey",
                LastName = "Smirnov",
                MiddleName = "Petrovich",
                PhoneNumber = "+7 905 678-90-12",
                Email = "sergey.smirnov@example.com"
            }
        };

        FillOrders();
    }

    private void FillOrders()
    {
        var rnd = new Random();

        for (var i = 0; i < 1000; i++)
        {
            Orders.Add(new Order()
            {
                OrderDate = DateTime.Now,
                BuyerId = rnd.Next(1, 5),
                Products = [Products[0], Products[rnd.Next(2, 5)]]
            });
        }
    }
}