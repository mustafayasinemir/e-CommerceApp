using eCommerce.UI.Models;
using eCommerce.UI.Services.ProductService;
using System;
using System.Collections.ObjectModel;

namespace eCommerce.UI.Pages.AdminPage
{
    public partial class AdminProductUpdated : ContentPage
    {
        private readonly ProductService _productService;
        public ObservableCollection<Product> Products { get; set; }

        public AdminProductUpdated()
        {
            _productService = new ProductService();
            Products = new ObservableCollection<Product>();

            InitializeComponent();
            LoadProducts();
        }

        private async void LoadProducts()
        {
            try
            {
                var products = await _productService.GetProductsAdmin();
                Products.Clear();
                foreach (var product in products)
                {
                    Products.Add(product);
                }
                ProductListView.ItemsSource = Products;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Hata", "Ürünler Yüklenemedi!", "Tamam");
            }
        }

        private async void OnUpdateProductClicked(object sender, EventArgs e)
        {
            if (int.TryParse(ProductIdEntry.Text, out int productId))
            {
                var product = new Product
                {
                    Name = ProductNameEntry.Text,
                    Detail = ProductDescriptionEntry.Text,
                    Price = Convert.ToDouble(ProductPriceEntry.Text),
                    ImageUrl = ProductImageURLEntry.Text,
                    IsTrending = TrendSwitch.IsToggled,
                    IsBestSelling = BestSellingSwitch.IsToggled
                };

                try
                {
                    var result = await _productService.UpdateProductAsync(productId, product);
                    if (result)
                    {
                        await DisplayAlert("Baþarýlý", "Ürün Baþarý ile Güncellendi!", "Tamam");
                        LoadProducts();
                    }
                    else
                    {
                        await DisplayAlert("Hata", "Ürün Güncellenemedi!", "Tamam");
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Hata", "Ürün Güncellenemedi!", "Tamam");
                }
            }
            else
            {
                await DisplayAlert("Hata", "Ürün ID Bulunamadý!", "Tamam");
            }
        }

        private void OnProductSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is Product selectedProduct)
            {
                ProductIdEntry.Text = selectedProduct.Id.ToString();
                ProductNameEntry.Text = selectedProduct.Name;
                ProductDescriptionEntry.Text = selectedProduct.Detail;
                ProductPriceEntry.Text = selectedProduct.Price.ToString();
                ProductImageURLEntry.Text = selectedProduct.ImageUrl;
                TrendSwitch.IsToggled = selectedProduct.IsTrending ?? false;
                BestSellingSwitch.IsToggled = selectedProduct.IsBestSelling ?? true;
            }
        }
    }
}
