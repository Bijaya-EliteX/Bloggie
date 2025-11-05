using Bloggie.web.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace Bloggie.web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;

        public AccountController(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }

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
