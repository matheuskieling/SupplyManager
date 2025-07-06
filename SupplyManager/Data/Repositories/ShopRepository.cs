using Microsoft.EntityFrameworkCore;
using SupplyManager.Model;

namespace SupplyManager.Data.Repositories;

public class ShopRepository(AppDbContext dbContext)
{

    public async Task<Transaction> CreateTransactionAsync(Transaction transaction)
    {
        await dbContext.Transactions.AddAsync(transaction);
        await dbContext.SaveChangesAsync();
        return transaction;
    }

    public async Task<Transaction?> GetTransactionByIdAsync(Guid transactionId)
    {
        return await dbContext.Transactions.Where(t => t.Id == transactionId)
            .Include(t=> t.ShoppingCart)
            .ThenInclude(sc => sc.ProductOrders)
            .FirstOrDefaultAsync();
    }
}