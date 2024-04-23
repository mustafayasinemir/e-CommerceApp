using eCommerce.Client.Services;

namespace eCommerce.Client.Views;

public partial class OrderDetailPage : ContentPage
{
    public OrderDetailPage(int orderId, int totalPrice)
    {
        InitializeComponent();
        LblTotalPrice.Text = totalPrice + " $";
        GetOrderDetail(orderId);
    }

    private async void GetOrderDetail(int orderId)
    {
        var orderDetails = await ApiService.GetOrdersDetails(orderId);
        CvOrderDetail.ItemsSource = orderDetails;
    }
}