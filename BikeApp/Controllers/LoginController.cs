using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BikeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly RoleManager<IdentityRole> _roleManager;
        public LoginController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ILogger<RegisterModel> _logger, IEmailSender _emailSender, RoleManager<IdentityRole> _roleManager)
        {
            userManager = userManager;
            signInManager = signInManager;
            _logger = _logger;
            _emailSender = emailSender;
            _roleManager = _roleManager;
        }
     
        public class LoginModel
        {
            public string ReturnUrl { get; set; }

            public int MyProperty { get; set; }
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            //public int MyProperty { get; set; }
            [Required]
            [EmailAddress]
            [StringLength(100, ErrorMessage = "The required length of the string is 100")]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }


            [Required]
            [StringLength(100, ErrorMessage = "The required length of the string is 100")]
            [DataType(DataType.Password)]
            [Display(Name = "Confirm Password")]
            [Compare("Password", ErrorMessage = "The confirm password is ")]
            public string CPassword { get; set; }

            public bool IsAdmin { get; set; }
            public void OnGetAsync(string returnUrl = null)
            {
                // ReturnUrl = returnUrl;
                if (!string.IsNullOrEmpty(ErrorMessage))
                {
                    ModelState.AddModelError(string.Empty,ErrorMessage);
                }
            }

            public async Task<IActionResult> OnPostAsync(string returnUrl = null)
            {
                returnUrl = returnUrl ?? returnUrl.Content("~/");
                if (ModelState.IsValid)
                {
                    var user = new ApplicationUser { Username = InputModel.UserName, Email = InputModel.Email };
                    var result = await _userManger.CreateAsync(user, Input.Password);
                    if (result.IsSucceded)
                    {
                        if (!await _roleManager.RoleExistsAsync("Admin"))
                        {
                            await _roleManager.CreateAsync(new IdentityRole("Admin"));
                        }
                        if (!await _roleManager.RoleExistsAsync("Exceutive"))
                        {
                            await _roleManager.CreateAsync(new IdentityRole("Exceutive"));
                        }
                        if (Input.IsAdmin())
                        {
                            await _userManager.AddToRoleAsync(user, "Admin");
                        }
                        else
                        {
                            await _userManager.AddToRoleAsync(user, "Admin");
                        }


                        _logger.LogInformation("user created");
                        var code = await userManager.CreateEmailConfirmationAsync(user);
                        var callbackUrl = Url.Page(
                            "Account/ConfirmEmail",
                            pageHandler = null,
                            values: new { userId = user.Id, code = code },
                            protocol: Request.Scheme
                            );

                        await _emailSender.SendEmailAsync(InputModel.Email,
                            "Confirm your email",
                            $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}");


                    }
                }


            }

        }
    }
}
