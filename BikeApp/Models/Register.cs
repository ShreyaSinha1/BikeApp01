using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BikeApp.Models
{
    public class Register
    {

    }
    public class InputModel
    {
        public string ReturnUrl { get; set; }
        
        public int MyProperty { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name="Email")]
        public string Email { get; set; }

        //public int MyProperty { get; set; }
        [Required]
        [EmailAddress]
        [StringLength(100,ErrorMessage ="The required length of the string is 100")]
        [DataType(DataType.Password)]
        [Display(Name="Password")]
        public string Password { get; set; }

       
        [Required]
        [StringLength(100, ErrorMessage = "The required length of the string is 100")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password",ErrorMessage ="The confirm password is ")]
        public string CPassword { get; set; }

       public bool IsAdmin { get; set; }
        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }
    }

  
}

