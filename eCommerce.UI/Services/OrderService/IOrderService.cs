using eCommerce.UI.Models;

namespace eCommerce.UI.Services.OrderService
{
    public interface IOrderService
    {
        Task<bool> PlaceOrder(Order order);
        Task<List<OrderByUser>> GetOrdersByUser(int userId);
        Task<List<OrderDetail>> GetOrderDetails(int orderId);


    }
}
