﻿using System.Collections.Generic;
using CoreDemo.Areas.Admin.Models;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ChartController : Controller
    {
        public IActionResult Index()
        {
            return View ();
        }

        public IActionResult CategoryChart()
        {
            List<CategoryClass> list = new List<CategoryClass>();

            list.Add(new CategoryClass { categoryname = "Yazılım", categorycount = 10 });
            list.Add(new CategoryClass { categoryname = "Teknoloji", categorycount = 14 });
            list.Add(new CategoryClass { categoryname = "Spor", categorycount = 5 });
            list.Add(new CategoryClass { categoryname = "Sinema", categorycount = 3 });

            return Json(new {jsonlist = list});
        }
    }
}
