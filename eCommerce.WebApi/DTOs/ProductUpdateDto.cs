namespace eCommerce.Api.DTOs
{
    public class ProductUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Detail { get; set; }
        public string? ImageUrl { get; set; }
        public double? Price { get; set; }
        public bool? IsTrending { get; set; } = false;
        public bool? IsBestSelling { get; set; } = false;
        public int CategoryId { get; set; }
        public DateTime? UpdatedDate { get; set; } = DateTime.Now;
    }
}
