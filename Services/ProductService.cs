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

        public (bool Success, string ErrorMessage) AddProduct(string name, string category, DateTime productionDate, DateTime endDate, int farmerId)
        {
            if (productionDate >= endDate)
            {
                return (false, "Production date must be before the end date.");
            }

            if (!_context.Farmers.Any(f => f.Id == farmerId))
            {
                return (false, $"Farmer with ID {farmerId} does not exist.");
            }

            var product = new Product
            {
                Name = name,
                Category = category,
                ProductionDate = productionDate,
                EndDate = endDate,
                FarmerId = farmerId
            };

            _context.Products.Add(product);
            _context.SaveChanges();
            return (true, null);
        }

        public bool EditProduct(int id, string name, string category, DateTime productionDate, DateTime endDate, int farmerId)
        {
            var product = _context.Products
                .FirstOrDefault(p => p.Id == id && p.FarmerId == farmerId);
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

        public bool DeleteProduct(int id, int farmerId)
        {
            var product = _context.Products
                .FirstOrDefault(p => p.Id == id && p.FarmerId == farmerId);
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