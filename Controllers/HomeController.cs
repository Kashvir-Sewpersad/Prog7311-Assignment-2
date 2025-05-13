

//////////////////////////////////////////// start of file /////////////////////////////////////


//---------------------------- start of imports -------------//

using Microsoft.AspNetCore.Mvc;

using Prog7311_Assignment_2.Models;

using System.Diagnostics;

//------------------------------- end of imports --------------// 

namespace Prog7311_Assignment_2.Controllers
{
    //*********************** start of code ************************//

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        // home page 
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SelectRole(string role)
        {
            // Direct action for   farmers to farmer login, employees to login/register
            if (role == "Farmer")
            {
                return RedirectToAction("FarmerLogin", "Auth");
            }
            else
            {
                return RedirectToAction("EmployeeLoginRegister", "Auth");
            }
        }

        public IActionResult Contact()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
    //******************************* end of code *******************************//
}

//////////////////////////////////////////////////////// end of file //////////////////////////////////////////////