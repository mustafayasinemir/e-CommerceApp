using SQLite;

namespace eCommerce.UI.Models
{
    public class FavoriProduct
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }
        public int Price { get; set; }
        public string ImageUrl { get; set; }
        public bool IsFavori { get; set; }
    }
}
