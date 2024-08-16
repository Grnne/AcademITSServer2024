using UnitOfWork.Model;

namespace UnitOfWork.Data;

public class ShopEfDbContext : DbContext
{
    private const string ConnectionString = @"Addr=SEREGA\Grnne;Database=ShopEf;Integrated Security=true;TrustServerCertificate=True;";

    public DbSet<Category> Category { get; set; } = null!;

    public DbSet<Product> Product { get; set; } = null!;

    public DbSet<Buyer> Buyer { get; set; } = null!;

    public DbSet<Order> Order { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlServer(ConnectionString).UseLazyLoadingProxies();
    }
}