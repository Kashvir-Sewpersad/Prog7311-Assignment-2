
//////////////////////////////////////////////////////// start of file /////////////////////////////////////////////////////////
///
//-------------------- start of imports ----------------//
using Microsoft.AspNetCore.Mvc;

using Prog7311_Assignment_2.Models;

using Prog7311_Assignment_2.Services;

using Prog7311_Assignment_2.Data;

//--------------------------- end of imports ---------------------//
namespace Prog7311_Assignment_2.Controllers
{
    public class DashboardController : Controller
    {
        //***************************************** start of code ************************//

        private readonly ProductService _productService;

        private readonly FarmerService _farmerService;

        private readonly DataContext _context;

        public DashboardController(ProductService productService, FarmerService farmerService, DataContext context)
        {
            _productService = productService;

            _farmerService = farmerService;

            _context = context;
        }

        public IActionResult Index()
        {
            var role = HttpContext.Session.GetString("Role");

            var userIdString = HttpContext.Session.GetString("UserId") ?? "0";

            var userId = int.Parse(userIdString);

            // Farmer
            if (role == "Farmer")
            {
                var farmer = _context.Farmers.FirstOrDefault(f => f.UserId == userId);

                if (farmer == null)
                {
                    TempData["Error"] = "Farmer account not found. Please contact an employee to register your account.";
                    return RedirectToAction("LoginRegister", "Auth", new { role = "Farmer" });
                }

                var products = _productService.GetProductsByFarmer(farmer.Id);
                return View("FarmerDashboard", products);
            }
            // Employee 
            else if (role == "Employee")
            {
                var model = new EmployeeDashboardViewModel
                {
                    Products = _productService.GetAllProducts(),

                    Farmers = _farmerService.GetAllFarmers(),

                    Employees = _context.Users.Where(u => u.Role == "Employee").ToList()
                };

                return View("EmployeeDashboard", model);
            }
            return RedirectToAction("Index", "Home");
        }
        /*
         below method is to add a product to the system 
         */
        [HttpPost]
        public IActionResult AddProduct(string name, string category, DateTime productionDate, DateTime endDate, string otherCategory)
        {

            var userIdString = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(userIdString) || userIdString == "0")
            {
                return RedirectToAction("LoginRegister", "Auth", new { role = "Farmer" });
            }
            var userId = int.Parse(userIdString);


            /*
             this was causing absolute hell due to the fact that keeping consistency from users is practically imposible 

                  // as such i am leaving it in but removed it from the ui 
             
             */


            if (category == "Other" && !string.IsNullOrEmpty(otherCategory)) 
            {
                category = otherCategory;
            }

            var (success, errorMessage) = _productService.AddProduct(name, category, productionDate, endDate, userId);
            if (!success)
            {
                if (errorMessage.Contains("Farmer with UserId"))
                {
                    TempData["Error"] = "Your farmer profile is missing. Please contact an employee to register your account.";

                    return RedirectToAction("LoginRegister", "Auth", new { role = "Farmer" });
                }
                TempData["Error"] = errorMessage;
            }
            return RedirectToAction("Index");
        }
        /*
         
         below function  is to allow farmers to edit their producuts 
         */
        [HttpPost]
        public IActionResult EditProduct(int id, string name, string category, DateTime productionDate, DateTime endDate)
        {
            var userId = int.Parse(HttpContext.Session.GetString("UserId") ?? "0");
            var farmer = _context.Farmers.FirstOrDefault(f => f.UserId == userId);

            if (farmer == null)
            {
                TempData["Error"] = "Farmer account not found. Please contact an employee to register your account.";

                return RedirectToAction("Index");
            }

            var otherCategoryKey = $"otherCategoryEdit_{id}";

            var otherCategory = Request.Form.ContainsKey(otherCategoryKey) ? Request.Form[otherCategoryKey].ToString() : null;

            if (category == "Other" && !string.IsNullOrEmpty(otherCategory))
            {
                category = otherCategory;
            }
            var result = _productService.EditProduct(id, name, category, productionDate, endDate, farmer.Id);

            if (!result)
            {
                TempData["Error"] = "Failed to edit product. Ensure the product exists and the production date is before the end date.";
            }
            return RedirectToAction("Index");
        }
        /*
         delete product is not part of the rubric 

                // ive added this in as i intend to work on this project in my holiday 

        // currently non functional 
         */
        [HttpPost]
        public IActionResult DeleteProduct(int id)
        {
            var userId = int.Parse(HttpContext.Session.GetString("UserId") ?? "0");

            var farmer = _context.Farmers.FirstOrDefault(f => f.UserId == userId);

            if (farmer == null)
            {
                TempData["Error"] = "Farmer account not found. Please contact an employee to register your account.";

                return RedirectToAction("Index");
            }

            var result = _productService.DeleteProduct(id, farmer.Id);
            if (!result)
            {
                TempData["Error"] = "Failed to delete product. Ensure the product exists.";
            }
            return RedirectToAction("Index");
        }
         // code to manage add farmer actions 
        [HttpPost]
        public IActionResult AddFarmer(string name, string surname, string username, string email, string password, string confirmPassword)
        {
            var result = _farmerService.AddFarmer(name, surname, username, email, password, confirmPassword);

            if (!result.Success)
            {
                TempData["Error"] = result.ErrorMessage;
            }
            else
            {
                TempData["Success"] = "Farmer added successfully.";
            }
            return RedirectToAction("Index");
        }

        // Enhanced FilterProducts action to handle date range filtering
        [HttpGet]
        public IActionResult FilterProducts(string category, DateTime? startDate, DateTime? endDate)
        {
            // Validate date range if both are provided
            if (startDate.HasValue && endDate.HasValue && startDate > endDate)
            {
                TempData["Error"] = "Start date must be before end date.";
                return RedirectToAction("Index");
            }

            var products = _productService.FilterProducts(category, startDate, endDate);

            var model = new EmployeeDashboardViewModel
            {
                Products = products,
                Farmers = _farmerService.GetAllFarmers(),
                Employees = _context.Users.Where(u => u.Role == "Employee").ToList()
            };

            // Pass filter parameters back to the view for maintaining state
            ViewBag.SelectedCategory = category;

            ViewBag.StartDate = startDate?.ToString("yyyy-MM-dd");

            ViewBag.EndDate = endDate?.ToString("yyyy-MM-dd");

            return View("EmployeeDashboard", model);
        }
    }

    public class EmployeeDashboardViewModel
    {
        public List<Product> Products { get; set; }
        public List<Farmer> Farmers { get; set; }
        public List<User> Employees { get; set; }
    }
    //********************************************* end of code ****************************//
}
//////////////////////////////////////////////////////////////////// end of file ///////////////////////////////////////////////////