using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace eCommerce.Client.Models
{
    public class OrderByDetail
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("qty")]
        public int Qty { get; set; }

        [JsonProperty ("subTotal")]
        public int SubTotal { get; set; }

        [JsonProperty("productName")]
        public string ProductName { get; set; }

        [JsonProperty("productImage")]
        public string ProductImage { get; set; }

        [JsonProperty("productPrice")]
        public int ProductPrice { get; set; }

    }
}
