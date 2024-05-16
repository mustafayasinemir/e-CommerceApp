namespace eCommerce.UI.Services.RegisterService
{
    internal interface IRegisterService
    {
        Task<bool> RegisterUser(string name, string email, string phone, string password);
    }
}
