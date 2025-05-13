

///////////////////////////////////////////// start of file ////////////////////////////////////////

//------------------- start of imports -----------------//

using Prog7311_Assignment_2.Data;

using Prog7311_Assignment_2.Models;

//-------------------- end of imports -----------------//
namespace Prog7311_Assignment_2.Services
{
    //******************************** start of code ***************************************//
    public class FarmerService
    {
        private readonly DataContext _context;  // database contedxt 

        public FarmerService(DataContext context)
        {
            _context = context;
        }

        public class FarmerResult
        {
            public bool Success { get; set; }
            public string ErrorMessage { get; set; }
        }
        /*
         retriving all information stored as a farmer 
         */
        public List<Farmer> GetAllFarmers()
        {
            return _context.Farmers.ToList();
        }


        //**
        //below code contains logic ofr adding a new farmer 
       
        //variables to be stored in database 

        //**/
        public FarmerResult AddFarmer(string name, string surname, string username, string email, string password, string confirmPassword)
        {
            if (_context.Farmers.Any(f => f.Username == username))
            {
                return new FarmerResult { Success = false, ErrorMessage = "Username already exists" }; // error is user is currentlyy in database 
            }

            if (password != confirmPassword)
            {
                return new FarmerResult { Success = false, ErrorMessage = "Passwords do not match" }; // error is passwords dont line up
            }

            var user = new User
            {
                Name = name,

                Surname = surname,

                Username = username,

                Email = email,

                Password = password,

                Role = "Farmer"
            };

            _context.Users.Add(user);

            _context.SaveChanges(); // Save the User first to get the auto-generated Id

            var farmer = new Farmer
            {
                Name = name,

                Surname = surname,

                Username = username,

                Email = email,

                Password = password,

                UserId = user.Id // Link the Farmer to the User via UserId
            };

            _context.Farmers.Add(farmer);

            _context.SaveChanges();

            return new FarmerResult { Success = true };
        }
    }
    //******************************** end of code ***************///
}

///////////////////////////////////////////////end of file ///////////////////////////////////////