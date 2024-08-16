using ShopEFTask.Data;

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

        foreach (var pair in buyersExpenses)
        {
            Console.WriteLine($"{pair.Key} {pair.Value}");
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
        var result = db.Order
            .SelectMany(o => o.Products.Select(p => new { Product = p, OrderAmount = o.Amount }))
            .GroupBy(x => new { x.Product.Id, x.Product.Name })
            .Select(g => new
            {
                g.Key.Name,
                Count = g.Count(),
                TotalAmount = g.Sum(x => x.OrderAmount)
            })
            .OrderByDescending(x => x.Count * x.TotalAmount)
            .FirstOrDefault();

        return result?.Name;
    }

    public static Dictionary<string, decimal> GetBuyersExpenses(ShopEfDbContext db)
    {
        var result = db.Buyer
            .SelectMany(b => b.Orders.SelectMany(o => o.Products
                .Select(p => new
                {
                    BuyerId = b.Id,
                    b.FirstName,
                    b.MiddleName,
                    b.LastName,
                    ProductAmount = o.Amount,
                    ProductPrice = p.Price,
                })
            ))
            .GroupBy(s => s.BuyerId)
            .ToDictionary(
                g => // Как это отформатировать?
                    $"{g.FirstOrDefault()!.FirstName} {(string.IsNullOrEmpty(g.FirstOrDefault()!.MiddleName) ? "" : g.FirstOrDefault()!.MiddleName + " ")}{g.FirstOrDefault()!.LastName}",
                g => g.Sum(x => x.ProductPrice * x.ProductAmount)
            );

        return result;
    }

    public static Dictionary<string, int> GetProductsAmountByCategory(ShopEfDbContext db)
    {
        var result = db.Order
            .SelectMany(o => o.Products.SelectMany(p => p.Categories
                .Select(c => new
                {
                    OrderAmount = o.Amount,
                    CategoryName = c.Name
                })))
            .GroupBy(c => c.CategoryName)
            .ToDictionary(g =>
                g.Key, g =>
                g.Sum(c => c.OrderAmount));

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