using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace eCommerce.Client.Models
{
    public class ProfileImage
    {
        [JsonProperty("image_url")]
        public string ImageUrl { get; set; }

    }
}
