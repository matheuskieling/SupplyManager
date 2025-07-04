using Microsoft.EntityFrameworkCore;
using SupplyManager.Model;

namespace SupplyManager.Data.Repositories;

public class ProductRepository(AppDbContext dbContext)
{

    public async Task<List<Product>> GetProductsAsync()
    {
        return await dbContext.Products.ToListAsync();
    }
    
    public async Task<Product?> GetProductByIdAsync(Guid id)
    {
        return await dbContext.Products.FindAsync(id);
    }

    public async Task<Product> SaveNewProductAsync(Product product)
    {
        await dbContext.Products.AddAsync(product);
        await dbContext.SaveChangesAsync();
        return product;
    }

    public async Task SaveChanges()
    {
        await dbContext.SaveChangesAsync();
    }
    
    public async Task<ProductStock?> GetProductStockByProductIdAsync(Guid productId)
    {
        return await dbContext.ProductStocks.Where(ps => ps.Product.Id == productId).FirstOrDefaultAsync();
    }
    
    public async Task<ProductStock> CreateProductStockAsync(ProductStock productStock)
    {
        await dbContext.ProductStocks.AddAsync(productStock);
        await dbContext.SaveChangesAsync();
        return productStock;
    }
    
        
}
