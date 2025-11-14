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

            
            var identityResult = await userManager.CreateAsync(identityUser, registerViewModel.Password);

            if (identityResult.Succeeded)
            {
                var roleIdentityResult = await userManager.AddToRoleAsync(identityUser, "User");
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
        // Handles GET requests for the login page
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            // Use SignInManager to attempt to sign the user in with the provided credentials
            var signInResult = await signInManager.PasswordSignInAsync(loginViewModel.Username,
                loginViewModel.Password, false, false);

            if (signInResult != null && signInResult.Succeeded)
            {
                // If login is successful, redirect the user to the homepage
                return RedirectToAction("Index", "Home");
            }

            // If login fails, return the user to the login view so they can try again.
            // You can add error handling here later.
            return View();
        }


    }
    
}
