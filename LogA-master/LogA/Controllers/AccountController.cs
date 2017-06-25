using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using LogA.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using LogA.Models;
using Microsoft.AspNetCore.Identity;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LogA.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> userManager;
        private SignInManager<ApplicationUser> singInManager;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> singInManager)
        {
            this.userManager = userManager;
            this.singInManager = singInManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await singInManager.PasswordSignInAsync(model.Login, model.Password, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid Login");
                }
            }
            return View();
        }
        [Authorize]
        public string Check()
        {
            return "You are logging in";
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName =model.Email ,
                    Email =model.Email,
                    Profile=new Models.Profile.ProfileModel()
                };
                IdentityResult result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await singInManager.SignInAsync(user, false);
                    return RedirectToAction( "Edit","Profile");  
                }
                foreach(var e in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, e.Description);
                }
            }
            return View();
        }
        [HttpGet]
        [Authorize]
        
        public async Task<IActionResult> Logout()
        {
            await singInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
    }
}
