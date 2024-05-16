using eCommerce.UI.Models;
using eCommerce.UI.Services.FavoriteService;

namespace eCommerce.UI.Pages;

public partial class FavoritesPage : ContentPage
{
    private FavoriteService favoriteService;
    public FavoritesPage()
    {
        InitializeComponent();
        favoriteService = new FavoriteService();
    }

    private void CvProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var currentSelection = e.CurrentSelection.FirstOrDefault() as FavoriProduct;
        if (currentSelection == null) return;
        Navigation.PushAsync(new ProductDetailPage(currentSelection.ProductId));
        ((CollectionView)sender).SelectedItem = null;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        GetFavoriteProducts();
    }

    private void GetFavoriteProducts()
    {
        var favoriteProducts = favoriteService.ReadAll();
        CvProducts.ItemsSource = favoriteProducts;
    }
}