using Microsoft.AspNetCore.Mvc;
using Prog7311_Assignment_2.Models;
using System.Linq;

namespace Prog7311_Assignment_2.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult LoginRegister(string role)
        {
            ViewBag.Role = role;
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password, string role)
        {
            var user = DataStore.Users.FirstOrDefault(u => u.Username == username && u.Password == password && u.Role == role);
            if (user != null)
            {
                HttpContext.Session.SetString("UserId", user.Id.ToString());
                HttpContext.Session.SetString("Role", user.Role);
                return RedirectToAction("Index", "Dashboard");
            }
            ViewBag.Error = "Invalid credentials";
            ViewBag.Role = role;
            return View("LoginRegister");
        }

        [HttpPost]
        public IActionResult Register(string username, string password, string role)
        {
            if (DataStore.Users.Any(u => u.Username == username))
            {
                ViewBag.Error = "Username already exists";
                ViewBag.Role = role;
                return View("LoginRegister");
            }

            var newUser = new User
            {
                Id = DataStore.Users.Count + 1,
                Username = username,
                Password = password,
                Role = role
            };
            DataStore.Users.Add(newUser);
            HttpContext.Session.SetString("UserId", newUser.Id.ToString());
            HttpContext.Session.SetString("Role", newUser.Role);
            return RedirectToAction("Index", "Dashboard");
        }
    }
}