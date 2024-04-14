using eCommerce.Client.Services;

namespace eCommerce.Client.Views;

public partial class SignupPage : ContentPage
{
	public SignupPage()
	{
		InitializeComponent();
	}

    private async void BtnSignup_Clicked(object sender, EventArgs e)
    {
		var response =await ApiService.RegisterUser(EntName.Text, EntEmail.Text, EntPhone.Text, EntPassword.Text);
		if(response)
		{
			await DisplayAlert("", "Hesap oluþturma iþlemi baþarýlý !", "Tamam");
		}
		else
		{
			await DisplayAlert("", "Üzgünüz! Hesap oluþturulamadý!", "Tamam");
		}
    }

}