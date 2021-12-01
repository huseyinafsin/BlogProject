using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CoreDemo.Controllers
{
    [AllowAnonymous]
    public class MessageController : Controller
    {
        private IMessage2Service _message2Service;

        public MessageController(IMessage2Service message2Service)
        {
            _message2Service = message2Service;
        }

        public IActionResult InBox()
        {
            int id = 2;
            var values = _message2Service.GetInboxListByWriter(id);
            return View(values);
        }

        public IActionResult MessageDetails(int id)
        {
            var value = _message2Service.GetById(id);
          

            return View(value);
        }

    }
}
