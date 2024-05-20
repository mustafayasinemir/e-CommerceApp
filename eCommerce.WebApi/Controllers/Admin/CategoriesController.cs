using eCommerce.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Api.Controllers.Admin
{
    [Route("api/admin/[controller]")]
    [Authorize(Roles = "admin")]
    [ApiController]
    public class CategoriesController(ApiDbContext dbContext) : ControllerBase
    {
        [HttpPost("[action]")]
        public IActionResult AddCategory(Category category)
        {
            
                dbContext.Categories.Add(category);
                dbContext.SaveChanges();
                return Ok(category);
            

            return BadRequest(ModelState);
        }

        [HttpDelete("[action]")]
        public IActionResult DeleteCategory(int id)
        {
            var category = dbContext.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }

            dbContext.Categories.Remove(category);
            dbContext.SaveChanges();
            return Ok("Kategori silindi!");
        }

       
    }
}

