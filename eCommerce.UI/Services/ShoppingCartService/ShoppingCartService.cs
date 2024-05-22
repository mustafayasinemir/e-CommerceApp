using eCommerce.UI.Models;
using Newtonsoft.Json;
using System.Text;

namespace eCommerce.UI.Services.ShoppingCartService
{
    public class ShoppingCartService : ApiService
    {
        public async Task<bool> AddItemsInCart(ShoppingCart shoppingCart)
        {
            var json = JsonConvert.SerializeObject(shoppingCart);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(AppSettings.ApiUrl + "api/shoppingcartitems", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<List<ShoppingCartItem>> GetShoppingCartItems(int userId)
        {
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "api/shoppingcartitems/" + userId);
            return JsonConvert.DeserializeObject<List<ShoppingCartItem>>(response);
        }

        public async Task<bool> UpdateCartQuantity(int productId, string action)
        {
            var content = new StringContent(string.Empty);
            var response = await httpClient.PutAsync(AppSettings.ApiUrl + "api/shoppingcartitems?productId=" + productId + "&action=" + action, content);
            return response.IsSuccessStatusCode;
        }
    }
}



