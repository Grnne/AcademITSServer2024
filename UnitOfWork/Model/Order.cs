namespace UnitOfWork.Model;

public class Order
{
    public int Id { get; set; }

    public DateTime OrderDate { get; set; }

    public int BuyerId { get; set; }

    public virtual Buyer Buyer { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}   