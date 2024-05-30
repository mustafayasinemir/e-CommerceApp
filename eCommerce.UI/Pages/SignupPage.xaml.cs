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
                await DisplayAlert("", "Hesab�n�z olu�turuldu", "Tamam");
                await Navigation.PushAsync(new LoginPage());
            }
            else
            {
                await DisplayAlert("", "�zg�n�z! Bir �eyler yanl�� gitti!", "Kapat");
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
            DisplayAlert("Ge�ersiz Giri�", "L�tfen t�m alanlar� doldurun.", "Kapat");
            return false;
        }

        if (!IsValidEmail(EntEmail.Text))
        {
            DisplayAlert("Ge�ersiz E-posta", "L�tfen ge�erli bir e-posta adresi girin.", "Kapat");
            return false;
        }

        if (EntPassword.Text.Length < 6)
        {
            DisplayAlert("Ge�ersiz �ifre", "�ifre en az 6 karakter uzunlu�unda olmal�d�r.", "Kapat");
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
