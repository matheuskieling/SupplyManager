namespace SupplyManager.Model.DTO.Reponses;

public class ProductResponseDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public long Price { get; set; }
}