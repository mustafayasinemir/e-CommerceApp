using eCommerce.UI.Models;
using eCommerce.UI.Services.ProductService;
using eCommerce.UI.Services.CategoryService;
using System.Collections.ObjectModel;

namespace eCommerce.UI.Pages
{
    public partial class HomePage : ContentPage
    {
        ProductService productService = new ProductService();
        public ObservableCollection<Product> Products { get; set; }

        CategoryService categoryService = new CategoryService();
        public HomePage()
        {
            InitializeComponent();
            productService = new ProductService();
            Products = new ObservableCollection<Product>();
            CvBestSelling.ItemsSource = Products;
            LblUserName.Text = "Merhaba! " + Preferences.Get("username", string.Empty);
            GetCategories();
            GetTrendingProducts();
            GetBestSellingProducts();
        }

        private async void OnSearchButtonPressed(object sender, EventArgs e)
        {
            var query = ProductSearchBar.Text;
           
        }



        private async void GetBestSellingProducts()
        {
            var products = await productService.GetProducts("bestselling", string.Empty);
            CvBestSelling.ItemsSource = products;
        }

        private async void GetTrendingProducts()
        {
            var products = await productService.GetProducts("trending", string.Empty);
            CvTrending.ItemsSource = products;
        }

        private async void GetCategories()
        {
            var categories = await categoryService.GetCategories();
            CvCategories.ItemsSource = categories;
        }

        private void Categories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var currentSelection = e.CurrentSelection.FirstOrDefault() as Category;
            if (currentSelection == null) return;
            Navigation.PushAsync(new ProductListPage(currentSelection.Id));
            ((CollectionView)sender).SelectedItem = null;
        }

        private void BestSelling_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var currentSelection = e.CurrentSelection.FirstOrDefault() as Product;
            if (currentSelection == null) return;
            Navigation.PushAsync(new ProductDetailPage(currentSelection.Id));
            ((CollectionView)sender).SelectedItem = null;
        }

        private void Trending_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var currentSelection = e.CurrentSelection.FirstOrDefault() as Product;
            if (currentSelection == null) return;
            Navigation.PushAsync(new ProductDetailPage(currentSelection.Id));
            ((CollectionView)sender).SelectedItem = null;
        }
    }
}
