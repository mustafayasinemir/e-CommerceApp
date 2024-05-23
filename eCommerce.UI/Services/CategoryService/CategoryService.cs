using eCommerce.UI.Models;
using Newtonsoft.Json;

namespace eCommerce.UI.Services.CategoryService
{
    public class CategoryService : ApiService, ICategoryService
    {
        public async Task<List<Category>> GetCategories()
        {
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "api/categories");
            return JsonConvert.DeserializeObject<List<Category>>(response);
        }
    }

}
