using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyAspNetCoreApp.Web.Controllers
{
	[AllowAnonymous]
	public class ErrorPageController : Controller
	{
		public IActionResult Error1(int code)
		{



			return View();
		}
	}
}
