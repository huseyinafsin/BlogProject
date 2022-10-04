using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using BusinessLayer.Abstract;

namespace CoreDemo.Controllers
{
    public class NewsLetterController : Controller
    {
        private readonly INewsletterService _newsletterService;

        public NewsLetterController(INewsletterService newsletterService)
        {
            _newsletterService = newsletterService;
        }

        public PartialViewResult SubscribeMail()
        {
            return PartialView();
        }

        [HttpPost]
        public IActionResult SubscribeMail(NewsLetter p)
        {
            p.MailStatus = true;
            _newsletterService.NewsletterAdd(p);

            return RedirectToAction("BlogReadAll","Blog");
        }
    }
}
