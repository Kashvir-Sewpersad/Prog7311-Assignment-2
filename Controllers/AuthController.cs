using Microsoft.AspNetCore.Mvc;
using Prog7311_Assignment_2.Models;
using Prog7311_Assignment_2.Services;

namespace Prog7311_Assignment_2.Controllers
{
    public class AuthController : Controller
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        public IActionResult LoginRegister(string role)
        {
            ViewBag.Role = role;
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password, string role)
        {
            var result = _authService.Login(username, password, role, HttpContext.Session);
            if (result.Success)
            {
                return RedirectToAction("Index", "Dashboard");
            }
            ViewBag.Error = result.ErrorMessage;
            ViewBag.Role = role;
            return View("LoginRegister");
        }

        [HttpPost]
        public IActionResult Register(string name, string surname, string username, string email, string password, string confirmPassword, string role)
        {
            var result = _authService.Register(name, surname, username, email, password, confirmPassword, role, HttpContext.Session);
            if (result.Success)
            {
                return RedirectToAction("Index", "Dashboard");
            }
            ViewBag.Error = result.ErrorMessage;
            ViewBag.Role = role;
            return View("LoginRegister");
        }
    }
}