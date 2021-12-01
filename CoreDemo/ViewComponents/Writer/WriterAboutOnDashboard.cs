using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Abstract;
using DataAccessLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo.ViewComponents.Writer
{


    public class WriterAboutOnDashboard : ViewComponent
    {
        private IWriterService _writerService;

        public WriterAboutOnDashboard(IWriterService writerService)
        {
            _writerService = writerService;
        }

        public IViewComponentResult Invoke()
        {
            var userMail = User.Identity.Name;
            var user = _writerService.GetWriterByMail(userMail);
            return View(user);
        }
    }
}
