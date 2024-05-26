using eCommerce.UI.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace eCommerce.UI.Services.ProductService
{
    public class ProductService : ApiService, IProductService
    {
        public async Task<List<Product>> GetProducts(string productType, string? categoryId)
        {
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "api/products?productType=" + productType + "&categoryId=" + categoryId);
            return JsonConvert.DeserializeObject<List<Product>>(response);
        }

        public async Task<ProductDetail> GetProductDetail(int productId)
        {
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "api/products/" + productId);
            return JsonConvert.DeserializeObject<ProductDetail>(response);
        }


        public async Task<bool> UpdateProductAsync(int id, Product product)
        {
            var json = JsonConvert.SerializeObject(product);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PutAsync(AppSettings.ApiUrl+"api/admin/products/"+id, content);
            return response.IsSuccessStatusCode;
        }

        public async Task<List<Product>> GetProductsAdmin()
        {
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "api/admin/products");
            return JsonConvert.DeserializeObject<List<Product>>(response);
        }

        public async Task<bool> DeleteProductAsync(string productId)
        {
            var url = AppSettings.ApiUrl + $"api/admin/Products/DeleteProduct/?id={productId}";

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Delete, url);
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await httpClient.SendAsync(request);
                if (!response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                }
                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException httpEx)
            {
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }




    }
}
