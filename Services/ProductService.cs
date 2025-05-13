

/////////////////////////////////////////////////// Start of file ////////////////////////////////////////////////////

//---------------------------------- Start of Imports -----------------------//

using Prog7311_Assignment_2.Data;

using Prog7311_Assignment_2.Models;

//------------------------------------ End of imports -----------------------//

namespace Prog7311_Assignment_2.Services
{
    //*************************************** start of code ****************************************//


    /// <summary>
    /// Kashvir 
    /// Sewpersad 
    /// st10257503 
    /// </summary>
    public class ProductService
    {
        private readonly DataContext _context; // variable for data context 

        public ProductService(DataContext context)
        {
            _context = context; // giving the context an instance 
        }

        /*
         The below function gets and sets a variable for messages 
         */
        public class ProductResult
        {
            public bool Success { get; set; }
            public string ErrorMessage { get; set; }
        }
        /*
         the below function gets the information stored in the product databse by using the ID unique to each farmer 

         
         */
        public List<Product> GetProductsByFarmer(int farmerId)
        {
            return _context.Products
                .Where(p => p.FarmerId == farmerId) 
                .ToList();
        }
        /*
         the below code is to retive all product data stored in the database 
                
                this will be used to display products on the dashboard 
         */
        public List<Product> GetAllProducts()
        {
            return _context.Products.ToList();
        }
        /*
         the below code is for the adding product process 

        variables are passed in 
         
         */
        public (bool Success, string ErrorMessage) AddProduct(string name, string category, DateTime productionDate, DateTime endDate, int userId)
        {
            var farmer = _context.Farmers.FirstOrDefault(f => f.UserId == userId);
            if (farmer == null)
            {
                return (false, $"Farmer with UserId {userId} not found.");
            }

            if (productionDate >= endDate)
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
                _context.SaveChanges();
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, $"Failed to add product: {ex.Message}");
            }
        }

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

        public List<Product> FilterProducts(string category, DateTime? startDate, DateTime? endDate)
        {
            var products = _context.Products.AsQueryable();

            if (!string.IsNullOrEmpty(category))

                products = products.Where(p => p.Category == category);

            if (startDate.HasValue)

                products = products.Where(p => p.ProductionDate >= startDate.Value);

            if (endDate.HasValue)

                products = products.Where(p => p.EndDate <= endDate.Value);

            return products.ToList();
        }
    }
    //************************************* end of code ****************************************//
}
//////////////////////////////////////////////////// End of file //////////////////////////////////////////////////