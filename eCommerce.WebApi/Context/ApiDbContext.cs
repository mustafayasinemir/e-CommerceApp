using eCommerce.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class ApiDbContext : DbContext
{
    public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(p =>
        {
            p.HasKey(x => x.Id);
            p.Property(x => x.Id).ValueGeneratedOnAdd();
            p.Property(p => p.Role).IsRequired();
        });

        modelBuilder.Entity<Product>()
            .Property(p => p.CategoryId)
            .HasDefaultValue(1); // Varsayılan değer ayarı

        modelBuilder.Entity<Product>()
            .HasOne(p => p.Category) // Product sınıfının bir Category'ye sahip olduğunu belirtiyoruz
            .WithMany(c => c.Products) // Category sınıfının birçok ürüne sahip olabileceğini belirtiyoruz
            .HasForeignKey(p => p.CategoryId) // Ürünlerin ilişkili olduğu kategori ID'si
            .OnDelete(DeleteBehavior.SetNull); // Kategori silindiğinde CategoryId'yi null olarak ayarlar
    }
}
