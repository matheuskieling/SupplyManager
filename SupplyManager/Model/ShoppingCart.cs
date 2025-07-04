using System.ComponentModel.DataAnnotations;

namespace SupplyManager.Model;

public class ShoppingCart
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public List<ProductOrder> ProductOrders { get; set; } = new();
}