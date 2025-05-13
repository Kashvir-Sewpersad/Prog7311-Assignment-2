

/////////////////////////////////////// start of file  ////////////////////////////////
///
// ------------ start of imports ----------------//

using Microsoft.AspNetCore.Mvc;

using Prog7311_Assignment_2.Services;

//--------------------- end of imports ---------// 

namespace Prog7311_Assignment_2.Controllers
{
    //****************************************** start of code ******************************///
    public class AuthController : Controller
    {
        private readonly AuthService _authService;  // variable  of the authorising service 

        /*
         setting up an instance of the authService 
         */
        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        //  method to direct to appropriate login page based on role : farmer or employee 
        public IActionResult LoginRegister(string role)
        {
            if (role == "Farmer")
            {
                return RedirectToAction("FarmerLogin");
            }

            ViewBag.Role = role;
            return View(); 
        }

        // Farmer  login page
        [HttpGet]
        public IActionResult FarmerLogin()
        {
            ViewBag.Role = "Farmer";

            return View();
        }

        // Employee login + register page
        [HttpGet]
        public IActionResult EmployeeLoginRegister()
        {
            ViewBag.Role = "Employee";

            return View("LoginRegister");
        }

        // Login action for both roles: farmer and employee 
        /*
         this method works by taking in the username, password and role and testing it againt data stored in the database 

                if the login is a success the user is directed to dashboard based on role

                    if the user is unsucessful they are redirected bacck to the home page 
         */
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

            // Redirect to the appropriate view based on role selected 
            if (role == "Farmer")
            {
                return View("FarmerLogin");
            }

            return View("LoginRegister");
        }

        // Register action only for the  employees
        [HttpPost]
        public IActionResult Register(string name, string surname, string username, string email, string password, string confirmPassword, string role)
        {
            // if a farmer aattempt to register, they will encounter an error 
            // only employees can register 
            if (role != "Employee")
            {
                ViewBag.Error = "Direct registration is not allowed for this role. Please contact an employee.";

                ViewBag.Role = role;
                return View("LoginRegister"); // return to view 
            }


            // success login logic 
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
    //************************************************** end of code ***********************************//
}
////////////////////////////////////// end of file //////////////////////////////