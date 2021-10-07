using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;

namespace CoreDemo.Controllers
{
    public class AboutController : Controller
    {
        private IAboutService _aboutService;

        public AboutController(IAboutService aboutService)
        {
            _aboutService = aboutService;   
        }
        public IActionResult Index()
        { 
            var values = _aboutService.GetList();
            return View(values);
            
        }

        public PartialViewResult SocialMedyaAbout()
        {
            var values = _aboutService.GetList();
           return PartialView();
        }
    }
}
