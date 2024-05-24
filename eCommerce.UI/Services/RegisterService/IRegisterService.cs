namespace eCommerce.UI.Services.RegisterService
{
    public interface IRegisterService
    {
        Task<bool> RegisterUser(string name, string email, string phone, string password);
    }
}
