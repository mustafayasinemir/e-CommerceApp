using eCommerce.UI.Models;
using eCommerce.UI.Services.ProductService;
using eCommerce.UI.Services.FavoriteService;
using eCommerce.UI.Services.ShoppingCartService;



namespace eCommerce.UI.Pages;

public partial class ProductDetailPage : ContentPage
{
    
    private int productId;
    private string imageUrl;
    private FavoriteService favoriteService = new FavoriteService();
    private ProductService productService = new ProductService();
    private ShoppingCartService cartService = new ShoppingCartService();

    public ProductDetailPage(int productId)
    {
        InitializeComponent();
        GetProductDetail(productId);
        this.productId = productId;
    }

    private async void GetProductDetail(int productId)
    {
        var product = await productService.GetProductDetail(productId);
        LblProductName.Text = product.Name;
        LblProductDescription.Text = product.Detail;
        ImgProduct.Source = product.FullImageUrl;
        LblProductPrice.Text = product.Price.ToString();
        LblTotalPrice.Text = product.Price.ToString();
        imageUrl = product.FullImageUrl;
    }

    private void BtnAdd_Clicked(object sender, EventArgs e)
    {
        var i = Convert.ToInt32(LblQty.Text);
        i++;
        LblQty.Text = i.ToString();
        var totalPrice = i * Convert.ToInt32(LblProductPrice.Text);
        LblTotalPrice.Text = totalPrice.ToString();
    }

    private void BtnRemove_Clicked(object sender, EventArgs e)
    {
        var i = Convert.ToInt32(LblQty.Text);
        i--;
        if (i < 1)
        {
            return;
        }
        LblQty.Text = i.ToString();
        var totalPrice = i * Convert.ToInt32(LblProductPrice.Text);
        LblTotalPrice.Text = totalPrice.ToString();
    }

    private async void BtnAddToCart_Clicked(object sender, EventArgs e)
    {
        var shoppingCart = new ShoppingCart()
        {
            Qty = Convert.ToInt32(LblQty.Text),
            Price = Convert.ToInt32(LblProductPrice.Text),
            TotalAmount = Convert.ToInt32(LblTotalPrice.Text),
            ProductId = productId,
            CustomerId = Preferences.Get("userid", 0)
        };

        var response = await cartService.AddItemsInCart(shoppingCart);
        if (response)
        {
            await DisplayAlert("", "Ürün sepetinize eklendi", "Tamam");
        }
        else
        {
            await DisplayAlert("", "Üzgünüz! Bir þeyler kötü gitti ! ", "Kapat");
        }
    }

    private void ImgBtnFavorite_Clicked(object sender, EventArgs e)
    {
        var existingBookmark = favoriteService.Read(productId);
        if (existingBookmark != null)
        {
            favoriteService.Delete(existingBookmark);
        }
        else
        {
            var bookmarkedProduct = new FavoriProduct()
            {
                ProductId = productId,
                IsFavori = true,
                Detail = LblProductDescription.Text,
                Name = LblProductName.Text,
                Price = Convert.ToInt32(LblProductPrice.Text),
                ImageUrl = imageUrl
            };

            favoriteService.Create(bookmarkedProduct);
        }
        UpdateFavoriteButton();
    }
    private void UpdateFavoriteButton()
    {
        var existingBookmark = favoriteService.Read(productId);
        if (existingBookmark != null)
        {
            ImgBtnFavorite.Source = "heartfill";
        }
        else
        {
            ImgBtnFavorite.Source = "heart";
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        UpdateFavoriteButton();
    }
}