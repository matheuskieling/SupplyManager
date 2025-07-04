using Microsoft.AspNetCore.Mvc;
using SupplyManager.Business;
using SupplyManager.Exceptions;
using SupplyManager.Model.DTO;

namespace SupplyManager.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController(ProductService service) : ControllerBase
{

    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        var products = await service.GetProductsAsync();
        return Ok(products);
    }

    [HttpPost]
    public async Task<IActionResult> AddProduct(AddProductRequestDto request)
    {
        var product = await service.SaveNewProductAsync(request);
        return CreatedAtAction(nameof(AddProduct), new { id = product.Id }, product);
    }
    
    [HttpPost("UpdateProductStock")]
    public async Task<IActionResult>UpdateProductStock(UpdateProductStockRequestDto request)
    {
        try
        {

            var productStock = await service.UpdateProductStockAsync(request);

            return Ok(productStock);
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
        catch (Exception ex)
        {
            return BadRequest(new ProblemDetails
            {
                Title = "There was a problem with your request",
                Detail = ex.Message,
                Status = 400
            });
        }
    }
}