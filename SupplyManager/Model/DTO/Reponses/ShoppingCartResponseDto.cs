namespace SupplyManager.Model.DTO.Reponses;

public class ShoppingCartResponseDto
{
    public Guid Id { get; set; }
    public List<ProductOrderResponseDto> ProductOrders { get; set; } = new ();
}