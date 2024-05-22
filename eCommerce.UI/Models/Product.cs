using Newtonsoft.Json;

namespace eCommerce.UI.Models
{
    public class Product
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("detail")]
        public string Detail { get; set; }

        [JsonProperty("imageUrl")]
        public string ImageUrl { get; set; }

        [JsonProperty("price")]
        public double Price { get; set; }

        [JsonProperty("isTrend")]  
        public bool? IsTrending { get; set; }

        [JsonProperty("isBest")]
        public bool? IsBestSelling { get; set; }

        [JsonProperty("categoryId")]
        public int? CategoryId { get; set; }

        [JsonProperty("createddate")]
        public DateTime CreatedDate { get; set; }

        [JsonProperty("removeddate")]
        public DateTime? RemoveDate { get; set; }

        [JsonProperty("updateddate")]
        public DateTime? UpdatedDate { get; set; }

      public string FullImageUrl => AppSettings.ApiUrl + ImageUrl;

    }
}
