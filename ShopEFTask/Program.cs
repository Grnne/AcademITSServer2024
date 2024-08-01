using Microsoft.EntityFrameworkCore;
using ShopEFTask.Data;
using ShopEFTask.Model;

namespace ShopEFTask;

internal class Program
{
    static void Main(string[] args)
    {
        using var db = new ShopEfDbContext();

        db.Database.EnsureDeleted();
        db.Database.EnsureCreated();

        var dataStub = new DbDataStub();

        db.Category.AddRange(dataStub.Categories);
        db.Product.AddRange(dataStub.Products);
        db.Buyer.AddRange(dataStub.Buyers);
        db.Order.AddRange(dataStub.Orders);

        db.SaveChanges();

        Console.WriteLine("Список покупателей до работы с базой данных:");

        PrintBuyers(db);
        Console.WriteLine();

        var buyer = db.Buyer.FirstOrDefault(x => x.Id == 1);

        if (buyer != null)
        {
            buyer.FirstName = "Dick";
            Console.WriteLine($"Поменяли имя покупателю с Id 1: {buyer.FirstName} {buyer.LastName}");
            Console.WriteLine();
            db.Buyer.Remove(buyer);
        }

        db.SaveChanges();

        Console.WriteLine("Удалили покупателя с Id 1");
        PrintBuyers(db);
        Console.WriteLine();

        Console.WriteLine("Отобразим наиболее часто покупаемый товар");
        Console.WriteLine(GetMostFrequentlyPurchasedProduct(db));
        Console.WriteLine();

        Console.WriteLine("Отобразим количество потраченных покупателями денег");
        var buyersExpenses = GetBuyersExpenses(db);

        foreach (var buyersExpense in buyersExpenses)
        {
            Console.WriteLine($"{buyersExpense.Key.FirstName} {buyersExpense.Key.MiddleName ?? ""} {buyersExpense.Key.LastName} {buyersExpense.Value}");
        }

        Console.WriteLine();

        Console.WriteLine("Отобразим количество купленных товаров по категориям");
        var productsAmountByCategory = GetProductsAmountByCategory(db);

        foreach (var pair in productsAmountByCategory)
        {
            Console.WriteLine($"{pair.Key} {pair.Value}");
        }
    }

    public static string? GetMostFrequentlyPurchasedProduct(ShopEfDbContext db)
    {
        var result = db.Order.Include(o => o.Products)
            .SelectMany(o => o.Products)
            .GroupBy(p => new { p.Id, p.Name }) //Сомневаюсь, что корректно, но иначе надо делать второй запрос и выбирать по полученному id
            .Select(g => new { g.Key.Name, Count = g.Count() })
            .OrderByDescending(x => x.Count)
            .FirstOrDefault();

        return result?.Name;
    }

    public static Dictionary<Buyer, decimal> GetBuyersExpenses(ShopEfDbContext db)
    {
        var result = db.Order.Include(o => o.Buyer)
            .Include(o => o.Products)
            .GroupBy(o => o.Buyer)
            .Select(g => new { BuyerName = g.Key, Expanses = g.SelectMany(o => o.Products).Sum(p => p.Price) })
            .ToDictionary(key => key.BuyerName, value => value.Expanses);

        return result;
    }

    public static Dictionary<string, int> GetProductsAmountByCategory(ShopEfDbContext db)
    {
        var result = db.Category.Include(c => c.Products)
            .ThenInclude(c => c.Orders)
            .Select(c => new
            {
                c.Name,
                Amount = c.Products.SelectMany(p => p.Orders).Count()
            })
            .ToDictionary(key => key.Name, value => value.Amount);

        return result;
    }

    public static void PrintBuyers(ShopEfDbContext db)
    {
        var buyersBeforeDeleting = db.Buyer.ToList();

        foreach (var buyer in buyersBeforeDeleting)
        {
            Console.WriteLine($"{buyer.FirstName} {buyer.LastName}");
        }
    }
}