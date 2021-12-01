using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Abstract;
using DataAccessLayer.Concrete;
using Microsoft.AspNetCore.Authorization;

namespace CoreDemo.Controllers
{
    public class DashboardController : Controller
    {
        private IWriterService _writerService;

        public DashboardController(IWriterService writerService)
        {
            _writerService = writerService;
        }

        public IActionResult Index()
        {
            var userMail = User.Identity.Name;

            var user = _writerService.GetWriterByMail(userMail).WriterID;

            return View(user);
        }
    }
}
