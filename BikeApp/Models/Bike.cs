using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeApp.Models
{
    public class Bike
    {
        public int Id { get; set; }
        public Make Make { get; set; }

        [RegularExpression("^[1-9]$",ErrorMessage="Sleect Make")]
        public int MakeId { get; set; }

        public Model Model { get; set; }

        public int ModelId { get; set; }

        [Required]
        [YearModelAttribute(2000,ErrorMessage="Invalid Year")]
        public int Year { get; set; }

        [Required]
        [ErrorMessage("Enter the Mileage")]
        public int Mileage { get; set; }

        public string Features { get; set; }

        [Required]
        [ErrorMessage("Enter the Seller Name")]

        public string SellerName { get; set; }
        [ErrorMessage("Enter the Seller Email")]

        public string SellerEmail { get; set; }
        [ErrorMessage("Enter the Seller Phone")]

        public string SellerPhone { get; set; }

        public int Price { get; set; }

        public string Currency { get; set; }

        public string ImagePath { get; set; }

    }
}
