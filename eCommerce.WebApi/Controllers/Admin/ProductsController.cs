using eCommerce.Api.DTOs;
using eCommerce.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

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
        public IActionResult AddProduct(ProductAddedDto productDto)
        {
            if (ModelState.IsValid)
            {
                var product = new Product
                {
                    Name = productDto.Name,
                    Detail = productDto.Detail,
                    ImageUrl = productDto.ImageUrl,
                    Price = productDto.Price,
                    IsTrending = productDto.IsTrending,
                    IsBestSelling = productDto.IsBestSelling,
                    CategoryId = productDto.CategoryId,
                    CreatedDate = DateTime.UtcNow 
                };

                dbContext.Products.Add(product);
                dbContext.SaveChanges();
                return Ok(product);
            }

            return BadRequest(ModelState);
        }

        [HttpDelete("[action]")]
        public IActionResult DeleteProduct(int id)
        {
            var product = dbContext.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            dbContext.Products.Remove(product);
            dbContext.SaveChanges();
            return Ok("Kategori silindi!");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, ProductUpdateDto productUpdateDto)
        {
            if (id != productUpdateDto.Id)
            {
                return BadRequest();
            }

            var existingProduct = dbContext.Products.Find(id);
            if (existingProduct == null)
            {
                return NotFound();
            }

            existingProduct.Name = productUpdateDto.Name;
            existingProduct.Detail = productUpdateDto.Detail;
            existingProduct.ImageUrl = productUpdateDto.ImageUrl;
            existingProduct.Price = productUpdateDto.Price;
            existingProduct.IsTrending = productUpdateDto.IsTrending;
            existingProduct.IsBestSelling = productUpdateDto.IsBestSelling;
            existingProduct.CategoryId = productUpdateDto.CategoryId;
            existingProduct.UpdatedDate = DateTime.UtcNow;

            dbContext.SaveChanges();
            return Ok("Ürün güncellendi!");
        }
    }
}
