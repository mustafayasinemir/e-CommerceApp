using SQLite;
using eCommerce.UI.Models;

namespace eCommerce.UI.Services
{
    public class BookmarkItemService
    {
        private readonly SQLiteConnection _database;
        public BookmarkItemService()
        {
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "SqlLite");
            _database = new SQLiteConnection(dbPath);
            _database.CreateTable<BookmarkedProduct>();
        }

        public BookmarkedProduct Read(int id)
        {
            return _database.Table<BookmarkedProduct>().Where(p => p.ProductId == id).FirstOrDefault();
        }

        public List<BookmarkedProduct> ReadAll()
        {
            return _database.Table<BookmarkedProduct>().ToList();
        }

        public void Create(BookmarkedProduct bookmarkedProduct)
        {
            _database.Insert(bookmarkedProduct);
        }

        public void Delete(BookmarkedProduct bookmarkedProduct)
        {
            _database.Delete(bookmarkedProduct);
        }

    }
}
