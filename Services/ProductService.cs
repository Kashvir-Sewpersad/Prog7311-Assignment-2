using Microsoft.EntityFrameworkCore;
using Prog7311_Assignment_2.Data;
using Prog7311_Assignment_2.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Prog7311_Assignment_2.Services
{
    public class ProductService
    {
        private readonly DataContext _context;

        public ProductService(DataContext context)
        {
            _context = context;
        }

        public class ProductResult
        {
            public bool Success { get; set; }
            public string ErrorMessage { get; set; }
        }

        public List<Product> GetProductsByFarmer(int farmerId)
        {
            return _context.Products
                .Where(p => p.FarmerId == farmerId)
                .ToList();
        }

        public List<Product> GetAllProducts()
        {
            return _context.Products.ToList();
        }

        public (bool Success, string ErrorMessage) AddProduct(string name, string category, DateTime productionDate, DateTime endDate, int userId)
        {
            var farmer = _context.Farmers.FirstOrDefault(f => f.UserId == userId);
            if (farmer == null)
            {
                return (false, $"Farmer with UserId {userId} not found.");
            }

            if (productionDate >= endDate)
            {
                return (false, "Production Date must be before End Date.");
            }

            var product = new Product
            {
                Name = name,
                Category = category,
                ProductionDate = productionDate,
                EndDate = endDate,
                FarmerId = farmer.Id
            };

            try
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, $"Failed to add product: {ex.Message}");
            }
        }

        public bool EditProduct(int id, string name, string category, DateTime productionDate, DateTime endDate, int userId)
        {
            var farmer = _context.Farmers.FirstOrDefault(f => f.UserId == userId);
            if (farmer == null)
            {
                return false;
            }

            var product = _context.Products
                .FirstOrDefault(p => p.Id == id && p.FarmerId == farmer.Id);
            if (product == null || productionDate >= endDate)
            {
                return false;
            }

            product.Name = name;
            product.Category = category;
            product.ProductionDate = productionDate;
            product.EndDate = endDate;
            _context.SaveChanges();
            return true;
        }

        public bool DeleteProduct(int id, int userId)
        {
            var farmer = _context.Farmers.FirstOrDefault(f => f.UserId == userId);
            if (farmer == null)
            {
                return false;
            }

            var product = _context.Products
                .FirstOrDefault(p => p.Id == id && p.FarmerId == farmer.Id);
            if (product == null)
            {
                return false;
            }

            _context.Products.Remove(product);
            _context.SaveChanges();
            return true;
        }

        public List<Product> FilterProducts(string category, DateTime? startDate, DateTime? endDate)
        {
            var products = _context.Products.AsQueryable();
            if (!string.IsNullOrEmpty(category))
                products = products.Where(p => p.Category == category);
            if (startDate.HasValue)
                products = products.Where(p => p.ProductionDate >= startDate.Value);
            if (endDate.HasValue)
                products = products.Where(p => p.EndDate <= endDate.Value);
            return products.ToList();
        }
    }
}