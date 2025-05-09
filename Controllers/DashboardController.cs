using Microsoft.AspNetCore.Mvc;
using Prog7311_Assignment_2.Models;
using System;
using System.Linq;

namespace Prog7311_Assignment_2.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            var role = HttpContext.Session.GetString("Role");
            var userId = int.Parse(HttpContext.Session.GetString("UserId") ?? "0");

            if (role == "Farmer")
            {
                var products = DataStore.Products.Where(p => p.FarmerId == userId).ToList();
                return View("FarmerDashboard", products);
            }
            else if (role == "Employee")
            {
                var model = new EmployeeDashboardViewModel
                {
                    Products = DataStore.Products,
                    Farmers = DataStore.Farmers
                };
                return View("EmployeeDashboard", model);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult AddProduct(string name, string category, DateTime productionDate)
        {
            var userId = int.Parse(HttpContext.Session.GetString("UserId") ?? "0");
            var product = new Product
            {
                Id = DataStore.Products.Count + 1,
                Name = name,
                Category = category,
                ProductionDate = productionDate,
                FarmerId = userId
            };
            DataStore.Products.Add(product);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult AddFarmer(string name, string contactInfo)
        {
            var farmer = new Farmer
            {
                Id = DataStore.Farmers.Count + 1,
                Name = name,
                ContactInfo = contactInfo
            };
            DataStore.Farmers.Add(farmer);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult FilterProducts(string category, DateTime? startDate, DateTime? endDate)
        {
            var products = DataStore.Products.AsQueryable();
            if (!string.IsNullOrEmpty(category))
                products = products.Where(p => p.Category == category);
            if (startDate.HasValue)
                products = products.Where(p => p.ProductionDate >= startDate.Value);
            if (endDate.HasValue)
                products = products.Where(p => p.ProductionDate <= endDate.Value);

            var model = new EmployeeDashboardViewModel
            {
                Products = products.ToList(),
                Farmers = DataStore.Farmers
            };
            return View("EmployeeDashboard", model);
        }
    }

    public class EmployeeDashboardViewModel
    {
        public List<Product> Products { get; set; }
        public List<Farmer> Farmers { get; set; }
    }
}