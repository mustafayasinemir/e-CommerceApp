using eCommerce.UI.Models;
using Newtonsoft.Json;
using System.Text;

namespace eCommerce.UI.Services.CategoryService
{
    public class CategoryService : ApiService, ICategoryService
    {

        public async Task<List<Category>> GetCategories()
        {
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "api/categories");
            return JsonConvert.DeserializeObject<List<Category>>(response);
        }

        public async Task<bool> UpdateCategoryAsync(int id, Category category)
        {
            var json = JsonConvert.SerializeObject(category);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PutAsync($"{AppSettings.ApiUrl}api/admin/categories/updatecategory?id={id}", content);
            return response.IsSuccessStatusCode;
        }
        


    }

}
