using BussinesLayer.Concrete;
using DataAccesLayer.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyAspNetCoreApp.Web.Controllers
{
	[AllowAnonymous]
	public class AboutController : Controller
	{
		AboutManager abm = new AboutManager(new EFAboutRepository());
	
		public IActionResult Index()
		{
			var values = abm.GetList();
			return View(values);
		}
	}

}
