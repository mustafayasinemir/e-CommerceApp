using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Api.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class CategoriesController(ApiDbContext dbContext) : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(dbContext.Categories);
    }
}