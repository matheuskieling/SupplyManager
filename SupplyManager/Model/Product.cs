using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SupplyManager.Model;

[Index(nameof(Name), IsUnique = true)]
public class Product
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Name { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public long Price { get; set; }
}