using Microsoft.AspNetCore.Mvc;
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

        // General method to direct to appropriate login page based on role
        public IActionResult LoginRegister(string role)
        {
            if (role == "Farmer")
            {
                return RedirectToAction("FarmerLogin");
            }

            ViewBag.Role = role;
            return View();
        }

        // Farmer-specific login page
        [HttpGet]
        public IActionResult FarmerLogin()
        {
            ViewBag.Role = "Farmer";
            return View();
        }

        // Employee login/register page
        [HttpGet]
        public IActionResult EmployeeLoginRegister()
        {
            ViewBag.Role = "Employee";
            return View("LoginRegister");
        }

        // Login action for both roles
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

            // Redirect to the appropriate view based on role
            if (role == "Farmer")
            {
                return View("FarmerLogin");
            }

            return View("LoginRegister");
        }

        // Register action only for employees
        [HttpPost]
        public IActionResult Register(string name, string surname, string username, string email, string password, string confirmPassword, string role)
        {
            // Only allow employee registration
            if (role != "Employee")
            {
                ViewBag.Error = "Direct registration is not allowed for this role. Please contact an employee.";
                ViewBag.Role = role;
                return View("LoginRegister");
            }

            var result = _authService.Register(name, surname, username, email, password, confirmPassword, role, HttpContext.Session);
            if (result.Success)
            {
                return RedirectToAction("Index", "Dashboard");
            }

            ViewBag.Error = result.ErrorMessage;
            ViewBag.Role = role;
            return View("LoginRegister");
        }

        // Logout action
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}