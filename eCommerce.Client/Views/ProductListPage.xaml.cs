using eCommerce.Client.Models;
using eCommerce.Client.Services;
using Microsoft.Build.Tasks.Deployment.Bootstrapper;

namespace eCommerce.Client.Views;

public partial class ProductListPage : ContentPage
{
    public ProductListPage(int categoryId)
    {
        InitializeComponent();
        GetProducts(categoryId);
    }

    private async void GetProducts(int categoryId)
    {
        var products = await ApiService.GetProducts("category", categoryId.ToString());
        CvProducts.ItemsSource = products;
    }

    private void CvProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var currentSelection = e.CurrentSelection.FirstOrDefault() as ProductDetail;
        if (currentSelection == null) return;
        Navigation.PushAsync(new ProductDetailPage(currentSelection.Id));
        ((CollectionView)sender).SelectedItem = null;
    }
}