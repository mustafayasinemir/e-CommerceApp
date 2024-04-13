using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace eCommerce.Client.Models
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
        public string CustomerId { get; set; }
    }
}
