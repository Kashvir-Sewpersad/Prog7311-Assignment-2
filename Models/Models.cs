using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Prog7311_Assignment_2.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Username { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
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
        public string Surname { get; set; }
        [Required]
        public string Username { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
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
        [Required]
        public DateTime EndDate { get; set; }
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
            new User { Id = 1, Name = "James", Surname = "May", Username = "JamesMay555", Email = "james@gmail.com", Password = "Jamie555", Role = "Farmer" },
            new User { Id = 2, Name = "Jane", Surname = "Smith", Username = "employee1", Email = "jane.smith@example.com", Password = "pass123", Role = "Employee" }
        };

        public static List<Farmer> Farmers { get; set; } = new List<Farmer>
        {
            new Farmer { Id = 1, Name = "James", Surname = "May", Username = "JamesMay555", Email = "james@gmail.com", Password = "Jamie555" }
        };

        public static List<Product> Products { get; set; } = new List<Product>
        {
            new Product { Id = 1, Name = "Apples", Category = "Food", ProductionDate = DateTime.Now.AddDays(-10), EndDate = DateTime.Now.AddDays(20), FarmerId = 1 }
        };
    }
}