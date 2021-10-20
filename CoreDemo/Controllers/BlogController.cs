using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System;
using BusinessLayer.Abstract;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace CoreDemo.Controllers
{
    [AllowAnonymous]
    public class BlogController : Controller
    {
        IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public IActionResult Index()
        {
            var values = _blogService.GetBlogListWithCatgory();
            return View(values);
        }

        public IActionResult BlogReadAll(int id)
        {
            ViewBag.BlogId = id;
            
            var value = _blogService.GetBlog(id);
            if (value is null)
            {
                return NotFound();
            }
            return View(value);
        }
    }
}
