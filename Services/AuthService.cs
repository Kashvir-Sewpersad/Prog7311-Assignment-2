

////////////////////////////////////////////// start of file ////////////////////////////////////////
///

//------------------- start of imports -----------------//
using Prog7311_Assignment_2.Data;

using Prog7311_Assignment_2.Models;

//--------------------- end of imports -------------------// 

namespace Prog7311_Assignment_2.Services
{
    //************************************** start of code ***************************************//
    public class AuthService
    {
        private readonly DataContext _context; // variable for database : datacontext 

        public AuthService(DataContext context)
        {
            _context = context;
        }

        // variables for success or errors 
        public class AuthResult
        {
            public bool Success { get; set; }
            public string ErrorMessage { get; set; }
        }

        /*
         below code is for login actions using variables entered on form 

        // the process of login works by comparinf data that is entered against stored data from registering 
         */
        public AuthResult Login(string username, string password, string role, ISession session)
        {
            var user = _context.Users
                .FirstOrDefault(u => u.Username == username && u.Password == password && u.Role == role);

            if (user != null) // if the details match , the result will be a success 
                    {
                        session.SetString("UserId", user.Id.ToString());

                        session.SetString("Role", user.Role);

                        return new AuthResult { Success = true };
                    }

            return new AuthResult { Success = false, ErrorMessage = "Invalid credentials" }; // error message 
        }
        /*
         below code is to handle the process of storing data from a registration process, making use of the variables below 
         
         */
            public AuthResult Register(string name, string surname, string username, string email, string password, string confirmPassword, string role, ISession session)
        {
            if (_context.Users.Any(u => u.Username == username))
                {
                    return new AuthResult { Success = false, ErrorMessage = "Username already exists" }; // user credentials stored in databse 
                }

                    if (password != confirmPassword)
                            {
                                return new AuthResult { Success = false, ErrorMessage = "Passwords do not match" }; // error mesasge 
                            }
                    // save new user 
            var newUser = new User
            {
                Name = name,

                Surname = surname,

                Username = username,

                Email = email,

                Password = password,

                Role = role
            };

            _context.Users.Add(newUser);

            _context.SaveChanges(); // save to database 


             // save farmer 
            if (role == "Farmer")
            {
                var newFarmer = new Farmer
                {
                    Id = newUser.Id, // Ensure the Farmer Id matches the User Id

                    Name = name,

                    Surname = surname,

                    Username = username,

                    Email = email,

                    Password = password
                };
                _context.Farmers.Add(newFarmer);

                _context.SaveChanges(); // save to databse 
            }

            session.SetString("UserId", newUser.Id.ToString());

            session.SetString("Role", newUser.Role);

            return new AuthResult { Success = true };
        }
    }
    //****************************** end of code ****************************//
}

//////////////////////////////////////////////// end of file ///////////////////////////////////////////////