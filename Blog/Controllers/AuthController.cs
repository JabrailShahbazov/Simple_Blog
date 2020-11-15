using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    public class AuthController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public AuthController(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new LogInViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> Login(LogInViewModel model)
        {
           var result = await _signInManager
               .PasswordSignInAsync(model.UserName, model.Password, false, false);
            return RedirectToAction("Index","Panel");
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
          await _signInManager.SignOutAsync();
          return RedirectToAction("Index","Home");

        }
    }
}
