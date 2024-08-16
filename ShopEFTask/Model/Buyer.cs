namespace ShopEFTask.Model;

public class Buyer
{
    public int Id { get; set; }

    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public string? MiddleName { get; set; } = null!;

    public string? PhoneNumber { get; set; } = null!;

    public string? Email { get; set; } = null!;

    public virtual IList<Order> Orders { get; set; } = new List<Order>();
}