using BussinesLayer.Concrete;
using DataAccesLayer.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace MyAspNetCoreApp.Web.Areas.Admin.Controllers
{
	[Area("Admin")]
	[AllowAnonymous]
	public class AdminCommentController : Controller
	{
		CommentManager commentManager = new CommentManager(new EFCommentRepository());
		public IActionResult Index(int page=1)
		{ var values = commentManager.GetCommentWithBlog().ToPagedList(page, 7);
			return View(values);
		}
	}
}
