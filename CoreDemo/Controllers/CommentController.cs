using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;

namespace CoreDemo.Controllers
{
    [AllowAnonymous]
    public class CommentController : Controller
    {
        ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        
        [HttpGet]
        public PartialViewResult PartialAddComment()
        {

            return PartialView();
        } 
        [HttpPost]
        public PartialViewResult PartialAddComment(Comment comment )
        {
            comment.CommentDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            comment.CommentStatus = true;
            comment.BlogID = 2;
            _commentService.CommentAdd(comment);

            return PartialView(null);
        } 
        
        public PartialViewResult CommentListByBlog(int id)
        {
            var values = _commentService.GetCommentsByBlog(id);

            return PartialView(values);
        }
    }
}
