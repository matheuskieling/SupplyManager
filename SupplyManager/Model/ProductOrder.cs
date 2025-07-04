using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SupplyManager.Model;

public class ProductOrder
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    public Product Product { get; set; } = null!;
    public int Quantity { get; set; }

    [JsonIgnore] 
    public ShoppingCart ShoppingCart { get; set; } = null!;
}