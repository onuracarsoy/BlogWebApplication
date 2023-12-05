using DataAccesLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace MyAspNetCoreApp.Web.Areas.Admin.ViewComponents.Statistic
{
    public class Statistic3 : ViewComponent
    {
        Context c = new Context();

        public IViewComponentResult Invoke()
        {
            int id = 2;

            ViewBag.v1 = c.Admins.Where(x => x.AdminID == id).Select
                (y => y.Name).FirstOrDefault();

            ViewBag.v2 = c.Admins.Where(x => x.AdminID == id).Select
                (y => y.Role).FirstOrDefault();

            ViewBag.v3 = c.Admins.Where(x => x.AdminID == id).Select
              (y => y.ShortDescription).FirstOrDefault();

            return View();
        }
    }
}
