using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace eCommerce.UI.Pages.AdminPage
{
    public partial class AdminCategoryAdded : ContentPage
    {
        private readonly HttpClient _httpClient;

        public AdminCategoryAdded()
        {
            InitializeComponent();

            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(AppSettings.ApiUrl) 
            };

            //_httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private async void OnAddCategoryButtonClicked(object sender, EventArgs e)
        {
            string categoryName = CategoryNameEntry.Text;
            string categoryDescription = CategoryDescriptionEditor.Text;

            
            if (string.IsNullOrEmpty(categoryName) || string.IsNullOrEmpty(categoryDescription))
            {
                await DisplayAlert("Hata", "Tüm alanlarý doldurunuz ! ", "Tamam");
                return;
            }

            var category = new
            {
                Name = categoryName,
                Description = categoryDescription
            };

            var json = JsonConvert.SerializeObject(category);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", string.Empty));

            try
            {
                var response = await _httpClient.PostAsync("api/admin/Categories/AddCategory", content); 

                if (response.IsSuccessStatusCode)
                {
                    await DisplayAlert("Tamamlandý", "Kategori baþarý ile eklendi.", "Tamam");

                }
                else
                {
                    await DisplayAlert("Hata", "Kategori eklenirken bir hata oluþtu. Lütfen tekrar deneyin.", "Tamam");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Hata", "Hata oluþtu", "Tamam");
            }
        }
    }
}
