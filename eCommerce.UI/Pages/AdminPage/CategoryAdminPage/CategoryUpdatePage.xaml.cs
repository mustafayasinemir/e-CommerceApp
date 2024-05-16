using Microsoft.Maui.Controls;

namespace eCommerce.UI.Pages.AdminPage
{
    public partial class CategoryUpdatePage : ContentPage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }

        public CategoryUpdatePage(int id, string name, string imageUrl)
        {
            InitializeComponent();

            IdEntry.Text = id.ToString();
            NameEntry.Text = name;
            ImageUrlEntry.Text = imageUrl;
        }

        private void OnUpdateButtonClicked(object sender, EventArgs e)
        {
            // Güncelleme iþlemlerini burada yapabilirsiniz
            var updatedId = int.Parse(IdEntry.Text);
            var updatedName = NameEntry.Text;
            var updatedImageUrl = ImageUrlEntry.Text;

            // Güncelleme iþlemleri için API çaðrýsý veya veri tabaný iþlemleri yapýlabilir
            DisplayAlert("Baþarýlý", "Kategori güncellendi", "Tamam");
        }
    }
}