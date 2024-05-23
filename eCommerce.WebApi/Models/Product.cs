using System.Text.Json.Serialization;

namespace eCommerce.Api.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Detail { get; set; }
        public string? ImageUrl { get; set; }
        public double? Price { get; set; }
        public bool? IsTrending { get; set; } = false;
        public bool? IsBestSelling { get; set; } = false;
        public int? CategoryId { get; set; }
        public DateTime CreatedDate { get; set; }= DateTime.Now;
        public DateTime? RemoveDate { get; set; }= DateTime.Now;
        public DateTime? UpdatedDate { get; set; }=DateTime.Now;


        [JsonIgnore]
        public ICollection<OrderDetail>? OrderDetails { get; set; }
        [JsonIgnore]
        public ICollection<ShoppingCartItem>? ShoppingCartItems { get; set; }

    }
}
