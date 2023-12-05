using DataAccesLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace MyAspNetCoreApp.Web.Areas.Admin.ViewComponents.Statistic
{
    public class Statistic2 : ViewComponent
    {
        Context c = new Context();

        public IViewComponentResult Invoke()
        {
            //kategori
             ViewBag.v1 = c.Blogs
    .GroupBy(b => b.CategoryID)
    .Select(g => new
    {
        CategoryID = g.Key,
        BlogCount = g.Count()
    })
    .Join(c.Categorys,
        b => b.CategoryID,
        c => c.CategoryID,
        (b, c) => new
        {
            CategoryName = c.CategoryName,
            BlogCount = b.BlogCount
        })
    .OrderByDescending(bc => bc.BlogCount)
    .FirstOrDefault()?.CategoryName;

            //Yorum
            ViewBag.v2 = c.Blogs
    .OrderByDescending(b => b.Comments.Count())
    .FirstOrDefault()?.BlogTitle;

            //Yazar
           ViewBag.v3 = c.Writers
       .OrderByDescending(w => w.Blogs.Count())
       .Select(w => w.WriterName)
       .FirstOrDefault();



            return View();
        }
    }
}
