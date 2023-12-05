using Microsoft.AspNetCore.Mvc;

namespace MyAspNetCoreApp.Web.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
