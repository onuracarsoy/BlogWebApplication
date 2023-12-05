using BussinesLayer.Concrete;
using BussinesLayer.ValidationRules;
using DataAccesLayer.Concrete;
using DataAccesLayer.EntityFramework;
using DocumentFormat.OpenXml.Office2010.Excel;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyAspNetCoreApp.Web.Controllers
{
   
    public class CommentController : Controller
    {

        CommentManager cm = new CommentManager(new EFCommentRepository());
        Context c = new Context();

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public PartialViewResult PartialAddComment()
        {
            
            return PartialView();
        }

		[HttpPost]
		public PartialViewResult PartialAddComment(Comment p)
		{

			try
			{


				var usermail = User.Identity.Name;
				var writername = c.Writers.Where(x => x.WriterMail == usermail).Select(y => y.WriterName).FirstOrDefault();

				p.CommentDate = DateTime.Now;
				p.CommentStatus = true;
				p.BlogID = 87;
				p.CommentUserName = writername;
			

				cm.CommentAdd(p);
			}
			catch (Exception ex)
			{
				// Hata mesajını yazdırın
				System.Diagnostics.Debug.WriteLine(ex.Message);
			}

			return PartialView();
		}

		[AllowAnonymous]
		public PartialViewResult CommentListByBlog(int id)
        {
            var values = cm.GetList(id);
            return PartialView(values);
        }

    }


}

