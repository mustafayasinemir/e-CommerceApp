namespace eCommerce.Api.DTOs
{
    public class ProductAddedDto
    {
        public string Name { get; set; }
        public string Detail { get; set; }
        public string ImageUrl { get; set; }
        public double Price { get; set; }
        public bool IsTrending { get; set; } 
        public bool IsBestSelling { get; set; } 
        public int CategoryId { get; set; }
        public DateTime CreatedDate { get; set; }= DateTime.UtcNow;

    }
}
