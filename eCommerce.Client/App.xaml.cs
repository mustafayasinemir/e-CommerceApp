using eCommerce.Client.Views;
namespace eCommerce.Client
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new eCommerce.Client.Views.SignupPage();
        }
    }
}
