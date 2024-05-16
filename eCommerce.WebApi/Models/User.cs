using System.ComponentModel.DataAnnotations.Schema;

namespace eCommerce.Api.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Password { get; set; }
        public string? ImageUrl { get; set; }
        public string Role { get; set; } = "customer";

        [NotMapped]
        public IFormFile? Image { get; set; }
        public ICollection<Order>? Orders { get; set; }  
    }
}
