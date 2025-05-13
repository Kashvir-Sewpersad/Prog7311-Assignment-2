
///////////////////////////////////////////////// START OF FILE /////////////////////////////////////////

//------------------------------- START OF IMPORTS -------------------------//
using System.ComponentModel.DataAnnotations;

using System.ComponentModel.DataAnnotations.Schema;

//-------------------------------- END OF IMPORTS --------------------------//
namespace Prog7311_Assignment_2.Models
{

    /*
     the below code is for user database : user variables with id (primary key )

    // the form will not allow any actions to commense should the form not be filled out completely : required 
     */
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string Username { get; set; } // username for login / registerr 

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; } // "Farmer" or "Employee"

        public List<Farmer> Farmers { get; set; } // Navigation property
    }


    /*
     the below code is for farmer database : farmer  variables with id (primary key )

    // the form will not allow any actions to commense should the form not be filled out completely : required 
     */

    public class Farmer
    {
        [Key]
        public int Id { get; set; } // PK

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string Username { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public int UserId { get; set; } // Foreign key to Users table
        [ForeignKey("UserId")]
        public User User { get; set; }  // Navigation property

        // Navigation property for products
        public List<Product> Products { get; set; } = new List<Product>();
    }


    /*
     the below code is for product  database : product  variables with id (primary key )

    // the form will not allow any actions to commense should the form not be filled out completely : required 
     */

    public class Product
    {
        [Key]
        public int Id { get; set; } // pk

        [Required]
        public string Name { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public DateTime ProductionDate { get; set; } // start date 

        [Required]
        public DateTime EndDate { get; set; }

        public int FarmerId { get; set; }

        [ForeignKey("FarmerId")]
        public Farmer Farmer { get; set; }
    }

    //****************************************** end of code ************************************//
}
///////////////////////////////////////////////// end of file //////////////////////////////////////////