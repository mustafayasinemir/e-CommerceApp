using eCommerce.Api.Models;
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

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Elektronik Aletler", ImageUrl = "elektronik.jpg" },
                new Category { Id = 2, Name = "Süpermarket", ImageUrl = "spr.png" },
                new Category { Id = 3, Name = "Erkek", ImageUrl = "erkek.png" },
                new Category { Id = 4, Name = "Kadın", ImageUrl = "kadın.png" },
                new Category { Id = 5, Name = "Kozmetik", ImageUrl = "kozmetik.jpg" }
                );
        }

    }

