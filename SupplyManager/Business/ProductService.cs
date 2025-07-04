using SupplyManager.Data.Repositories;
using SupplyManager.Model;
using SupplyManager.Model.DTO;
using SupplyManager.Model.Mappers;

namespace SupplyManager.Business;

public class ProductService(ProductRepository repository)
{

    public async Task<List<Product>> GetProductsAsync()
    {
        return await repository.GetProductsAsync();
    }
    
    public async Task<Product> SaveNewProductAsync(AddProductRequestDto request)
    {
        var product = ProductMapper.MapToProduct(request);
        return await repository.SaveNewProductAsync(product);
    }
}