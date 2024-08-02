using Microsoft.EntityFrameworkCore;
using MigrationEFTask.Data;

namespace MigrationEFTask;

internal class Program
{
    static void Main(string[] args)
    {
        using var db = new ShopEfDbContext();

        db.Database.Migrate();

        db.SaveChanges();
    }
}
