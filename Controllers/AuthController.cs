
//////////////////////////////////////////////////// Start of file ///////////////////////////////////////////////////

//******************************* Start of imports ******************************//
using Microsoft.AspNetCore.Mvc;

using Prog7311_Assignment_2.Services;

//******************************* End of file *********************************//

namespace Prog7311_Assignment_2.Controllers
{
    public class AuthController : Controller
    {
        private readonly AuthService _authService; // private variable for authentication service 

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        /*
         below is a login function which is used to manage which role is selected for login and registering 
         */
        public IActionResult LoginRegister(string role)
        {
            ViewBag.Role = role;

            return View(); // returning a view
        }


        /*
         * 
         * 
         below is a fucntion for the actions regarding the login poceedure 

            the login passes the variables of username, password and the role selected into the authentication services for verification 

                if the output is success the user will be redirected to the home dashboard 
         
        should a failure occure an error message will be displayed and the user will be redirected to the login/ register  form 
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

            return View("LoginRegister");
        }

        /*
         the following code is to allow the registration of users by making use of vatiabls listed bellow 
            the variables are passed into the authentication service 

        if the outcome is a success the user will be sent to their respective dashboard 

        if a fail the user will be redirected to the rigister form 
         
         */
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
        
        [HttpGet]
        public IActionResult FarmerLogin()
        {
            return View();
        }

        

        [HttpGet]
        public IActionResult RegisterFarmer()
        {
            return View("FarmerRegister");
        }
    }
}
///////////////////////////////////////////////////// end of file ///////////////////////////////////////////////////////////