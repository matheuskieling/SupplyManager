using Microsoft.AspNetCore.Mvc;

namespace SupplyManager.Controllers;

[ApiController]
[Route("[controller]")]
public class ShopController : ControllerBase
{
    [HttpPost]
    public Task<IActionResult> CreateTransaction()
    {
        
    }
}