using eCommerce.Client.Models;
using eCommerce.Client.Services;

namespace eCommerce.Client.Views;

public partial class HomePage : ContentPage
{
    public HomePage()
    {
        InitializeComponent();
        LblUserName.Text = "Merhaba" + Preferences.Get("username", string.Empty);
        GetCategories();
        GetTrendingProducts();
        GetBestSellingProducts();
    }

    private async void GetBestSellingProducts()
    {
        var products = await ApiService.GetProducts("En Çok Satanlar", string.Empty);
        CvBestSelling.ItemsSource = products;
    }

    private async void GetTrendingProducts()
    {
        var products = await ApiService.GetProducts("Trendler", string.Empty);
        CvTrending.ItemsSource = products;
    }

    private async void GetCategories()
    {
        var categories = await ApiService.GetCategories();
        CvCategories.ItemsSource = categories;
    }

    private void CvCategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var currentSelection = e.CurrentSelection.FirstOrDefault() as Category;
        if (currentSelection == null) return;
        Navigation.PushAsync(new ProductListPage(currentSelection.Id));
        ((CollectionView)sender).SelectedItem = null;
    }

    private void CvBestSelling_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var currentSelection = e.CurrentSelection.FirstOrDefault() as Product;
        if (currentSelection == null) return;
        Navigation.PushAsync(new ProductDetailPage(currentSelection.Id));
        ((CollectionView)sender).SelectedItem = null;
    }

    private void CvTrending_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var currentSelection = e.CurrentSelection.FirstOrDefault() as Product;
        if (currentSelection == null) return;
        Navigation.PushAsync(new ProductDetailPage(currentSelection.Id));
        ((CollectionView)sender).SelectedItem = null;
    }
}