using SupplyManager.Model.DTO;
using SupplyManager.Model.DTO.Reponses;

namespace SupplyManager.Model.Mappers;

public static class ProductOrderMapper
{
    public static ProductOrder MapToProductOrder(ProductOrderRequestDto request, Product product)
    {
        return new ProductOrder
        {
            Product = product,
            Quantity = request.Quantity
        };
    }
    public static ProductOrderResponseDto MapToProductOrderReponseDto(ProductOrder productOrder)
    {
        return new ProductOrderResponseDto
        {
            Id = productOrder.Id,
            Product = ProductMapper.MapToProductResponseDto(productOrder.Product),
            Quantity = productOrder.Quantity
        };
    }
}