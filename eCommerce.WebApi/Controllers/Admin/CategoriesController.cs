using eCommerce.Api.DTOs;
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
        public IActionResult AddCategory(CategoryAddedDto categoryDto)
        {
            if (categoryDto == null)
            {
                return BadRequest("Kategori isterlerini doldurunuz ! ");
            }

            if (string.IsNullOrEmpty(categoryDto.Name) || string.IsNullOrEmpty(categoryDto.ImageUrl))
            {
               
                return BadRequest(ModelState);
            }


            var category = new Category
            {
                Name = categoryDto.Name,
                ImageUrl = categoryDto.ImageUrl,
            };

            dbContext.Categories.Add(category);
            dbContext.SaveChanges();

            return Ok(categoryDto);
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

        [HttpPut("[action]")]
        public IActionResult UpdateCategory(int id, CategoryUpdatedDto categoryDto)
        {
            if (categoryDto == null)
            {
                return BadRequest("Kategori bilgilerini doldurunuz!");
            }

            var category = dbContext.Categories.Find(id);
            if (category == null)
            {
                return NotFound("Kategori bulunamadı!");
            }

            category.Name = categoryDto.Name;
            category.ImageUrl = categoryDto.ImageUrl;

            dbContext.Categories.Update(category);
            dbContext.SaveChanges();

            return Ok("Kategori güncellendi!");
        }


    }
}

