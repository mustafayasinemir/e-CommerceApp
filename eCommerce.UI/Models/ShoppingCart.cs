using Newtonsoft.Json;

namespace eCommerce.UI.Models
{
    public class ShoppingCart
    {
        [JsonProperty("price")]
        public int Price { get; set; }

        [JsonProperty("qty")]
        public int Qty { get; set; }

        [JsonProperty("totalAmount")]
        public int TotalAmount { get; set; }

        [JsonProperty("productId")]
        public int ProductId { get; set; }

        [JsonProperty("customerId")]
        public int CustomerId { get; set; }
    }
}
