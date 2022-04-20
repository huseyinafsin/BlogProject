using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo.ViewComponents.Writer
{
    public class WriterMessageNotification : ViewComponent
    {
        private IMessage2Service _message2Service;
        private IWriterService _writerService;

        public WriterMessageNotification(IMessage2Service message2Service, IWriterService writerService)
        {
            _message2Service = message2Service;
            _writerService = writerService;
        }

        public IViewComponentResult Invoke()
        {
            var writerMail = HttpContext.User.Identity.Name;
            int id = _writerService.GetWriterByMail(writerMail).WriterID;
            var values = _message2Service.GetInboxListByWriter(id);
            return View(values);
        }
    }
}
