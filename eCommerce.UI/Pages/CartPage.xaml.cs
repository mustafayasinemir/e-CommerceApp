using System.Collections.ObjectModel;
using eCommerce.UI.Models;
using eCommerce.UI.Services.ShoppingCartService;
using eCommerce.UI.Services.OrderService;


namespace eCommerce.UI.Pages;

public partial class CartPage : ContentPage
{
    private ObservableCollection<ShoppingCartItem> ShoppingCartItems = new ObservableCollection<ShoppingCartItem>();

    private ShoppingCartService shoppingCartServices = new ShoppingCartService();

    private OrderService orderService = new OrderService();


    public CartPage()
    {
        InitializeComponent();
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        GetShoppingCartItems();
        bool address = Preferences.ContainsKey("address");
        if (address)
        {
            LblAddress.Text = Preferences.Get("address", string.Empty);
        }
        else
        {
            LblAddress.Text = "Adresinizi doldurun";
        }
    }

    private async void GetShoppingCartItems()
    {
        ShoppingCartItems.Clear();
        var shoppingcartitems = await shoppingCartServices.GetShoppingCartItems(Preferences.Get("userid", 0));
        foreach (var shoppingCart in shoppingcartitems)
        {
            ShoppingCartItems.Add(shoppingCart);
        }

        CvCart.ItemsSource = ShoppingCartItems;
        UpdateTotalPrice();
    }


    private void UpdateTotalPrice()
    {
        var totalPrice = ShoppingCartItems.Sum(item => item.Price * item.Qty);
        LblTotalPrice.Text = totalPrice.ToString();
    }




    private async void UpdateCartQuantity(int productId, string action)
    {

        var response = await shoppingCartServices.UpdateCartQuantity(productId, action);
        if (response)
        {
            return;
        }
        else
        {
            await DisplayAlert("", "Üzgünüz,Bir þeyler kötü gitti!", "Kapat");
        }
    }


    private void BtnDecrease_Clicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.BindingContext is ShoppingCartItem cartItem)
        {
            if (cartItem.Qty == 1) return;
            else if (cartItem.Qty > 1)
            {
                cartItem.Qty--;
                UpdateTotalPrice();
                UpdateCartQuantity(cartItem.ProductId, "decrease");
            }
        }
    }

    private void BtnIncrease_Clicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.BindingContext is ShoppingCartItem cartItem)
        {
            cartItem.Qty++;
            UpdateTotalPrice();
            UpdateCartQuantity(cartItem.ProductId, "increase");
        }
    }

    private void BtnDelete_Clicked(object sender, EventArgs e)
    {
        if (sender is ImageButton button && button.BindingContext is ShoppingCartItem cartItem)
        {

            ShoppingCartItems.Remove(cartItem);
            UpdateTotalPrice();
            UpdateCartQuantity(cartItem.ProductId, "delete");
        }
    }

    private void BtnEditAddress_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new AddressPage());
    }

    private async void TapPlaceOrder_Tapped(object sender, TappedEventArgs e)
    {
        var order = new Order()
        {
            Address = LblAddress.Text,
            UserId = Preferences.Get("userid", 0),
            OrderTotal = Convert.ToInt32(LblTotalPrice.Text)
        };

        var response = await orderService.PlaceOrder(order);
        if (response)
        {
            await DisplayAlert("", "Sipariþiniz verildi", "Tamam");
            ShoppingCartItems.Clear();
        }
        else
        {
            await DisplayAlert("", "Üzgünüz,Bir þeyler kötü gitti!", "Kapat");
        }
    }
}