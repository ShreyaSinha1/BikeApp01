using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeApp.Models
{
    public class RegisterModel:PageModel
    {
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly RoleManager<IdentityRole> _roleManager;
        public RegisterModel(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager,ILogger<RegisterModel> _logger, IEmailSender _emailSender, RoleManager<IdentityRole> _roleManager)
        {
            userManager = userManager;
            signInManager = signInManager;
            _logger = _logger;
            _emailSender = emailSender;
            _roleManager = _roleManager;
        }
    }
}
