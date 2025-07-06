namespace SupplyManager.Model.DTO.Reponses;

public class TransactionResponseDto
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public ShoppingCartResponseDto ShoppingCart { get; set; } = null!;
}