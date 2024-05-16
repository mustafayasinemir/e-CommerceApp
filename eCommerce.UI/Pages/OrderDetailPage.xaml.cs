using eCommerce.UI.Services;

namespace eCommerce.UI.Pages;

public partial class OrderDetailPage : ContentPage
{
    OrderDetailsService orderDetailsService =new OrderDetailsService();
    public OrderDetailPage(int orderId, int totalPrice)
    {
        InitializeComponent();
        LblTotalPrice.Text = totalPrice + " ?";
        GetOrderDetail(orderId);
    }

    private async void GetOrderDetail(int orderId)
    {
        var orderDetails = await ApiService.GetOrderDetails(orderId);
        CvOrderDetail.ItemsSource = orderDetails;
    }
}