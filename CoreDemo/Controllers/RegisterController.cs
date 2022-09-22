using BusinessLayer.Abstract;
using BusinessLayer.ValidationRules;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDemo.Controllers
{
    [AllowAnonymous]
    public class RegisterController : Controller
    {
        IWriterService _writerService;
        private readonly UserManager<AppUser> _userManager;


        public RegisterController(IWriterService writerService, UserManager<AppUser> userManager)
        {
            _writerService = writerService;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }  
        
        [HttpPost]
        public async Task<IActionResult> Index(Writer model)
        {
            WriterValidator wv = new WriterValidator();
            ValidationResult results = wv.Validate(model);
            if (results.IsValid)
            {
                model.WriterAbout = "Deneme Test";
                model.WriterStatus = true;

                var user = new AppUser()
                {
                    Email = model.WriterMail,
                    UserName = model.WriterMail,
                    NameSurname = model.WriterName,
                };

                var result = await _userManager.CreateAsync(user, model.WriterPassword);

                if (result.Succeeded)
                {
                    
                    _writerService.TAdd(model);
                    RedirectToAction("Index", "Login");

                }
            }
            else
            {
                foreach (var error in results.Errors)
                {
                    ModelState.AddModelError(error.PropertyName,error.ErrorMessage);
                }
            }

            return View();
        }
    }
}
