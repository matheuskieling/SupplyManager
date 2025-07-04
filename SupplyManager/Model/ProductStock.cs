using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SupplyManager.Model;

public class ProductStock
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public long Quantity { get; set; }
    public Guid ProductId { get; set; }
    
    [ForeignKey(nameof(ProductId))]
    public Product Product { get; set; }
}