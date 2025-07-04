namespace SupplyManager.Model.DTO;

public record AddProductRequestDto(
    string Name,
    string? Description,
    long Price
);