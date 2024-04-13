using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace eCommerce.Client.Models
{
    public class Orders
    {
        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty ("orderTotal")]
        public int OrderTotal { get; set; }

        [JsonProperty("userId")]
        public int UserId { get; set; }
    }
}
