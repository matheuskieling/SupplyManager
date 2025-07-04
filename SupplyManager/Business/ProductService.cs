using SupplyManager.Data.Repositories;
using SupplyManager.Exceptions;
using SupplyManager.Model;
using SupplyManager.Model.DTO;
using SupplyManager.Model.Enums;
using SupplyManager.Model.Mappers;

namespace SupplyManager.Business;

public class ProductService(ProductRepository repository)
{

    public async Task<List<Product>> GetProductsAsync()
    {
        return await repository.GetProductsAsync();
    }
    
    public async Task<Product?> GetProductByIdAsync(Guid id)
    {
        return await repository.GetProductByIdAsync(id);
    }
    
    public async Task<Product> SaveNewProductAsync(AddProductRequestDto request)
    {
        var product = ProductMapper.MapToProduct(request);
        return await repository.SaveNewProductAsync(product);
    }

    public async Task<ProductStock> UpdateProductStockAsync(UpdateProductStockRequestDto request)
    {
        var product = await repository.GetProductByIdAsync(request.ProductId);
        if (product is null)
        {
            throw new ProductNotFoundException($"Product with ID {request.ProductId} not found.");
        }
        var productStock = await repository.GetProductStockByProductIdAsync(request.ProductId);
        if (productStock is null)
        {
            if (request.StockAction == StockAction.Remove)
            {
                throw new InvalidOperationException($"Product stock for product with ID {request.ProductId} is empty");
            }
            productStock = new ProductStock {
                Product = product,
                Quantity = 0
            };
            await repository.CreateProductStockAsync(productStock);
        }

        ValidateTransaction(request, productStock);
        productStock.Quantity += request.StockAction == StockAction.Add ? request.Quantity : -request.Quantity;
        await repository.SaveChanges();
        return productStock;
    }

    public void ValidateTransaction(UpdateProductStockRequestDto request, ProductStock productStock)
    {
        if (request.StockAction == StockAction.Remove && productStock.Quantity < request.Quantity)
        {
            throw new InvalidOperationException($"Insufficient stock for product with ID {request.ProductId}. Available: {productStock.Quantity}, Requested: {request.Quantity}");
        }
        
        if (request.StockAction == StockAction.Add && request.Quantity <= 0)
        {
            throw new InvalidOperationException("Quantity to add must be greater than zero.");
        }
        
        if (request.StockAction == StockAction.Remove && request.Quantity <= 0)
        {
            throw new InvalidOperationException("Quantity to remove must be greater than zero.");
        }
    }
}