using System.ComponentModel.DataAnnotations;

namespace ShopEFTask.Model;

public class Category
{
    public int Id { get; set; }

    [MaxLength(100)]
    public required string Name { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}