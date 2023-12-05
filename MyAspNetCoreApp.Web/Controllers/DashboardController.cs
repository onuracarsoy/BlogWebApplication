using BussinesLayer.Concrete;
using DataAccesLayer.Concrete;
using DataAccesLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyAspNetCoreApp.Web.Controllers
{
   
    public class DashboardController : Controller
    {
     

        WriterManager wm = new WriterManager(new EFWriterRepository());
        Context c = new Context();
       
		public IActionResult Index(Writer p)
		{
         
            Context c = new Context();
            var usermail = User.Identity.Name;

            var writerID = c.Writers.Where(x => x.WriterMail == usermail)
                 .Select(y => y.WriterID).FirstOrDefault();

            int writerId = writerID;
           
         //Yorum sayısı
            int commentCount = c.Blogs
                                  .Where(blog => blog.WriterID == writerId)
                                  .SelectMany(blog => blog.Comments)
                                  .Count();
            ViewBag.v1 = commentCount.ToString();

            //Blog sayısı
            int blogCount = c.Blogs
                                  .Where(blog => blog.WriterID == writerId)
                                  .Count();
            ViewBag.v2 = blogCount.ToString();

            //son 1 ay blog sayısı
            DateTime oneMonthAgo = DateTime.Now.AddMonths(-1);

            int blogMCount = c.Blogs
                       .Where(blog => blog.WriterID == writerId && blog.CreateDate >= oneMonthAgo)
                       .Count();
            ViewBag.v3 = blogMCount.ToString();
            return View();
		}
	}
}
