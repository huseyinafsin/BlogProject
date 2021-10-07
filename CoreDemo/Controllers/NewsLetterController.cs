using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;

namespace CoreDemo.Controllers
{
    public class NewsLetterController : Controller
    {
        NewsletterManager nm  = new NewsletterManager(new EfNewsletterRepository());


        public PartialViewResult SubscribeMail()
        {
            return PartialView();
        }

        [HttpPost]
        public IActionResult SubscribeMail(NewsLetter p)
        {
            p.MailStatus = true;
            nm.NewsletterAdd(p);

            return RedirectToAction("BlogReadAll","Blog");
        }
    }
}
