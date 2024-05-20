using eCommerce.UI.Models;
using eCommerce.UI.Services;

namespace eCommerce.UI.Pages;

public partial class OrdersPage : ContentPage
{
    private ApiService apiService = new ApiService();
    public OrdersPage()
    {
        InitializeComponent();
        GetOrdersList();
    }

    private async void GetOrdersList()
    {
        var orders = await apiService.GetOrdersByUser(Preferences.Get("userid", 0));
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