using Microsoft.EntityFrameworkCore;
using SupplyManager.Model;

namespace SupplyManager.Data.Repositories;

public class ProductRepository(AppDbContext dbContext)
{

    public async Task<List<Product>> GetProductsAsync()
    {
        return await dbContext.Products.ToListAsync();
    }

    public async Task<Product> SaveNewProductAsync(Product product)
    {
        await dbContext.Products.AddAsync(product);
        await dbContext.SaveChangesAsync();
        return product;
    }
}