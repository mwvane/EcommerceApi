using Microsoft.EntityFrameworkCore;
using System.Data;
using static System.Net.Mime.MediaTypeNames;

namespace EcommerceApp.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions options) : base(options) { }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImages> ProductImages { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }
        public DbSet<WishlistItem> WishlistItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<ProductRating> ProductRatings { get; set; }
        public DbSet<ProductViewCount> ProductViewCounts { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Country> Countries { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>()
            .HasMany(c => c.SubCategories)
            .WithOne(c => c.ParentCategory)
            .HasForeignKey(c => c.ParentCategoryId);
            modelBuilder.Entity<CartItem>()
                .HasKey(ci => new { ci.CartId, ci.ProductId });

            modelBuilder.Entity<WishlistItem>()
                .HasKey(wi => new { wi.WishlistId, wi.ProductId });

            modelBuilder.Entity<OrderItem>()
                .HasKey(oi => new { oi.OrderId, oi.ProductId });

            modelBuilder.Entity<Manufacturer>()
            .HasOne(m => m.Country)
            .WithMany(c => c.Manufacturers)
            .HasForeignKey(m => m.CountryId);

            // INITIAL DATA
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "Electronics" },
                new Category { CategoryId = 2, Name = "Smartphone", ParentCategoryId = 1 });

            modelBuilder.Entity<Country>().HasData(
                new Country { CountryId = 1, Name = "South Korea", ImageUrl = "/Resources/Images/Countries/South-Korea.jpg" },
                new Country { CountryId = 2, Name = "Jamaica", ImageUrl = "/Resources/Images/Countries/Jamaica.jpg" },
                new Country { CountryId = 3, Name = "Georgia", ImageUrl = "/Resources/Images/Countries/Georgia.jpg" });

            modelBuilder.Entity<Manufacturer>().HasData(
                new Manufacturer { ManufacturerId = 1, Name = "Samsung", CountryId = 1 },
                new Manufacturer { ManufacturerId = 2, Name = "Kingstone", CountryId = 2 });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 1,
                Name = "Samsung Galaxy s24 ultra",
                Price = 3500,
                CategoryId = 2,
                ManufacturerId = 1,
                Description = "100x zoom",

            });
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = 1,
                    Email = "test@gmail.com",
                    UserName = "test@gmail.com",
                    Role = UserRole.Client,
                    Password = "password",

                });
            modelBuilder.Entity<ProductRating>().HasData(
                new ProductRating
                {
                    ProductRatingId = 1,
                    ProductId = 1,
                    Rating = 4,
                    Review = "Good Product",
                    UserId = 1
                });
            modelBuilder.Entity<ProductViewCount>().HasData(
                new ProductViewCount
                {
                    ProductViewCountId = 1,
                    ProductId = 1,
                    ViewedAt = DateTime.Now,
                });
            modelBuilder.Entity<ProductImages>().HasData(
               new ProductImages
               {
                   ProductImageId = 1,
                   ProductId = 1,
                   Url = "/Resources/Images/Products/s24.jpg",
               });

        }


    }
}
