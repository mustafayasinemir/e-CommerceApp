using eCommerce.UI.Models;
using eCommerce.UI.Services.CategoryService;
using System.Collections.ObjectModel;

namespace eCommerce.UI.Pages.AdminPage;

public partial class AdminCategoryUpdated : ContentPage
{
    private readonly CategoryService _categoryService;
    public ObservableCollection<Category> Categories { get; set; }

    public AdminCategoryUpdated()
    {
        _categoryService = new CategoryService();
        Categories = new ObservableCollection<Category>();

        InitializeComponent();
        LoadCategories();
    }

    private async void LoadCategories()
    {
        try
        {
            var categories = await _categoryService.GetCategories(); 
            Categories.Clear();
            foreach (var category in categories)
            {
                Categories.Add(category);
            }
            CategoryListView.ItemsSource = Categories;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Hata", "Kategoriler Y�klenemedi!", "Tamam");
        }
    }

    private async void OnUpdateCategoryClicked(object sender, EventArgs e)
    {
        if (int.TryParse(CategoryIdEntry.Text, out int categoryId))
        {
            var category = new Category
            {
                Name = CategoryNameEntry.Text,
                ImageUrl = CategoryImageUrlEntry.Text
            };

            try
            {
                var result = await _categoryService.UpdateCategoryAsync(categoryId, category);
                if (result)
                {
                    await DisplayAlert("Ba�ar�l�", "Kategori Ba�ar� ile G�ncellendi ! ","Tamam");
                    LoadCategories();
                }
                else
                {
                    await DisplayAlert("Hata", "Kategori G�ncelllenemedi ! ", "Tamam");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Hata", "Kategori  G�ncelllenemedi !", "Tamam");
            }
        }
        else
        {
            await DisplayAlert("Hata", "Kategori ID Bulunamad� ! ", "Tamam");
        }
    }

    private void OnCategorySelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem is Category selectedCategory)
        {
            CategoryIdEntry.Text = selectedCategory.Id.ToString();
            CategoryNameEntry.Text = selectedCategory.Name;
            CategoryImageUrlEntry.Text = selectedCategory.ImageUrl;
        }
    }
}
