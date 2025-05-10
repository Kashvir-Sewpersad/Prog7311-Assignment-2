using Prog7311_Assignment_2.Models;

namespace Prog7311_Assignment_2.Services
{
    public class FarmerService
    {
        public class FarmerResult
        {
            public bool Success { get; set; }
            public string ErrorMessage { get; set; }
        }

        public List<Farmer> GetAllFarmers()
        {
            return DataStore.Farmers.ToList();
        }

        public FarmerResult AddFarmer(string name, string surname, string username, string email, string password, string confirmPassword)
        {
            if (DataStore.Farmers.Any(f => f.Username == username))
            {
                return new FarmerResult { Success = false, ErrorMessage = "Username already exists" };
            }

            if (password != confirmPassword)
            {
                return new FarmerResult { Success = false, ErrorMessage = "Passwords do not match" };
            }

            var farmer = new Farmer
            {
                Id = DataStore.Farmers.Count + 1,
                Name = name,
                Surname = surname,
                Username = username,
                Email = email,
                Password = password
            };
            DataStore.Farmers.Add(farmer);
            return new FarmerResult { Success = true };
        }
    }
}