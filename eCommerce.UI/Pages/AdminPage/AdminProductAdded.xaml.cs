using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using eCommerce.UI.Models;

namespace eCommerce.UI.Pages.AdminPage
{
    public partial class AdminProductAdded : ContentPage
    {
        private readonly HttpClient _httpClient;
        private List<Category> _categories;

        public AdminProductAdded()
        {
            InitializeComponent();
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(AppSettings.ApiUrl)
            };
            LoadCategories();
        }

        private async void LoadCategories()
        {
            try
            {
                
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Preferences.Get("accesstoken", string.Empty));

                
                var response = await _httpClient.GetAsync("api/categories");

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    _categories = JsonConvert.DeserializeObject<List<Category>>(responseData);
                    CategoryPicker.ItemsSource = _categories;
                    CategoryPicker.ItemDisplayBinding = new Binding("Name");
                }
                else
                {
                    await DisplayAlert("Hata", $"Kategoriler yüklenirken bir hata oluþtu: {response.ReasonPhrase}", "Tamam");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Hata", $"Kategoriler yüklenirken bir hata oluþtu: {ex.Message}", "Tamam");
            }
        }

        private async void OnAddProductButtonClicked(object sender, EventArgs e)
        {
            if (CategoryPicker.SelectedItem == null)
            {
                await DisplayAlert("Hata", "Lütfen bir kategori seçiniz.", "Tamam");
                return;
            }

            var selectedCategory = (Category)CategoryPicker.SelectedItem;
            var product = new Product
            {
                Name = ProductNameEntry.Text,
                Detail = ProductDetailEntry.Text,
                ImageUrl = ProductImageURLEntry.Text,
                Price = int.TryParse(ProductPriceEntry.Text, out int price) ? price : 0,
                IsBestSelling = BestSellingSwitch.IsToggled,
                IsTrending = TrendSwitch.IsToggled,
                CategoryId = selectedCategory.Id,
                CreatedDate = CreationDatePicker.Date,
                UpdatedDate = DateTime.Now,
                RemoveDate = DateTime.MinValue
            };

            await AddProductAsync(product);
        }

        private async Task AddProductAsync(Product product)
        {
            try
            {
                var json = JsonConvert.SerializeObject(product);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Preferences.Get("accesstoken", string.Empty));

                var response = await _httpClient.PostAsync("api/admin/Products/AddProduct", content);

                if (response.IsSuccessStatusCode)
                {
                    await DisplayAlert("Baþarýlý", "Ürün baþarýyla eklendi.", "Tamam");
                }
                else
                {
                    await DisplayAlert("Hata", "Ürün eklenirken bir hata oluþtu.", "Tamam");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Hata", $"Ürün eklenirken bir hata oluþtu: {ex.Message}", "Tamam");
            }
        }
    }
}
