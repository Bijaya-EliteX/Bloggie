using Bloggie.web.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;       

namespace Bloggie.web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        // Handles GET requests for the login page
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel) { 
            // Parameters: Username, Password, IsPersistent (remember me?), LockoutOnFailure
            var signInResult = await signInManager.PasswordSignInAsync(loginViewModel.Username,
                loginViewModel.Password, false, false);

            if (signInResult != null && signInResult.Succeeded)
            {
                // If login is successful, redirect to the home page.
                return RedirectToAction("Index", "Home");
            }

            // If login fails, show the form again.
            // You can also add an error message to display to the user.
            // Example: ModelState.AddModelError("", "Invalid username or password");
            return View();
        }

        // Handles GET requests for the Register page
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            //create a new IdentityUser object
            var identityUser = new IdentityUser
            {
                UserName = registerViewModel.Username,
                Email = registerViewModel.Email
            };

              //  Create the user in the Identity store
              var identityResult = await userManager.AddToRoleAsync(identityUser, "User");

            if (identityResult.Succeeded)
            {
                //lRedirect to the GET register Action to clear the form
                return RedirectToAction("Register");
            }
        //Extra: In a real app, you'd add identityResult.Errors to ModelState for display.
               foreach (var error in identityResult.Errors)
               {
                   ModelState.AddModelError("", error.Description);
               }
            return View();
        }


    }
    
}
