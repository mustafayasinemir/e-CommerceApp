using eCommerce.UI.Models;
using Newtonsoft.Json;

namespace eCommerce.UI.Services.ProductService
{
    public class ProductService : ApiService
    {
        public async Task<List<Product>> GetProducts(string productType, string categoryId)
        {
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "api/products?productType=" + productType + "&categoryId=" + categoryId);
            return JsonConvert.DeserializeObject<List<Product>>(response);
        }

        public async Task<ProductDetail> GetProductDetail(int productId)
        {
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "api/products/" + productId);
            return JsonConvert.DeserializeObject<ProductDetail>(response);
        }
    }
}
