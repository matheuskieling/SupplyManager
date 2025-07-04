using SupplyManager.Model.DTO;
using SupplyManager.Model.DTO.Reponses;

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
        };
    }

    public static ProductResponseDto MapToProductResponseDto(Product product)
    {
        return new ProductResponseDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price
        };
    }
}