using Microsoft.EntityFrameworkCore;
using SupplyManager.Model;

namespace SupplyManager.Data;

public class AppDbContext: DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
    {
    }
    
    public DbSet<Product> Products { get; set; }
}