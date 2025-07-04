using SupplyManager.Model.DTO;
using SupplyManager.Model.DTO.Reponses;

namespace SupplyManager.Model.Mappers;

public static class TransactionMapper
{
    public static TransactionResponseDto MapToTransacionResponseDto(Transaction transaction)
    {
        return new TransactionResponseDto
        {
            Id = transaction.Id,
            CreatedAt = transaction.CreatedAt,
            ShoppingCart = ShoppingCartMapper.MapToShoppingCartResponseDto(transaction.ShoppingCart)
        };
    }
}