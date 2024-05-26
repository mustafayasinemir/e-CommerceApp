using eCommerce.UI.Services.ProductService;

namespace eCommerce.UI.Pages.AdminPage
{
    public partial class AdminProductRemove : ContentPage
    {
        private readonly HttpClient _httpClient;
        private readonly ProductService _productService;

        public AdminProductRemove()
        {
            InitializeComponent();
            _httpClient = new HttpClient();
            _productService = new ProductService();
        }

        private async void OnDeleteProductButtonClicked(object sender, EventArgs e)
        {
            var productId = ProductIdEntry.Text;
            if (string.IsNullOrEmpty(productId))
            {
                ResultLabel.Text = "Lütfen geçerli bir ürün ID girin.";
                return;
            }

            bool isConfirmed = await DisplayAlert("Onay", $"ID'si {productId} olan ürünü silmek istediğinizden emin misiniz?", "Evet", "Hayır");
            if (isConfirmed)
            {
                var isSuccess = await _productService.DeleteProductAsync(productId);
                ResultLabel.Text = isSuccess ? "Ürün başarıyla silindi!" : "Ürün silinirken bir hata oluştu.";

                if (isSuccess)
                {
                    
                    await Navigation.PushAsync(new AdminHomePage());
                }
            }
        }
    }
}
