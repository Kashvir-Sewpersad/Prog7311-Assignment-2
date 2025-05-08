using Microsoft.AspNetCore.Mvc;
using Prog7311_Assignment_2.Models;
using System.Diagnostics;

namespace Prog7311_Assignment_2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

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
            if (role == "Farmer" || role == "Employee")
            {
                return RedirectToAction("LoginRegister", "Auth", new { role });
            }
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}