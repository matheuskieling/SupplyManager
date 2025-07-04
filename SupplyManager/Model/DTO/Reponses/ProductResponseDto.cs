namespace SupplyManager.Model.DTO.Reponses;

public class ProductOrderResponseDto
{
    public Guid Id { get; set; }
    public ProductResponseDto Product { get; set; } = null!;
    public int Quantity { get; set; }
}