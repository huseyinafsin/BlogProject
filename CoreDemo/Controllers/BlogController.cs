using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System;
using BusinessLayer.Abstract;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.ValidationRules;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CoreDemo.Controllers
{   

    [AllowAnonymous]
    public class BlogController : Controller
    {
        private IBlogService _blogService;
        private ICategoryService _categoryService;
        private IWriterService _writerService;

        public BlogController(IBlogService blogService, ICategoryService categoryService, IWriterService writerService)
        {
            _blogService = blogService;
            _categoryService = categoryService;
            _writerService = writerService;
        }

        public IActionResult Index()
        {
            var values = _blogService.GetBlogListWithCategory();
            return View(values);
        }

        public IActionResult BlogReadAll(int id)
        {
            ViewBag.BlogId = id;
            
            var value = _blogService.GetById(id);
            if (value is null)
            {
                return NotFound();
            }
            return View(value);
        }   
        
        public IActionResult BlogListByWriter()
        {
            var userMail = User.Identity.Name;
            var user = _writerService.GetWriterByMail(userMail);
            var blogs = _blogService.GetBlogListByWriter(user.WriterID);
            return View(blogs);
        }

        [HttpGet]
        public IActionResult BlogAdd()
        {
            List<SelectListItem> categoryalues = (from x in _categoryService.GetList()
                                    select new SelectListItem
                                    {
                                        Text = x.CategoryName,
                                        Value = x.CategoryID.ToString()
                                    }).ToList();
            ViewBag.cv = categoryalues;
            return View();

        } 
        
        [HttpPost]
        public IActionResult BlogAdd(Blog p)
        {
            var userMail = User.Identity.Name;
            var user = _writerService.GetWriterByMail(userMail);

            BlogValidator bv = new BlogValidator();
            ValidationResult results = bv.Validate(p);
            if (results.IsValid)
            {
                p.BlogStatus = true;
                p.WriterID = user.WriterID;
                p.BlogCreateDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                _blogService.TAdd(p);
                return RedirectToAction("BlogListByWriter", "Blog");
            }
            else
            {
                foreach (var error in results.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
            }

            return View();
        }

        public IActionResult DeleteBlog(int id)
        {
            var blogvalue = _blogService.GetById(id); 
            _blogService.TDelete(blogvalue);

            return RedirectToAction("BlogListByWriter", "Blog");
        }


        [HttpGet]
        public IActionResult EditBlog(int id)
        {
            var blogvalue = _blogService.GetById(id);
            List<SelectListItem> categoryalues = (from x in _categoryService.GetList()
                select new SelectListItem
                {
                    Text = x.CategoryName,
                    Value = x.CategoryID.ToString()
                }).ToList();
            ViewBag.cv = categoryalues;

            return View(blogvalue);
        }


        [HttpPost]
        public IActionResult EditBlog(Blog p)
        {
            var userMail = User.Identity.Name;
            var user = _writerService.GetWriterByMail(userMail);


            p.WriterID = user.WriterID;
            p.BlogCreateDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            p.BlogStatus = true;
            _blogService.TUpdate(p);
            return RedirectToAction("BlogListByWriter");
        }
    }
}
