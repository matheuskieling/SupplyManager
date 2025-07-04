using Microsoft.EntityFrameworkCore;
using SupplyManager.Model;

namespace SupplyManager.Data;

public class AppDbContext: DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
    {
    }
    
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductOrder> ProductOrders { get; set; }
    public DbSet<ShoppingCart> ShoppingCarts { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<ProductStock> ProductStocks { get; set; }
}