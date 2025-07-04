using Microsoft.AspNetCore.Mvc;
using SupplyManager.Business;
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
}