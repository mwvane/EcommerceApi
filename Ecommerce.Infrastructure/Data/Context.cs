using Ecommerce.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace Ecommerce.Infrastructure.Data
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
        public DbSet<Option> Options { get; set; }
        public DbSet<OptionType> OptionsTypes { get; set; }
        public DbSet<ProductOption> ProductOptions { get; set; }


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

            modelBuilder.Entity<ProductOption>()
             .HasKey(po => new { po.Id, po.OptionId });

            modelBuilder.Entity<ProductOption>()
                .HasOne(po => po.Product)
                .WithMany(p => p.ProductOptions)
                .HasForeignKey(po => po.Id);

            modelBuilder.Entity<ProductOption>()
                .HasOne(po => po.Option)
                .WithMany(o => o.ProductOptions)
                .HasForeignKey(po => po.OptionId);

            modelBuilder.Entity<Option>()
            .HasOne(o => o.OptionType)
            .WithMany(ot => ot.Options)
            .HasForeignKey(o => o.OptionTypeId);

            // INITIAL DATA
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "PC Components", Image = "bi bi-memory" },
                new Category { Id = 2, Name = "RAM", ParentCategoryId = 1, Image = "bi bi-nvme" },
                new Category { Id = 3, Name = "Video Card", ParentCategoryId = 1, Image = "bi bi-nvidia" },
                new Category { Id = 4, Name = "HDD", ParentCategoryId = 1, Image = "bi bi-hdd" },
                new Category { Id = 5, Name = "SSD", ParentCategoryId = 1, Image = "bi bi-nvme" },
                new Category { Id = 6, Name = "Speaker", ParentCategoryId = 1, Image = "bi bi-speaker" },
                new Category { Id = 7, Name = "Microphone", ParentCategoryId = 1, Image = "bi bi-mic" },
                new Category { Id = 8, Name = "Web Camera", ParentCategoryId = 1, Image = "bi bi-webcam" },
                new Category { Id = 9, Name = "Console", Image = "bi bi-joystick" },
                new Category { Id = 10, Name = "Playstation", ParentCategoryId = 9, Image = "bi bi-playstation" },
                new Category { Id = 11, Name = "Xbox", ParentCategoryId = 9, Image = "bi bi-xbox" },
                new Category { Id = 12, Name = "Tecnic", Image = "bi bi-laptop" },
                new Category { Id = 13, Name = "Smarthone", ParentCategoryId = 12, Image = "bi bi-tablet" },
                new Category { Id = 14, Name = "TV", ParentCategoryId = 12, Image = "bi bi-tv" },
                new Category { Id = 15, Name = "Smart watch", ParentCategoryId = 12, Image = "bi bi-watch" },
                new Category { Id = 16, Name = "Keyboard", ParentCategoryId = 1, Image = "bi bi-keyboard" },
                new Category { Id = 17, Name = "Cloth", Image = "bi bi-scissors" },
                new Category { Id = 18, Name = "Hat", ParentCategoryId = 17, Image = "bi bi-backpack2" },
                new Category { Id = 19, Name = "Android", ParentCategoryId = 13, Image = "bi bi-android2" },
                new Category { Id = 20, Name = "IOS", ParentCategoryId = 13, Image = "bi bi-apple" },
                new Category { Id = 21, Name = "Iphone", ParentCategoryId = 20, Image = "bi bi-phone-fill" },
                new Category { Id = 22, Name = "Iphone", ParentCategoryId = 20, Image = "bi bi-tablet-fill" }
                );

            modelBuilder.Entity<Country>().HasData(
                new Country { Id = 1, Name = "South Korea", Image = "/Resources/Images/Countries/South-Korea.jpg" },
                new Country { Id = 2, Name = "Jamaica", Image = "/Resources/Images/Countries/Jamaica.jpg" },
                new Country { Id = 3, Name = "Georgia", Image = "/Resources/Images/Countries/Georgia.jpg" });

            modelBuilder.Entity<Manufacturer>().HasData(
                new Manufacturer { Id = 1, Name = "Samsung", CountryId = 1 },
                new Manufacturer { Id = 2, Name = "Kingstone", CountryId = 2 });

            //modelBuilder.Entity<Option>().HasData(
            //    new Option { OptionId = 1, Name = "Color", Value = "Black", OptionType = OptionType.Color },
            //    new Option { OptionId = 2, Name = "Color", Value = "Red", OptionType = OptionType.Color },
            //    new Option { OptionId = 3, Name = "Color", Value = "Blue", OptionType = OptionType.Color },
            //    new Option { OptionId = 4, Name = "Color", Value = "Cian", OptionType = OptionType.Color },
            //    new Option { OptionId = 5, Name = "Screen Type", Value = "Super Amoled", OptionType = OptionType.ScreenType },
            //    new Option { OptionId = 6, Name = "RAM", Value = "4GB", OptionType = OptionType.RAM },
            //    new Option { OptionId = 7, Name = "RAM", Value = "8GB", OptionType = OptionType.RAM });

            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 1,
                Name = "Samsung Galaxy s24 ultra",
                Price = 3500,
                CategoryId = 2,
                ManufacturerId = 1,
                Description = "100x zoom",
            });

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Email = "test@gmail.com",
                    Role = UserRole.Client,
                    Password = "password",
                    Firstname = "test",
                    Lastname = "password",
                    Phone = "598456000",
                    Image = "",
                });

            modelBuilder.Entity<ProductRating>().HasData(
                new ProductRating
                {
                    Id = 1,
                    ProductId = 1,
                    Rating = 4,
                    Review = "Good Product",
                    UserId = 1
                });
            modelBuilder.Entity<ProductViewCount>().HasData(
                new ProductViewCount
                {
                    Id = 1,
                    ProductId = 1,
                    ViewedAt = DateTime.Now,
                });
            modelBuilder.Entity<ProductImages>().HasData(
               new ProductImages
               {
                   Id = 1,
                   ProductId = 1,
                   Url = "/Resources/Images/Products/s24.jpg",
               });

        }


    }
}
