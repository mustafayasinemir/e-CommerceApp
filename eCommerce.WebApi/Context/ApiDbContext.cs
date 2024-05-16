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
        }

    }

