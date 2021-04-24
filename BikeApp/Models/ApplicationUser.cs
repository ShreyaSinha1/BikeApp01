using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BikeApp.Models
{
    public class ApplicationUser:IdentityUser
    {
        [DisplayName("Office Name")]
        public string PhoneNumber { get; set; }

        [NotMapped]
        public bool IsAdmin { get; set; }

    }
}
