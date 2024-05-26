using eCommerce.UI.Models;
using eCommerce.UI.Services.CategoryService;
using eCommerce.UI.Services.ProductService;
using System.Collections.ObjectModel;

namespace eCommerce.UI.Pages.AdminPage
{
    public partial class AdminProductUpdated : ContentPage
    {
        private readonly ProductService _productService;
        private readonly CategoryService _categoryService;

        public ObservableCollection<Product> Products { get; set; }

        public AdminProductUpdated()
        {
            InitializeComponent();
            _productService = new ProductService();
            _categoryService = new CategoryService();
            Products = new ObservableCollection<Product>();
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
                ProductPicker.ItemsSource = Products;
                ProductPicker.ItemDisplayBinding = new Binding("Name");
            }
            catch (Exception)
            {
                await DisplayAlert("Hata", "Ürünler Yüklenemedi!", "Tamam");
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                var categories = await _categoryService.GetCategories();
                CategoryPicker.ItemsSource = categories;
                CategoryPicker.ItemDisplayBinding = new Binding("Name");
            }
            catch (Exception)
            {
                await DisplayAlert("Hata", "Kategoriler Yüklenemedi!", "Tamam");
            }
        }

        private async void OnUpdateProductClicked(object sender, EventArgs e)
        {
            var selectedCategory = CategoryPicker.SelectedItem as Category;

            if (selectedCategory != null)
            {
                if (int.TryParse(ProductIdEntry.Text, out int productId))
                {
                    var product = new Product
                    {
                        Id = productId,
                        Name = ProductNameEntry.Text,
                        Detail = ProductDescriptionEntry.Text,
                        Price = Convert.ToDouble(ProductPriceEntry.Text),
                        ImageUrl = ProductImageURLEntry.Text,
                        IsTrending = TrendSwitch.IsToggled,
                        IsBestSelling = BestSellingSwitch.IsToggled,
                        CategoryId = selectedCategory.Id
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
                    catch (Exception)
                    {
                        await DisplayAlert("Hata", "Ürün Güncellenemedi!", "Tamam");
                    }
                }
                else
                {
                    await DisplayAlert("Hata", "Ürün ID Bulunamadý!", "Tamam");
                }
            }
            else
            {
                await DisplayAlert("Hata", "Lütfen bir kategori seçin!", "Tamam");
            }
        }

        private void OnProductSelected(object sender, EventArgs e)
        {
            if (ProductPicker.SelectedItem is Product selectedProduct)
            {
                ProductIdEntry.Text = selectedProduct.Id.ToString();
                ProductNameEntry.Text = selectedProduct.Name;
                ProductDescriptionEntry.Text = selectedProduct.Detail;
                ProductPriceEntry.Text = selectedProduct.Price.ToString();
                ProductImageURLEntry.Text = selectedProduct.ImageUrl;
                TrendSwitch.IsToggled = selectedProduct.IsTrending ?? false;
                BestSellingSwitch.IsToggled = selectedProduct.IsBestSelling ?? false;

                // Seçili kategoriyi ayarlamak için
                var category = CategoryPicker.ItemsSource.Cast<Category>().FirstOrDefault(c => c.Id == selectedProduct.CategoryId);
                CategoryPicker.SelectedItem = category;
            }
        }
    }
}
