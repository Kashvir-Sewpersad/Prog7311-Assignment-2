
/////////////////////////////////////////// start of file //////////////////////////////////////////////////////
///
//------------ start of imports -------------------------//

using Prog7311_Assignment_2.Data;

using Prog7311_Assignment_2.Models;

//------------------ end of imports ---------------//

namespace Prog7311_Assignment_2.Services
{

    //********************************* start of code *****************************// 
    public class ProductService
    {
        private readonly DataContext _context;

        public ProductService(DataContext context)
        {
            _context = context;
        }


        // error messages variables 
        public class ProductResult
        {
            public bool Success { get; set; }
            public string ErrorMessage { get; set; }
        }

        public List<Product> GetProductsByFarmer(int farmerId)
        {
            return _context.Products

                .Where(p => p.FarmerId == farmerId)

                .ToList();
        }

        public List<Product> GetAllProducts()
        {
            return _context.Products.ToList();
        }
        /*
         - below code is to display error messages based on invalid credentials 
         
         */
        public (bool Success, string ErrorMessage) AddProduct(string name, string category, DateTime productionDate, DateTime endDate, int userId)
        {
            var farmer = _context.Farmers.FirstOrDefault(f => f.UserId == userId);
            if (farmer == null)
            {
                return (false, $"Farmer with UserId {userId} not found.");
            }

            if (productionDate >= endDate) // checking if the end date preceeds the start date 
            {
                return (false, "Production Date must be before End Date.");
            }

            var product = new Product
            {
                Name = name,
                Category = category,
                ProductionDate = productionDate,
                EndDate = endDate,
                FarmerId = farmer.Id
            };

            try
            {
                _context.Products.Add(product);

                _context.SaveChanges(); // save 

                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, $"Failed to add product: {ex.Message}");
            }
        }

        /*
         below code is not required in the assignment 

        -- below code is not entirely functional either 
         
         */

        public bool EditProduct(int id, string name, string category, DateTime productionDate, DateTime endDate, int userId)
        {
            var farmer = _context.Farmers.FirstOrDefault(f => f.UserId == userId);
            if (farmer == null)
            {
                return false;
            }

            var product = _context.Products
                .FirstOrDefault(p => p.Id == id && p.FarmerId == farmer.Id);
            if (product == null || productionDate >= endDate)
            {
                return false;
            }

            product.Name = name;
            product.Category = category;
            product.ProductionDate = productionDate;
            product.EndDate = endDate;

            _context.SaveChanges();

            return true;
        }
        /*
         below code is not required in the assignment 

        -- below code is not entirely functional either 
         
         */
        public bool DeleteProduct(int id, int userId)
        {
            var farmer = _context.Farmers.FirstOrDefault(f => f.UserId == userId);
            if (farmer == null)
            {
                return false;
            }

            var product = _context.Products
                .FirstOrDefault(p => p.Id == id && p.FarmerId == farmer.Id);
            if (product == null)
            {
                return false;
            }

            _context.Products.Remove(product);
            _context.SaveChanges();
            return true;
        }
        //---------------------------------- filtration systems ------------------------------------------------//


        //  method to handle both category and date range
        public List<Product> FilterProducts(string category, DateTime? startDate, DateTime? endDate)
        {
            var products = _context.Products.AsQueryable();

            // Filter by category 

            if (!string.IsNullOrEmpty(category))
                products = products.Where(p => p.Category == category);

            // Filter by start date 

            if (startDate.HasValue)
                products = products.Where(p => p.ProductionDate >= startDate.Value);

            // Filter by end date 

            if (endDate.HasValue)
                products = products.Where(p => p.EndDate <= endDate.Value);

            // Include the Farmer navigation  for display

            var result = products.ToList();



            // Load farmer data for each product

            foreach (var product in result)
            {
                if (product.Farmer == null && product.FarmerId > 0)
                {
                    product.Farmer = _context.Farmers.FirstOrDefault(f => f.Id == product.FarmerId);
                }
            }

            return result;
        }
    }
    //************************************ end of code *******************************//
}
///////////////////////////////////////////////// end of file ////////////////////////////////////////////////////