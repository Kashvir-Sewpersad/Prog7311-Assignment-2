using Prog7311_Assignment_2.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Prog7311_Assignment_2.Services
{
    public class ProductService
    {
        public List<Product> GetProductsByFarmer(int farmerId)
        {
            return DataStore.Products.Where(p => p.FarmerId == farmerId).ToList();
        }

        public List<Product> GetAllProducts()
        {
            return DataStore.Products.ToList();
        }

        public bool AddProduct(string name, string category, DateTime productionDate, DateTime endDate, int farmerId)
        {
            if (productionDate >= endDate)
            {
                return false; // Validation: Production date should be before end date
            }
            var product = new Product
            {
                Id = DataStore.Products.Count + 1,
                Name = name,
                Category = category,
                ProductionDate = productionDate,
                EndDate = endDate,
                FarmerId = farmerId
            };
            DataStore.Products.Add(product);
            return true;
        }

        public bool EditProduct(int id, string name, string category, DateTime productionDate, DateTime endDate, int farmerId)
        {
            var product = DataStore.Products.FirstOrDefault(p => p.Id == id && p.FarmerId == farmerId);
            if (product == null || productionDate >= endDate)
            {
                return false;
            }
            product.Name = name;
            product.Category = category;
            product.ProductionDate = productionDate;
            product.EndDate = endDate;
            return true;
        }

        public bool DeleteProduct(int id, int farmerId)
        {
            var product = DataStore.Products.FirstOrDefault(p => p.Id == id && p.FarmerId == farmerId);
            if (product == null)
            {
                return false;
            }
            DataStore.Products.Remove(product);
            return true;
        }

        public List<Product> FilterProducts(string category, DateTime? startDate, DateTime? endDate)
        {
            var products = DataStore.Products.AsQueryable();
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