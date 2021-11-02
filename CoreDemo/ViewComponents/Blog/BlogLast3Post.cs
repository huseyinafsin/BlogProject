using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo.ViewComponents.BlogLast3Post
{
    public class BlogLast3Post :ViewComponent
    {
        IBlogService _blogService;

        public BlogLast3Post(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public IViewComponentResult Invoke()
        {
            var values = _blogService.GetLasBlogs(3);
            return View(values);
        }
    }
}
