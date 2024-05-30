using eCommerce.UI.Services.LoginService;
using System.Text.RegularExpressions;

namespace eCommerce.UI.Pages;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
    }

    private async void BtnSignIn_Clicked(object sender, EventArgs e)
    {
        if (IsValid())
        {
            var loginService = new LoginService();
            var response = await loginService.Login(EntEmail.Text, EntPassword.Text);
            if (response)
            {
                Application.Current.MainPage = new AppShell();
            }
            else
            {
                await DisplayAlert("", "�zg�n�z, bir �eyler yanl�� gitti!", "Kapat");
            }
        }
    }

    private async void TapRegister_Tapped(object sender, TappedEventArgs e)
    {
        await Navigation.PushAsync(new SignupPage());
    }

    private async void ForgotPassword_Tapped(object sender, EventArgs e)
    {
        // await Navigation.PushAsync(new ForgotPasswordPage());
    }

    private bool IsValid()
    {
        if (string.IsNullOrWhiteSpace(EntEmail.Text) || string.IsNullOrWhiteSpace(EntPassword.Text))
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
