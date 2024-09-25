using Lesson_7.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_7.Context
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source = DESKTOP-VKCKL67\\MSSQLSERVER02; Database=MediaDB;Trusted_Connection=True;TrustServerCertificate=True");
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<RestaurantUser> RestaurantUsers { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<DishCategory> DishCategories { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<DishIngredient> DishIngredients { get; set; }
        public DbSet<MediaUser> MediaUsers { get; set; }
        public DbSet<MediaRestaurant> MediaRestaurants { get; set; }
        public DbSet<MediaMenu> MediaMenus { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<RestaurantUser>()
                .HasKey(ru => new { ru.RestaurantId, ru.UserId }); 

            modelBuilder.Entity<RestaurantUser>()
                .HasOne(ru => ru.Restaurant)
                .WithMany(r => r.RestaurantUsers)
                .HasForeignKey(ru => ru.RestaurantId);

            modelBuilder.Entity<RestaurantUser>()
                .HasOne(ru => ru.User)
                .WithMany(u => u.RestaurantUsers)
                .HasForeignKey(ru => ru.UserId);

            
            modelBuilder.Entity<DishIngredient>()
                .HasKey(di => new { di.IngredientId, di.MenuId }); 

            modelBuilder.Entity<DishIngredient>()
                .HasOne(di => di.Ingredient)
                .WithMany(i => i.DishIngredients)
                .HasForeignKey(di => di.IngredientId);

            modelBuilder.Entity<DishIngredient>()
                .HasOne(di => di.Menu)
                .WithMany(m => m.DishIngredients)
                .HasForeignKey(di => di.MenuId);

            
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            
            modelBuilder.Entity<Review>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade); 

            
            modelBuilder.Entity<Review>()
                .HasOne(r => r.Restaurant)
                .WithMany(rest => rest.Reviews)
                .HasForeignKey(r => r.RestaurantId)
                .OnDelete(DeleteBehavior.Cascade); 

            
            modelBuilder.Entity<Dish>()
                .HasOne(d => d.DishCategory)
                .WithMany(dc => dc.Dishes)
                .HasForeignKey(d => d.DishCategoryId);

            
            modelBuilder.Entity<Restaurant>()
                .HasOne(r => r.Category)
                .WithMany(c => c.Restaurants)
                .HasForeignKey(r => r.CategoryId);

            
            modelBuilder.Entity<Restaurant>()
                .HasOne(r => r.MediaRestaurant)
                .WithOne()
                .HasForeignKey<Restaurant>(r => r.MediaRestaurantId)
                .OnDelete(DeleteBehavior.Cascade);

            
            modelBuilder.Entity<Dish>()
                .HasOne(d => d.MediaMenu)
                .WithOne()
                .HasForeignKey<Dish>(d => d.MediaMenuId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Dish>()
                .Property(d => d.Price)
                .HasColumnType("decimal(18,2)");


            modelBuilder.Entity<Category>()
                .Property(c => c.Name)
                .HasDefaultValue("Uncategorized");

            base.OnModelCreating(modelBuilder);
        }
    }

}
