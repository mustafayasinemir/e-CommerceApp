using eCommerce.UI.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace eCommerce.UI.Pages.AdminPage
{
    public partial class AdminCategoryRemove : ContentPage
    {
        private readonly HttpClient _httpClient;

        public AdminCategoryRemove()
        {
            InitializeComponent();

            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Preferences.Get("accesstoken", string.Empty));

            LoadCategoriesAsync();
        }

        private async void LoadCategoriesAsync()
        {
            try
            {
                var response = await _httpClient.GetStringAsync(AppSettings.ApiUrl + "api/categories");

                if (response != null)
                {
                    var categories = JsonConvert.DeserializeObject<List<Category>>(response);
                    CategoryListView.ItemsSource = categories;
                }
                else
                {
                    await DisplayAlert("Hata", "Kategoriler yüklenirken bir hata oluþtu. Lütfen tekrar deneyin.", "Tamam");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Hata", $"Hata oluþtu: {ex.Message}", "Tamam");
            }
        }

        private async void OnRemoveCategoryButtonClicked(object sender, EventArgs e)
        {
            if (!(CategoryListView.SelectedItem is Category selectedCategory))
            {
                await DisplayAlert("Hata", "Lütfen silmek istediðiniz kategoriyi seçiniz!", "Tamam");
                return;
            }

            try
            {
                var response = await _httpClient.DeleteAsync(AppSettings.ApiUrl + $"api/admin/categories/DeleteCategory?id={selectedCategory.Id}");

                if (response.IsSuccessStatusCode)
                {
                    await DisplayAlert("Baþarýlý", "Kategori baþarýyla silindi.", "Tamam");
                    LoadCategoriesAsync();
                }
                else
                {
                    await DisplayAlert("Hata", "Kategori silinirken bir hata oluþtu. Lütfen tekrar deneyin.", "Tamam");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Hata", $"Hata oluþtu: {ex.Message}", "Tamam");
            }
        }
    }
}
