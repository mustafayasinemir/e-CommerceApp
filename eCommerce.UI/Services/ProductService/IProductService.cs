using eCommerce.UI.Models;

namespace eCommerce.UI.Services.ProductService
{
    public interface IProductService
    {
        Task<List<Product>> GetProducts(string productType, string categoryId);

        Task<ProductDetail> GetProductDetail(int productId);
    }
}
