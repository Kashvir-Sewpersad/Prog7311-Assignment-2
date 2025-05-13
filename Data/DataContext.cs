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

            // Seed Users table
            modelBuilder.Entity<User>().HasData(
                // Seeded Employee
                new User
                {
                    Id = 1,
                    Name = "Emma",
                    Surname = "Watson",
                    Username = "emma.watson",
                    Email = "emma.watson@example.com",
                    Password = "emma123",
                    Role = "Employee"
                },
                // Seeded Farmer (User entry)
                new User
                {
                    Id = 2,
                    Name = "Liam",
                    Surname = "Smith",
                    Username = "liam.smith",
                    Email = "liam.smith@example.com",
                    Password = "liam456",
                    Role = "Farmer"
                }
            );

            // Seed Farmers table
            modelBuilder.Entity<Farmer>().HasData(
                new Farmer
                {
                    Id = 1,
                    Name = "Liam",
                    Surname = "Smith",
                    Username = "liam.smith",
                    Email = "liam.smith@example.com",
                    Password = "liam456",
                    UserId = 2
                }
            );

            // Seed Products table for the farmer
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Organic Carrots",
                    Category = "Vegetables",
                    ProductionDate = new DateTime(2025, 4, 1),
                    EndDate = new DateTime(2025, 4, 30),
                    FarmerId = 1
                },
                new Product
                {
                    Id = 2,
                    Name = "Fresh Milk",
                    Category = "Dairy",
                    ProductionDate = new DateTime(2025, 5, 1),
                    EndDate = new DateTime(2025, 5, 15),
                    FarmerId = 1
                },
                new Product
                {
                    Id = 3,
                    Name = "Wheat Flour",
                    Category = "Wheat",
                    ProductionDate = new DateTime(2025, 5, 1),
                    EndDate = new DateTime(2025, 6, 1),
                    FarmerId = 1
                }
            );

            // Configure the relationship between Farmer and User
            modelBuilder.Entity<Farmer>()
                .HasOne(f => f.User)
                .WithMany(u => u.Farmers)
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}