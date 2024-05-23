using eCommerce.UI.Models;

namespace eCommerce.UI.Services.CategoryService
{
    public interface ICategoryService
    {
        Task<List<Category>> GetCategories();
        Task<bool> UpdateCategoryAsync(int id, Category category);

    }
}
