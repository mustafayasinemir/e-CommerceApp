using SQLite;
using eCommerce.UI.Models;

namespace eCommerce.UI.Services.FavoriteService
{
    public class FavoriteService
    {
        private readonly SQLiteConnection _database;
        public FavoriteService()
        {
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "SqlLite");
            _database = new SQLiteConnection(dbPath);
            _database.CreateTable<FavoriProduct>();
        }

        public FavoriProduct Read(int id)
        {
            return _database.Table<FavoriProduct>().Where(p => p.ProductId == id).FirstOrDefault();
        }

        public List<FavoriProduct> ReadAll()
        {
            return _database.Table<FavoriProduct>().ToList();
        }

        public void Create(FavoriProduct favoriProduct)
        {
            _database.Insert(favoriProduct);
        }

        public void Delete(FavoriProduct favoriProduct)
        {
            _database.Delete(favoriProduct);
        }

    }
}
