using System.Linq;
using System.Xml.Linq;
using BusinessLayer.Abstract;
using DataAccessLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo.Areas.Admin.ViewComponents.Statistic
{
    public class Statistic1 : ViewComponent
    {
        private IBlogService _blogService;
        private Context context;
        public Statistic1(IBlogService blogService, Context context)
        {
            _blogService = blogService;
            this.context = context;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.v1 = _blogService.GetList().Count;
            ViewBag.v2 = context.Contacts.Count();
            ViewBag.v3 = context.Comments.Count();

            string key = "70dc4a15a48061402e10ee2262076039";
            string city = "Istanbul";
            string connection = "https://api.openweathermap.org/data/2.5/weather?q="+city+"&lang=tr&units=metric&mode=xml&appid="+key;

            XDocument document = XDocument.Load(connection);

            ViewBag.v4 = document.Descendants("temperature").ElementAt(0).Attribute("value").Value;

            return View();
        }

    }
}
