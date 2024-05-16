using eCommerce.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Api.Controllers.Admin
{
    [Route("api/admin/[controller]")]
    [Authorize(Roles = "admin")]
    [ApiController]
    public class ProductsController(ApiDbContext dbContext) : ControllerBase
    {
        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                product.CreatedDate = DateTime.UtcNow;
                dbContext.Products.Add(product);
                dbContext.SaveChanges();
                return Ok(product);
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var product = dbContext.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            dbContext.Products.Remove(product);
            dbContext.SaveChanges();
            return Ok("Başarı ile silindi ! ");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            var existingProduct = dbContext.Products.Find(id);
            if (existingProduct == null)
            {
                return NotFound();
            }

            existingProduct.Name = product.Name;
            existingProduct.Detail = product.Detail;
            existingProduct.ImageUrl = product.ImageUrl;
            existingProduct.Price = product.Price;
            existingProduct.IsTrending = product.IsTrending;
            existingProduct.IsBestSelling = product.IsBestSelling;
            existingProduct.CategoryId = product.CategoryId;
            existingProduct.UpdatedDate = DateTime.UtcNow;

            dbContext.SaveChanges();
            return Ok("Ürün Güncelendi !");
        }
    }
}
