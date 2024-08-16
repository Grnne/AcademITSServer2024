using System.ComponentModel.DataAnnotations;

namespace UnitOfWork.Model;

public class Buyer
{
    public int Id { get; set; }

    [Required]
    [MaxLength (100)]
    public required string FirstName { get; set; }

    [Required]
    [MaxLength(100)]
    public required string LastName { get; set; }

    [MaxLength(100)]
    public string? MiddleName { get; set; } = null!;

    [MaxLength(100)]
    public string? PhoneNumber { get; set; } = null!;

    [MaxLength(100)]
    public string? Email { get; set; } = null!;

    public virtual IList<Order> Orders { get; set; } = new List<Order>();
}