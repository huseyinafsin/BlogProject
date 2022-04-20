using System.Threading.Tasks;
using CoreDemo.Models;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo.Controllers
{
    [AllowAnonymous]
    public class RegisterUserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        public RegisterUserController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        [HttpGet]
        public IActionResult SingUp()
        {
            return View();
        }   
        [HttpPost]
        public async Task<IActionResult> SingUp(UserSignUpViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser()
                {
                    Email = model.Email,
                    UserName = model.Username,
                    NameSurname = model.NameSurname
                };

                var result = await _userManager.CreateAsync(user,model.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Login");

                }
                else
                {
                    foreach (var identityError in result.Errors)
                    {
                        ModelState.AddModelError("",identityError.Description);
                    }

                }
            }
            return View(model);
        }
    }
}
