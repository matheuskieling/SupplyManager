using Microsoft.AspNetCore.Mvc;
using SupplyManager.Business;
using SupplyManager.Exceptions;
using SupplyManager.Model;
using SupplyManager.Model.DTO;
using SupplyManager.Model.Mappers;

namespace SupplyManager.Controllers;

[ApiController]
[Route("[controller]")]
public class ShopController(ShopService service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateTransaction(ShoppingCartRequestDto shoppingCart)
    {
        try
        {
            var transaction = await service.CreateTransactionAsync(shoppingCart);
            return Ok(TransactionMapper.MapToTransacionResponseDto(transaction));
        }
        catch (ProductNotFoundException ex)
        {
            return NotFound(new ProblemDetails
            {
                Title = "Product Not Found",
                Detail = ex.Message,
                Status = 404
            });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new ProblemDetails
            {
                Title = "Invalid Operation",
                Detail = ex.Message,
                Status = 400
            });
        }
    }
}