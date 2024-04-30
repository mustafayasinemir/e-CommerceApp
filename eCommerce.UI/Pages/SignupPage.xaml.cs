using eCommerce.UI.Services;

namespace eCommerce.UI.Pages;

public partial class SignupPage : ContentPage
{
	public SignupPage()
	{
		InitializeComponent();
	}

    private async void BtnSignup_Clicked(object sender, EventArgs e)
	{
        RegisterService RegisterService =new RegisterService();

		var response = await RegisterService.RegisterUser(EntName.Text, EntEmail.Text, EntPhone.Text, EntPassword.Text);
		if (response)
		{
			await DisplayAlert("", "Hesab�n�z olu�turuldu", "Tamam");
			await Navigation.PushAsync(new LoginPage());
		}
		else
		{
            await DisplayAlert("", "�zg�n�z!Bir �eyler yanl�� gitti! ", "Kapat");
        }
		
    }

    private async void TapLogin_Tapped(object sender, TappedEventArgs e)
    {
        await Navigation.PushAsync(new LoginPage());
    }
}