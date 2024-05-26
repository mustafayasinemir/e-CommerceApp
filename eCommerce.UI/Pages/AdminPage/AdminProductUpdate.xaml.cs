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
                await DisplayAlert("Hata", "�r�nler Y�klenemedi!", "Tamam");
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
                await DisplayAlert("Hata", "Kategoriler Y�klenemedi!", "Tamam");
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
                            await DisplayAlert("Ba�ar�l�", "�r�n Ba�ar� ile G�ncellendi!", "Tamam");
                            LoadProducts();
                        }
                        else
                        {
                            await DisplayAlert("Hata", "�r�n G�ncellenemedi!", "Tamam");
                        }
                    }
                    catch (Exception)
                    {
                        await DisplayAlert("Hata", "�r�n G�ncellenemedi!", "Tamam");
                    }
                }
                else
                {
                    await DisplayAlert("Hata", "�r�n ID Bulunamad�!", "Tamam");
                }
            }
            else
            {
                await DisplayAlert("Hata", "L�tfen bir kategori se�in!", "Tamam");
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

                // Se�ili kategoriyi ayarlamak i�in
                var category = CategoryPicker.ItemsSource.Cast<Category>().FirstOrDefault(c => c.Id == selectedProduct.CategoryId);
                CategoryPicker.SelectedItem = category;
            }
        }
    }
}
