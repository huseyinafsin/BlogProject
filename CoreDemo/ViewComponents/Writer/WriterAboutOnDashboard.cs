using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo.ViewComponents.Writer
{


    public class WriterAboutOnDashboard : ViewComponent
    {
        private IWriterService _writerService;

        public WriterAboutOnDashboard(IWriterService writerService)
        {
            _writerService = writerService;
        }

        public IViewComponentResult Invoke()
        {
            var values = _writerService.GetWriterById(1);
            return View(values);
        }
    }
}
