using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceStack;
using WebAppCore.ViewModel;

namespace WebAppCore.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<IdentityUser> userManager;
        private SignInManager<IdentityUser> signInManager;

        public AccountController(UserManager<IdentityUser> userManager,
                                    SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
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
            if (model != null && ModelState.IsValid)
            {
                var user = new IdentityUser() { Email = model.Email, UserName = model.Email };
                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: true);
                    return RedirectToAction("index", "home");
                }

                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }           

            return View(model);
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
            if (model != null && ModelState.IsValid)
            {
                //var user = new ClaimsPrincipal();
                //user.Identity = User.Identity;
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                
                if (result.Succeeded)
                {
                    return RedirectToAction("index", "home");
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt");
            }            
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
             await signInManager.SignOutAsync();                
             return RedirectToAction("index", "home");                 
        }
    }
}
