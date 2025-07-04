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
}