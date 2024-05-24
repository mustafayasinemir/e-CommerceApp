using eCommerce.UI.Models;

namespace eCommerce.UI.Services.ShoppingCartService
{
    public interface IShoppingCartService
    {
       Task<bool> AddItemsInCart(ShoppingCart shoppingCart);
       Task<List<ShoppingCartItem>> GetShoppingCartItems(int userId);
       Task<bool> UpdateCartQuantity(int productId, string action);



    }
}
