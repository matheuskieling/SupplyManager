namespace SupplyManager.Model.DTO;

public record ShoppingCartRequestDto(
    List<ProductOrderRequestDto> ProductOrders
);