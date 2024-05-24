using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace eCommerce.UI.Pages.AdminPage
{
    public partial class AdminProductRemove : ContentPage
    {
        private readonly HttpClient _httpClient;

        public AdminProductRemove()
        {
            InitializeComponent();
            _httpClient = new HttpClient();
        }

        private async void OnDeleteProductButtonClicked(object sender, EventArgs e)
        {
            var productId = ProductIdEntry.Text;
            if (string.IsNullOrEmpty(productId))
            {
                ResultLabel.Text = "L�tfen ge�erli bir �r�n ID girin.";
                return;
            }

            var isSuccess = await DeleteProductAsync(productId);
            ResultLabel.Text = isSuccess ? "�r�n ba�ar�yla silindi!" : "�r�n silinirken bir hata olu�tu.";
        }

        private async Task<bool> DeleteProductAsync(string productId)
        {
            var url = $"http://192.168.1.106:5039/api/admin/Products/DeleteProduct/{productId}";

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Delete, url);
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await _httpClient.SendAsync(request);
                if (!response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error: {response.StatusCode}, Content: {responseContent}");
                }
                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException httpEx)
            {
                Console.WriteLine($"HTTP Request Exception: {httpEx.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return false;
            }
        }
    }
}
