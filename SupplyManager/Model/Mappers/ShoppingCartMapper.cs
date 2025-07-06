using SupplyManager.Model.DTO;
using SupplyManager.Model.DTO.Reponses;

namespace SupplyManager.Model.Mappers;

public static class ShoppingCartMapper
{
    public static ShoppingCartResponseDto MapToShoppingCartResponseDto(ShoppingCart shoppingCart)
    {
        return new ShoppingCartResponseDto
        {
            Id = shoppingCart.Id,
            ProductOrders = shoppingCart.ProductOrders
                .Select(ProductOrderMapper.MapToProductOrderReponseDto)
                .ToList()
        };
    }
}