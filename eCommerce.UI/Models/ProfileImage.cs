using Newtonsoft.Json;

namespace eCommerce.UI.Models
{
    public class ProfileImage
    {
        [JsonProperty("imageUrl")]
        public string ImageUrl { get; set; }
        public string FullImageUrl => AppSettings.ApiUrl + ImageUrl;
    }
}
