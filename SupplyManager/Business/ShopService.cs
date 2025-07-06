using Microsoft.EntityFrameworkCore;
using SupplyManager.Data;
using SupplyManager.Data.Repositories;
using SupplyManager.Exceptions;
using SupplyManager.Model;
using SupplyManager.Model.DTO;
using SupplyManager.Model.Enums;
using SupplyManager.Model.Mappers;

namespace SupplyManager.Business;

public class ShopService(ProductService productService, ShopRepository repository, AppDbContext appDbContext)
{

    public async Task<Transaction> CreateTransactionAsync(ShoppingCartRequestDto request)
    {
        if (request.ProductOrders.Count == 0)
        {
            throw new InvalidOperationException("Shopping cart cannot be empty.");
        }
        

        var productOrders = request.ProductOrders.Select(po =>
        {
            var product = productService.GetProductByIdAsync(po.ProductId).Result;
            if (product is null)
            {
                throw new ProductNotFoundException($"Product with ID {po.ProductId} not found.");
            }
            return ProductOrderMapper.MapToProductOrder(po, product);
        }).ToList();
        
        var transactionObj = new Transaction { };
        
        productOrders.ForEach(po =>  po.ShoppingCart = transactionObj.ShoppingCart );
        transactionObj.ShoppingCart.ProductOrders = productOrders;

        await using var transaction = await appDbContext.Database.BeginTransactionAsync();
        try
        {
            foreach (var productOrder in productOrders)
            {
                await productService.UpdateProductStockAsync(new UpdateProductStockRequestDto
                {
                    ProductId = productOrder.Product.Id,
                    Quantity = productOrder.Quantity,
                    StockAction = StockAction.Remove
                });
            }

            var transactionResult = await repository.CreateTransactionAsync(transactionObj);
            await transaction.CommitAsync();
            return transactionResult;
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<Transaction?> GetTransactionByIdAsync(Guid id)
    {
        return await repository.GetTransactionByIdAsync(id);
    }
}