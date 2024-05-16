using Microsoft.Maui.Controls;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace eCommerce.UI.Pages.AdminPage
{
    
    public partial class CategoryAddPage : ContentPage
    {
        private readonly HttpClient _httpClient;

        public CategoryAddPage()
        {
            InitializeComponent();
            _httpClient = new HttpClient();
        }

        private async void OnAddButtonClicked(object sender, EventArgs e)
        {
            var name = NameEntry.Text;
            var imageUrl = ImageUrlEntry.Text;
            var id= new int();

            var newCategory = new
            {
                Id=new int(),
                Name = name,
                ImageUrl = imageUrl
            };

            var json = JsonSerializer.Serialize(newCategory);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("http://localhost:5039/api/admin/Categories", content);

            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("Baþarýlý", "Kategori eklendi", "Tamam");
                NameEntry.Text = string.Empty;
                ImageUrlEntry.Text = string.Empty;
            }
            else
            {
                await DisplayAlert("Hata", "Kategori eklenemedi", "Tamam");
            }
        }
    }
}