using eCommerce.UI.Models;
using eCommerce.UI.Services.OrderService;

namespace eCommerce.UI.Pages;

public partial class OrdersPage : ContentPage
{
    private OrderService orderService = new OrderService();
    public OrdersPage()
    {
        InitializeComponent();
        GetOrdersList();
    }

    private async void GetOrdersList()
    {
        var orders = await orderService.GetOrdersByUser(Preferences.Get("userid", 0));
        CvOrders.ItemsSource = orders;
    }

    private void CvOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selectedItem = e.CurrentSelection.FirstOrDefault() as OrderByUser;
        if (selectedItem == null) return;
        Navigation.PushAsync(new OrderDetailPage(selectedItem.Id, selectedItem.OrderTotal));
        ((CollectionView)sender).SelectedItem = null;
    }
}