using BussinesLayer.Concrete;
using DataAccesLayer.Concrete;
using DataAccesLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace MyAspNetCoreApp.Web.Areas.Admin.ViewComponents.Statistic
{
    public class Statistic1 : ViewComponent
    {
        BlogManager bm = new BlogManager(new EFBlogRepository());
        Context c = new Context();
        public IViewComponentResult Invoke()
        {
            ViewBag.v1 = bm.GetList().Count();
            ViewBag.v2 = c.Contacts.Count();
            ViewBag.v3 = c.Writers.Count();

            string api = "1fcc51f53058c77b62149afeae6695fa";
            string connection = "https://api.openweathermap.org/data/2.5/weather?q=istanbul&units=metric&mode=xml&appid=" + api;
            XDocument document = XDocument.Load(connection); 
            ViewBag.v4 = document.Descendants("temperature").ElementAt(0).Attribute("value").Value;   
            ViewBag.v5 = document.Descendants("city").ElementAt(0).Attribute("name").Value;   
            return View();
        }
    }
}
