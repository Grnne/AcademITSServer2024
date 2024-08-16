using Microsoft.EntityFrameworkCore;
using ShopEFTask.Model;

namespace ShopEFTask.Data;

public class ShopEfDbContext : DbContext
{
    private const string ConnectionString = @"Addr=SEREGA\Grnne;Database=ShopEf;Integrated Security=true;TrustServerCertificate=True;";

    public virtual DbSet<Category> Category { get; set; } = null!;

    public virtual DbSet<Product> Product { get; set; } = null!;

    public virtual DbSet<Buyer> Buyer { get; set; } = null!;

    public virtual DbSet<Order> Order { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlServer(ConnectionString).UseLazyLoadingProxies();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Buyer>(builder =>
        {
            builder.Property(b => b.FirstName).HasMaxLength(100);
            builder.Property(b => b.LastName).HasMaxLength(100);
            builder.Property(b => b.MiddleName).HasMaxLength(100);
            builder.Property(b => b.Email).HasMaxLength(100);
            builder.Property(b => b.PhoneNumber).HasMaxLength(100);
        });

        modelBuilder.Entity<Category>(builder =>
        {
            builder.Property(c => c.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Product>(builder =>
        {
            builder.Property(p => p.Name).HasMaxLength(100);
            builder.Property(p => p.Price).HasPrecision(18, 2);
        });
    }
}