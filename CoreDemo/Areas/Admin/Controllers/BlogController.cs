using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BusinessLayer.Abstract;
using ClosedXML.Excel;
using CoreDemo.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogController : Controller
    {
        IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public IActionResult ExportStaticExcelBlogList()
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Blog Listesi");
                worksheet.Cell(1, 1).Value = "Blog ID";
                worksheet.Cell(1, 2).Value = "Blog Adı";
                int BlogRowCount = 2;
                var blogs = _blogService.GetList();
                foreach (var item in blogs)
                {
                    worksheet.Cell(BlogRowCount, 1).Value = item.BlogID;
                    worksheet.Cell(BlogRowCount, 2).Value = item.BlogTitle;
                    BlogRowCount++;
                }

                using (var stream = new  MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "Calisma1.xlsx");
                }
            }
        }

        public IActionResult BlogListExcel()
        {
            return View();
        }


        public IActionResult ExportDynamicExcelBlogList()
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Blog Listesi");
                worksheet.Cell(1, 1).Value = "Blog ID";
                worksheet.Cell(1, 2).Value = "Blog Adı";
                int BlogRowCount = 2;
                var blogs = _blogService.GetList();
                foreach (var item in BlogTitleList())
                {
                    worksheet.Cell(BlogRowCount, 1).Value = item.ID;
                    worksheet.Cell(BlogRowCount, 2).Value = item.BlogName;
                    BlogRowCount++;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "Calisma1.xlsx");
                }
            }

        }

        public List<BlogModel> BlogTitleList()
        {
            List<BlogModel> bm = new List<BlogModel>();

            bm = _blogService.GetList().Select(x => new BlogModel
            {
                ID = x.BlogID,
                BlogName = x.BlogTitle
            }).ToList();
            return bm;
        }

        public IActionResult BlogTitleListExcel()
        {
            return View();
        }


    }
}
