using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Prog7311_Assignment_2.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public string Role { get; set; } // "Farmer" or "Employee"
    }

    public class Farmer
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string ContactInfo { get; set; }
    }

    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public DateTime ProductionDate { get; set; }
        public int FarmerId { get; set; }
    }

    public class Role
    {
        public string Name { get; set; }
    }

    // In-memory data store (temporary)
    public static class DataStore
    {
        public static List<User> Users { get; set; } = new List<User>
        {
            new User { Id = 1, Username = "farmer1", Password = "pass123", Role = "Farmer" },
            new User { Id = 2, Username = "employee1", Password = "pass123", Role = "Employee" }
        };

        public static List<Farmer> Farmers { get; set; } = new List<Farmer>
        {
            new Farmer { Id = 1, Name = "John Doe", ContactInfo = "john@example.com" }
        };

        public static List<Product> Products { get; set; } = new List<Product>
        {
            new Product { Id = 1, Name = "Apples", Category = "Fruit", ProductionDate = DateTime.Now.AddDays(-10), FarmerId = 1 }
        };
    }
}