using eCommerce.UI.Models;
using Newtonsoft.Json;
using System.Text;

namespace eCommerce.UI.Services.OrderService
{
    public class OrderService : ApiService
    {
        public async Task<bool> PlaceOrder(Order order)
        {
            var json = JsonConvert.SerializeObject(order);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(AppSettings.ApiUrl + "api/orders", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<List<OrderByUser>> GetOrdersByUser(int userId)
        {
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "api/orders/ordersbyuser/" + userId);
            return JsonConvert.DeserializeObject<List<OrderByUser>>(response);
        }

        public async Task<List<OrderDetail>> GetOrderDetails(int orderId)
        {
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "api/orders/orderdetails/" + orderId);
            return JsonConvert.DeserializeObject<List<OrderDetail>>(response);
        }
    }
}

