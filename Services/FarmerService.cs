using Microsoft.EntityFrameworkCore;
using Prog7311_Assignment_2.Data;
using Prog7311_Assignment_2.Models;

namespace Prog7311_Assignment_2.Services
{
    public class FarmerService
    {
        private readonly DataContext _context;

        public FarmerService(DataContext context)
        {
            _context = context;
        }

        public class FarmerResult
        {
            public bool Success { get; set; }
            public string ErrorMessage { get; set; }
        }

        public List<Farmer> GetAllFarmers()
        {
            return _context.Farmers.ToList();
        }

        public FarmerResult AddFarmer(string name, string surname, string username, string email, string password, string confirmPassword)
        {
            if (_context.Farmers.Any(f => f.Username == username))
            {
                return new FarmerResult { Success = false, ErrorMessage = "Username already exists" };
            }

            if (password != confirmPassword)
            {
                return new FarmerResult { Success = false, ErrorMessage = "Passwords do not match" };
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
}