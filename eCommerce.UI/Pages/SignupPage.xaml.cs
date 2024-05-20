using eCommerce.UI.Services.RegisterService;

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

		if(EntEmail.Text is not null && EntPassword.Text.Length>=6 && EntName.Text is not null && EntPhone.Text is not null)
		{
			await DisplayAlert("Hata", "Kay�t olurken �ifreniz en az 6 karakter olmal�d�r,T�m alanlar� l�tfen doldurunuz !","Tamam");
		}
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