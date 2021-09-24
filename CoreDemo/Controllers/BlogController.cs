using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDemo.Controllers
{
    public class BlogController : Controller
    {
        BlogManager blogManager = new BlogManager(new EFBlogRepository());
        public IActionResult Index()
        {
            var values = blogManager.GetBlogListWithCatgory();
            return View(values);
        }

        public IActionResult BlogReadAll(int id)
        {
            var value = blogManager.GetBlog(id);
            return View(value);
        }
    }
}
