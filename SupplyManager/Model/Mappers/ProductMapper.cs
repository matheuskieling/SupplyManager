using SupplyManager.Model.DTO;

namespace SupplyManager.Model.Mappers;

public static class ProductMapper
{
    public static Product MapToProduct(AddProductRequestDto request)
    {
        return new Product
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            Quantity = request.Quantity
        };
    }
}