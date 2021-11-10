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
        private IMessageService _messageService;

        public WriterMessageNotification(IMessageService messageService)
        {
            _messageService = messageService;
        }

        public IViewComponentResult Invoke()
        {
            string p = "murat@mail.com";
            var values = _messageService.GetInboxListByWriter(p);
            return View(values);
        }
    }
}
