using eCommerce.UI.Services;

namespace eCommerce.UI.Pages;

public partial class ProfilePage : ContentPage
{
    ApiService apiService =new ApiService();
    private byte[] imageArray;

    public ProfilePage()
    {
        InitializeComponent();
        LblUserName.Text = Preferences.Get("username", string.Empty);
    }

    private async void ImgUserProfileBtn_Clicked(object sender, EventArgs e)
    {
        var file = await MediaPicker.PickPhotoAsync();
        if (file != null)
        {
            using (var stream = await file.OpenReadAsync())
            {
                using (var memoryStream = new MemoryStream())
                {
                    await stream.CopyToAsync(memoryStream);
                    imageArray = memoryStream.ToArray();
                    ImgUserProfileBtn.Source = ImageSource.FromStream(() => new MemoryStream(imageArray));
                }
            }
        }


        var response = await apiService.UploadUserImage(imageArray);
        if (response)
        {
            await DisplayAlert("", "Foto�raf Y�kleme Ba�ar�l�", "Tamam");
        }
        else
        {
            await DisplayAlert("", "Hatal� ��lem", "Kapat");
        }

    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var response = await apiService.GetUserProfileImage();
        if (response.ImageUrl != null)
        {
            ImgUserProfileBtn.Source = response.FullImageUrl;
        }
    }

    private void TapOrders_Tapped(object sender, TappedEventArgs e)
    {
        Navigation.PushAsync(new OrdersPage());
    }

    private void BtnLogout_Clicked(object sender, EventArgs e)
    {
        Preferences.Set("accesstoken", string.Empty);
        Application.Current.MainPage = new NavigationPage(new LoginPage());
    }
}