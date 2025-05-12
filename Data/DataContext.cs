using Microsoft.EntityFrameworkCore;
using Prog7311_Assignment_2.Models;

namespace Prog7311_Assignment_2.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Farmer> Farmers { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Add indexes for frequently queried columns
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Role);

            // Seed initial data with static values
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Name = "James", Surname = "May", Username = "JamesMay555", Email = "james@gmail.com", Password = "Jamie555", Role = "Farmer" },
                new User { Id = 2, Name = "Jane", Surname = "Smith", Username = "employee1", Email = "jane.smith@example.com", Password = "pass123", Role = "Employee" }
            );

            modelBuilder.Entity<Farmer>().HasData(
                new Farmer { Id = 1, Name = "James", Surname = "May", Username = "JamesMay555", Email = "james@gmail.com", Password = "Jamie555" }
            );

            // Use static dates instead of DateTime.Now
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Apples",
                    Category = "Food",
                    ProductionDate = new DateTime(2025, 5, 1), // Static date: May 1, 2025
                    EndDate = new DateTime(2025, 5, 31),      // Static date: May 31, 2025
                    FarmerId = 1
                }
            );
        }
    }
}