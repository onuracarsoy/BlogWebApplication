using BussinesLayer.Concrete;
using DataAccesLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace MyAspNetCoreApp.Web.ViewComponents.Blog
{
	public class BlogLast3Post : ViewComponent
	{
		BlogManager bm = new BlogManager(new EFBlogRepository());

		public IViewComponentResult Invoke()
		{
			var values = bm.GetLast3Blog();
			return View(values);
		}
	}
}

