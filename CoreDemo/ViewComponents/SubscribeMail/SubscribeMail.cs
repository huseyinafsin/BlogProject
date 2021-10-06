using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace CoreDemo.ViewComponents.SubscribeMail
{
    public class SubscribeMail : ViewComponent
    {
        private readonly INewsletterDal _newsletterDal;

        public SubscribeMail(INewsletterDal newsletterDal)
        {
            _newsletterDal = newsletterDal;
        }

        [HttpPost]
        public IViewComponentResult Invoke()
        {

            NewsLetter nl = new NewsLetter();
            nl.Mail = (string) RouteData.Values["Email"];
            nl.MailStatus = true;
            _newsletterDal.Insert(nl);

            return View("Default");
        }
    }
}   
