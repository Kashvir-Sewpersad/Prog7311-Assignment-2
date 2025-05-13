
////////////////////////////////////////////// start of file ///////////////////////////////////////


//------------------ start of imports -----------------//

using Microsoft.AspNetCore.Mvc;

using Prog7311_Assignment_2.Models;

using Prog7311_Assignment_2.Services;

using Prog7311_Assignment_2.Data;

//-------------------- end of imports -----------------//


namespace Prog7311_Assignment_2.Controllers
{
    //***************************************** start of code ***************************************//
    public class DashboardController : Controller
    {
        //------------- start of global variables ------------//

        private readonly ProductService _productService;

        private readonly FarmerService _farmerService;

        private readonly DataContext _context;

        //--------------- end of global variables -------------// 
        public DashboardController(ProductService productService, FarmerService farmerService, DataContext context)
        {
            _productService = productService;

            _farmerService = farmerService;

            _context = context;
        }
        /*
         
         // the below code is to control the actions chosen in the home page 
         
         
         */
        public IActionResult Index()
        {
            var role = HttpContext.Session.GetString("Role");

            var userIdString = HttpContext.Session.GetString("UserId") ?? "0";

            var userId = int.Parse(userIdString);
             
            
            ///////////// farmer ///////////////////////
            if (role == "Farmer")
            {
                var farmer = _context.Farmers.FirstOrDefault(f => f.UserId == userId); // id is set 



                if (farmer == null) // if the farmer is not found 
                {
                    TempData["Error"] = "Farmer account not found. Please contact an employee to register your account."; // error message shall display 

                    return RedirectToAction("LoginRegister", "Auth", new { role = "Farmer" });
                }


                var products = _productService.GetProductsByFarmer(farmer.Id); // retrive the products created by a set farmer using the id (unique to each farmer due to me adding auto increment in database )

                return View("FarmerDashboard", products); // displaying the products in the farmer dash 
            }


            /////////////// employee /////////////
            else if (role == "Employee")
            {
                var model = new EmployeeDashboardViewModel
                {
                    Products = _productService.GetAllProducts(), // retrive products 

                    Farmers = _farmerService.GetAllFarmers(), // retrive farmers 

                    Employees = _context.Users.Where(u => u.Role == "Employee").ToList()
                };

                return View("EmployeeDashboard", model);
            }
            return RedirectToAction("Index", "Home");
        }



        /*
         
         // below code contains the logic to add a new product to the farmer dashboard. 

        
         
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

            Console.WriteLine($"UserId: {userId}");

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




        //****************************** edit product is not required in the rubric and does not work correctly as yet ******************************************//
        
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



        //**************************** delete product is not in the rubric and contains a few bugs ************************//
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


        /*
         below code is to allow a employee to add a farmer by filling the information listed on the form
         */
        [HttpPost]
        public IActionResult AddFarmer(string name, string surname, string username, string email, string password, string confirmPassword)
        {
            var result = _farmerService.AddFarmer(name, surname, username, email, password, confirmPassword); //  adding 
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

        /*
         below code is to filter the products based on category and date 
         */
        [HttpGet]
        public IActionResult FilterProducts(string category, DateTime? startDate, DateTime? endDate)
        {
            var products = _productService.FilterProducts(category, startDate, endDate);

            var model = new EmployeeDashboardViewModel
            {
                Products = products,

                Farmers = _farmerService.GetAllFarmers(),

                Employees = _context.Users.Where(u => u.Role == "Employee").ToList()
            };
            return View("EmployeeDashboard", model);
        }
    }

    public class EmployeeDashboardViewModel
    {
        public List<Product> Products { get; set; } // Added to hold loaded products 
        public List<Farmer> Farmers { get; set; } // Added to hold registered farmers
        public List<User> Employees { get; set; } // Added to hold registered employees
    }

    //*********************************** end of code ***************************//
}
////////////////////////////////////////////// end  of file ///////////////////////////////////////