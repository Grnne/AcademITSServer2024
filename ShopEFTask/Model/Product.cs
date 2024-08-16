namespace ShopEFTask.Model;

public class Product
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public required decimal Price { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();
}