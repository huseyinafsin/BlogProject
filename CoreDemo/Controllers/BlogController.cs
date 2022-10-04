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
using DataAccessLayer.Abstract;
using System.Xml;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Text.RegularExpressions;
using System.Collections;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace CoreDemo.Controllers
{

    public class BlogController : Controller
    {
        private IBlogService _blogService;
        private ICategoryService _categoryService;
        private IWriterService _writerService;
        private readonly ICategoryDal _categoryDal;
        private readonly IHostingEnvironment _hostingEnvironment;

        public BlogController(IBlogService blogService, ICategoryDal categoryDal, ICategoryService categoryService, IWriterService writerService, IHostingEnvironment hostingEnvironment)
        {
            _blogService = blogService;
            _categoryService = categoryService;
            _writerService = writerService;
            _categoryDal = categoryDal;
            _hostingEnvironment = hostingEnvironment;
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
            var values = _blogService.GetBlogListWithCategory();
            List<Blog> truncatedBlogs = new List<Blog>();

            foreach (var value in values)
            {
                var substr = TruncateHTMLSafeishChar(value.BlogContent, 150);
                value.BlogContent = String.Empty;
                value.BlogContent = substr;
                truncatedBlogs.Add(value);
            }

            return View(truncatedBlogs);
        }

        [AllowAnonymous]
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
            List<SelectListItem> categoryalues = (from x in _categoryDal.GetListAll()
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

        private string TruncateHTMLSafeishChar(string text, int charCount)
        {
            bool inTag = false;
            int cntr = 0;
            int cntrContent = 0;

            // loop through html, counting only viewable content
            foreach (Char c in text)
            {
                if (cntrContent == charCount) break;
                cntr++;
                if (c == '<')
                {
                    inTag = true;
                    continue;
                }

                if (c == '>')
                {
                    inTag = false;
                    continue;
                }
                if (!inTag) cntrContent++;
            }

            string substr = text.Substring(0, cntr);

            //search for nonclosed tags        
            MatchCollection openedTags = new Regex("<[^/](.|\n)*?>").Matches(substr);
            MatchCollection closedTags = new Regex("<[/](.|\n)*?>").Matches(substr);

            // create stack          
            Stack<string> opentagsStack = new Stack<string>();
            Stack<string> closedtagsStack = new Stack<string>();

            // to be honest, this seemed like a good idea then I got lost along the way 
            // so logic is probably hanging by a thread!! 
            foreach (Match tag in openedTags)
            {
                string openedtag = tag.Value.Substring(1, tag.Value.Length - 2);
                // strip any attributes, sure we can use regex for this!
                if (openedtag.IndexOf(" ") >= 0)
                {
                    openedtag = openedtag.Substring(0, openedtag.IndexOf(" "));
                }

                // ignore brs as self-closed
                if (openedtag.Trim() != "br")
                {
                    opentagsStack.Push(openedtag);
                }
            }

            foreach (Match tag in closedTags)
            {
                string closedtag = tag.Value.Substring(2, tag.Value.Length - 3);
                closedtagsStack.Push(closedtag);
            }

            if (closedtagsStack.Count < opentagsStack.Count)
            {
                while (opentagsStack.Count > 0)
                {
                    string tagstr = opentagsStack.Pop();

                    if (closedtagsStack.Count == 0 || tagstr != closedtagsStack.Peek())
                    {
                        substr += "</" + tagstr + ">";
                    }
                    else
                    {
                        closedtagsStack.Pop();
                    }
                }
            }

            return substr;
        }

        [HttpPost("UploadFiles")]
        [Produces("application/json")]
        public async Task<IActionResult> Post(List<IFormFile> my_editor)
        {
            // Get the file from the POST request
            var theFile = HttpContext.Request.Form.Files.GetFile("file");

            // Get the server path, wwwroot
            string webRootPath = _hostingEnvironment.WebRootPath;

            // Building the path to the uploads directory
            var fileRoute = Path.Combine(webRootPath, "uploads");

            // Get the mime type
            var mimeType = HttpContext.Request.Form.Files.GetFile("file").ContentType;

            // Get File Extension
            string extension = System.IO.Path.GetExtension(theFile.FileName);

            // Generate Random name.
            string name = Guid.NewGuid().ToString().Substring(0, 8) + extension;

            // Build the full path inclunding the file name
            string link = Path.Combine(fileRoute, name);

            // Create directory if it does not exist.
            FileInfo dir = new FileInfo(fileRoute);
            dir.Directory.Create();

            // Basic validation on mime types and file extension
            string[] fileMimetypes = { "text/plain", "application/msword", "application/x-pdf", "application/pdf", "application/json", "text/html" };
            string[] fileExt = { ".txt", ".pdf", ".doc", ".json", ".html" };

            try
            {
                if (Array.IndexOf(fileMimetypes, mimeType) >= 0 && (Array.IndexOf(fileExt, extension) >= 0))
                {
                    // Copy contents to memory stream.
                    Stream stream;
                    stream = new MemoryStream();
                    theFile.CopyTo(stream);
                    stream.Position = 0;
                    String serverPath = link;

                    // Save the file
                    using (FileStream writerFileStream = System.IO.File.Create(serverPath))
                    {
                        await stream.CopyToAsync(writerFileStream);
                        writerFileStream.Dispose();
                    }

                    // Return the file path as json
                    Hashtable fileUrl = new Hashtable();
                    fileUrl.Add("link", "/uploads/" + name);

                    return Json(fileUrl);
                }
                throw new ArgumentException("The file did not pass the validation");
            }

            catch (ArgumentException ex)
            {
                return Json(ex.Message);
            }
        }
    }
}