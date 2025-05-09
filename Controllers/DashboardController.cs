using Microsoft.AspNetCore.Mvc;
using Prog7311_Assignment_2.Models;
using Prog7311_Assignment_2.Services;
using System;

namespace Prog7311_Assignment_2.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ProductService _productService;
        private readonly FarmerService _farmerService;

        public DashboardController(ProductService productService, FarmerService farmerService)
        {
            _productService = productService;
            _farmerService = farmerService;
        }

        public IActionResult Index()
        {
            var role = HttpContext.Session.GetString("Role");
            var userId = int.Parse(HttpContext.Session.GetString("UserId") ?? "0");

            if (role == "Farmer")
            {
                var products = _productService.GetProductsByFarmer(userId);
                return View("FarmerDashboard", products);
            }
            else if (role == "Employee")
            {
                var model = new EmployeeDashboardViewModel
                {
                    Products = _productService.GetAllProducts(),
                    Farmers = _farmerService.GetAllFarmers()
                };
                return View("EmployeeDashboard", model);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult AddProduct(string name, string category, DateTime productionDate)
        {
            var userId = int.Parse(HttpContext.Session.GetString("UserId") ?? "0");
            var result = _productService.AddProduct(name, category, productionDate, userId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult AddFarmer(string name, string contactInfo)
        {
            var result = _farmerService.AddFarmer(name, contactInfo);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult FilterProducts(string category, DateTime? startDate, DateTime? endDate)
        {
            var products = _productService.FilterProducts(category, startDate, endDate);
            var model = new EmployeeDashboardViewModel
            {
                Products = products,
                Farmers = _farmerService.GetAllFarmers()
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