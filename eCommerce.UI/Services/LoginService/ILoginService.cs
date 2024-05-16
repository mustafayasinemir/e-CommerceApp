namespace eCommerce.UI.Services.LoginService
{
    internal interface ILoginService
    {
        Task<bool> Login(string email, string password);
    }
}
