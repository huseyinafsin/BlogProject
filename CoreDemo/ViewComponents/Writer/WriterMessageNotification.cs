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

        public WriterMessageNotification(IMessage2Service message2Service)
        {
            _message2Service = message2Service;
        }

        public IViewComponentResult Invoke()
        {
            int id = 2;
            var values = _message2Service.GetInboxListByWriter(id);
            return View(values);
        }
    }
}
