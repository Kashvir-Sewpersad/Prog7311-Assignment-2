using Microsoft.AspNetCore.Http;
using Prog7311_Assignment_2.Models;
using System.Linq;

namespace Prog7311_Assignment_2.Services
{
    public class AuthService
    {
        public class AuthResult
        {
            public bool Success { get; set; }
            public string ErrorMessage { get; set; }
        }

        public AuthResult Login(string username, string password, string role, ISession session)
        {
            var user = DataStore.Users.FirstOrDefault(u => u.Username == username && u.Password == password && u.Role == role);
            if (user != null)
            {
                session.SetString("UserId", user.Id.ToString());
                session.SetString("Role", user.Role);
                return new AuthResult { Success = true };
            }
            return new AuthResult { Success = false, ErrorMessage = "Invalid credentials" };
        }

        public AuthResult Register(string name, string surname, string username, string email, string password, string confirmPassword, string role, ISession session)
        {
            if (DataStore.Users.Any(u => u.Username == username))
            {
                return new AuthResult { Success = false, ErrorMessage = "Username already exists" };
            }

            if (password != confirmPassword)
            {
                return new AuthResult { Success = false, ErrorMessage = "Passwords do not match" };
            }

            var newUser = new User
            {
                Id = DataStore.Users.Count + 1,
                Name = name,
                Surname = surname,
                Username = username,
                Email = email,
                Password = password,
                Role = role
            };
            DataStore.Users.Add(newUser);
            session.SetString("UserId", newUser.Id.ToString());
            session.SetString("Role", newUser.Role);
            return new AuthResult { Success = true };
        }
    }
}