using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDemo.Controllers
{
    public class CommentController : Controller
    {
        ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        public IActionResult Index()
        {
            return View();
        }
        
        public PartialViewResult PartialAddComment()
        {

            return PartialView();
        } 
        
        public PartialViewResult CommentListByBlog(int id)
        {
            var values = _commentService.GetCommentsByBlog(id);

            return PartialView(values);
        }
    }
}
