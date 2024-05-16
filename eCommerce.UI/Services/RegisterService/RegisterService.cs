using eCommerce.UI.Models;
using Newtonsoft.Json;
using System.Text;

namespace eCommerce.UI.Services.RegisterService
{
    internal class RegisterService : IRegisterService
    {
        public async Task<bool> RegisterUser(string name, string email, string phone, string password)
        {
            var register = new Register()
            {
                Name = name,
                Email = email,
                Phone = phone,
                Password = password
            };
            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(register);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(AppSettings.ApiUrl + "api/users/register", content);
            if (!response.IsSuccessStatusCode) return false;
            return true;
        }
    }
}