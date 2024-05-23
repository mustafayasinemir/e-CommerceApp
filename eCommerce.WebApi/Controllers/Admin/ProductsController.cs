using eCommerce.Api.DTOs;
using eCommerce.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Api.Controllers.Admin
{
    [Route("api/admin/[controller]")]
    [Authorize(Roles = "admin")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApiDbContext dbContext;

        public ProductsController(ApiDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            var products = dbContext.Products.Select(p => new
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                ImageUrl = p.ImageUrl,
                IsTrending = p.IsTrending,
                IsBestSelling = p.IsBestSelling
            }).ToList();

            return Ok(products);
        }

        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            var product = dbContext.Products
                .Where(p => p.Id == id)
                .Select(p => new
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Detail = p.Detail,
                    ImageUrl = p.ImageUrl,
                    IsTrending = p.IsTrending,
                    IsBestSelling = p.IsBestSelling
                })
                .FirstOrDefault();

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost("AddProduct")]
        public IActionResult AddProduct(ProductAddedDto productDTO)
        {
            if (ModelState.IsValid)
            {
                var product = new Product
                {
                    Name = productDTO.Name,
                    Detail = productDTO.Detail,
                    ImageUrl = productDTO.ImageUrl,
                    Price = productDTO.Price,
                    IsTrending = productDTO.IsTrending,
                    IsBestSelling = productDTO.IsBestSelling,
                    CategoryId = productDTO.CategoryId,
                    CreatedDate = DateTime.UtcNow // Set the current UTC time for CreatedDate
                };

                dbContext.Products.Add(product);
                dbContext.SaveChanges();
                return Ok(product);
            }

            return BadRequest(ModelState);
        }

        [HttpDelete("DeleteProduct")]
        public IActionResult DeleteProduct(ProductRemoveDto id)
        {
            var product = dbContext.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            dbContext.Products.Remove(product);
            dbContext.SaveChanges();
            return Ok("Ürün başarıyla silindi!");
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
            return Ok("Ürün güncellendi!");
        }
    }
}
