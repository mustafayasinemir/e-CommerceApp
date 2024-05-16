using eCommerce.UI.Services.LoginService;

namespace eCommerce.UI.Pages;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
    }

    private async void BtnSignIn_Clicked(object sender, EventArgs e)
    {
        var LoginService=new LoginService();
        var response = await LoginService.Login(EntEmail.Text, EntPassword.Text);
        if (response)
        {
            Application.Current.MainPage = new AppShell();  
        }
        else
        {
            await DisplayAlert("", "Üzgünüz,Bir þeyler yanlýþ gitti!", "Kapat");
        }
    }

    private async void TapRegister_Tapped(object sender, TappedEventArgs e)
    {
        await Navigation.PushAsync(new SignupPage());
    }
}