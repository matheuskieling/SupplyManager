using System.ComponentModel.DataAnnotations;

namespace SupplyManager.Model;

public class Transaction
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public ShoppingCart ShoppingCart { get; set; } = new ShoppingCart();
}