using eCommerce.UI.Services.OrderService;

namespace eCommerce.UI.Pages;

public partial class OrderDetailPage : ContentPage
{
    OrderService orderService =new OrderService();
    public OrderDetailPage(int orderId, int totalPrice)
    {
        InitializeComponent();
        GetOrderDetail(orderId);
    }

    private async void GetOrderDetail(int orderId)
    {
        var orderDetails = await orderService.GetOrderDetails(orderId);
        CvOrderDetail.ItemsSource = orderDetails;
    }
}