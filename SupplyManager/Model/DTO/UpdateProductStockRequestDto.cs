using System.Text.Json.Serialization;
using SupplyManager.Model.Enums;

namespace SupplyManager.Model.DTO;

public class UpdateProductStockRequestDto
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter<StockAction>))]
    public StockAction StockAction { get; set; }
};