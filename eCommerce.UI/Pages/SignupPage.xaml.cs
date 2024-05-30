using eCommerce.UI.Services.RegisterService;
using System.Text.RegularExpressions;

namespace eCommerce.UI.Pages;

public partial class SignupPage : ContentPage
{
    public SignupPage()
    {
        InitializeComponent();
    }

    private async void BtnSignup_Clicked(object sender, EventArgs e)
    {
        if (IsValid())
        {
            var registerService = new RegisterService();
            var response = await registerService.RegisterUser(EntName.Text, EntEmail.Text, EntPhone.Text, EntPassword.Text);

            if (response)
            {
                await DisplayAlert("", "Hesabýnýz oluþturuldu", "Tamam");
                await Navigation.PushAsync(new LoginPage());
            }
            else
            {
                await DisplayAlert("", "Üzgünüz! Bir þeyler yanlýþ gitti!", "Kapat");
            }
        }
    }

    private async void TapLogin_Tapped(object sender, TappedEventArgs e)
    {
        await Navigation.PushAsync(new LoginPage());
    }

    private bool IsValid()
    {
        if (string.IsNullOrWhiteSpace(EntName.Text) ||
            string.IsNullOrWhiteSpace(EntEmail.Text) ||
            string.IsNullOrWhiteSpace(EntPhone.Text) ||
            string.IsNullOrWhiteSpace(EntPassword.Text))
        {
            DisplayAlert("Geçersiz Giriþ", "Lütfen tüm alanlarý doldurun.", "Kapat");
            return false;
        }

        if (!IsValidEmail(EntEmail.Text))
        {
            DisplayAlert("Geçersiz E-posta", "Lütfen geçerli bir e-posta adresi girin.", "Kapat");
            return false;
        }

        if (EntPassword.Text.Length < 6)
        {
            DisplayAlert("Geçersiz Þifre", "Þifre en az 6 karakter uzunluðunda olmalýdýr.", "Kapat");
            return false;
        }

        return true;
    }

    private bool IsValidEmail(string email)
    {
        var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        return emailRegex.IsMatch(email);
    }
}
