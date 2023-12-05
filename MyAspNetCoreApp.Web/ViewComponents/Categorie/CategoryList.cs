

using BussinesLayer.Concrete;
using DataAccesLayer.Concrete;
using DataAccesLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace MyAspNetCoreApp.Web.ViewComponents.Categorie
{
	public class CategoryList : ViewComponent
	{
		CategoryManager cm = new CategoryManager(new EFCategoryRepository());

		public IViewComponentResult Invoke()
		{
			Context c = new Context();
		
			var values = c.Blogs
				  .GroupBy(post => post.Category)
				  .OrderByDescending(group => group.Count())
				  .Take(3)
				  .Select(group => group.Key)
		.ToList();

		

			return View(values);


		}
	}
}