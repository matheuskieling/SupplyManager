namespace SupplyManager.Model.DTO;

public record ProductOrderRequestDto(
    Guid ProductId,
    int Quantity 
);