using BusinessLayer.Abstract;
using DocumentFormat.OpenXml.Bibliography;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CoreDemo.Controllers
{
    [AllowAnonymous]
    public class MessageController : Controller
    {
        private IMessage2Service _message2Service;
        private IWriterService _writerService;

        public MessageController(IMessage2Service message2Service, IWriterService writerService)
        {
            _message2Service = message2Service;
            _writerService = writerService;
        }

        public IActionResult InBox()
        {
            var writerMail= HttpContext.User.Identity.Name;
            int writerId = _writerService.GetWriterByMail(writerMail).WriterID;
            var values = _message2Service.GetInboxListByWriter(writerId);
            return View(values);
        }

        public IActionResult MessageDetails(int id)
        {
            var value = _message2Service.GetById(id);
          

            return View(value);
        }

    }
}
