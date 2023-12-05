using BussinesLayer.Concrete;
using BussinesLayer.ValidationRules;
using DataAccesLayer.Abstract;
using DataAccesLayer.Concrete;
using DataAccesLayer.EntityFramework;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Office2010.Excel;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyAspNetCoreApp.Web.Models;
using X.PagedList;



namespace MyAspNetCoreApp.Web.Controllers
{

    public class BlogController : Controller
    {
        BlogManager bm = new BlogManager(new EFBlogRepository());
        CategoryManager cm = new CategoryManager(new EFCategoryRepository());
        Context c = new Context();
        [AllowAnonymous]
        public IActionResult Index(int page = 1)
        {
            var values = bm.GetBlogListWithCategory().ToPagedList(page, 3);
            return View(values);
        }
        [AllowAnonymous]
        public IActionResult BlogReadAll(int id)
        {
            ViewBag.i = id;
            var values = bm.GetByID(id);
            return View(values);
        }

        public IActionResult BlogListByWriter(int page = 1)
        {
            var usermail = User.Identity.Name;

            var writerID = c.Writers.Where(x => x.WriterMail == usermail)
                 .Select(y => y.WriterID).FirstOrDefault();
            var values = bm.GetListWithCategoryByWriterBm(writerID).ToPagedList(page, 7);
            return View(values);
        }

        [HttpGet]
        public IActionResult BlogAdd()
        {

            List<SelectListItem> categoryvalues = (from x in cm.GetList()
                                                   select new SelectListItem
                                                   {
                                                       Text = x.CategoryName,
                                                       Value = x.CategoryID.ToString()
                                                   }).ToList();
            ViewBag.cv = categoryvalues;
            return View();
        }

        [HttpPost]
        public IActionResult BlogAdd(Blog p ,AddBlogImage i)
        {


            Blog b = new Blog();

            if (i.BlogImage != null)
            {
                var extension = Path.GetExtension(i.BlogImage.FileName);
                var newimagename = Guid.NewGuid() + extension;
                var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/BlogImageFiles/", newimagename);
                var stream = new FileStream(location, FileMode.Create);
                i.BlogImage.CopyTo(stream);
                b.BlogImage = newimagename;
            }
            else if (i.BlogThumbnailImage != null)
            {
                var extension = Path.GetExtension(i.BlogThumbnailImage.FileName);
                var newimagename = Guid.NewGuid() + extension;
                var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/BlogImageFiles/", newimagename);
                var stream = new FileStream(location, FileMode.Create);
                i.BlogThumbnailImage.CopyTo(stream);
                b.BlogThumbnailImage = newimagename;
            }

            var usermail = User.Identity.Name;

            var writerID = c.Writers.Where(x => x.WriterMail == usermail)
                 .Select(y => y.WriterID).FirstOrDefault();

            BlogValidator bv = new BlogValidator();
            ValidationResult results = bv.Validate(p);
            List<SelectListItem> categoryvalues = (from x in cm.GetList()
                                                   select new SelectListItem
                                                   {
                                                       Text = x.CategoryName,
                                                       Value = x.CategoryID.ToString()
                                                   }).ToList();
            ViewBag.cv = categoryvalues;
            if (results.IsValid)
            {
                b.BlogTitle = i.BlogTitle;
                b.BlogContent = i.BlogContent;
                b.BlogStatus = true;
                b.CreateDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                b.WriterID = writerID;


                bm.TAdd(b);


                return RedirectToAction("BlogListByWriter", "Blog");



            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
                }

            //        var usermail = User.Identity.Name;

            //        var writerID = c.Writers.Where(x => x.WriterMail == usermail)
            //             .Select(y => y.WriterID).FirstOrDefault();

            //        BlogValidator bv = new BlogValidator();
            //        ValidationResult results = bv.Validate(p);
            //        List<SelectListItem> categoryvalues = (from x in cm.GetList()
            //                                               select new SelectListItem
            //                                               {
            //                                                   Text = x.CategoryName,
            //                                                   Value = x.CategoryID.ToString()
            //                                               }).ToList();
            //        ViewBag.cv = categoryvalues;
            //        if (results.IsValid)
            //        {

            //if (i.BlogImage != null && i.BlogThumbnailImage != null)
            //{
            //	// Kullanıcı tarafından sağlanan dosya yolu bilgilerini işleyin
            //	string blogImagePath = SaveFile(i.BlogImage);
            //	string thumbnailImagePath = SaveFile(i.BlogThumbnailImage);

            //	p.BlogImage = blogImagePath;
            //	p.BlogThumbnailImage = thumbnailImagePath;
            //}


            //p.BlogStatus = true;
            //p.CreateDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            //p.WriterID = writerID;


            //bm.TAdd(p);


            //            return RedirectToAction("BlogListByWriter", "Blog");



            //        }
            //        else
            //        {
            //            foreach (var item in results.Errors)
            //            {
            //                ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
            //            }
            //        }
            //        return View();
            //    }
            //    private string SaveFile(IFormFile file)
            //    {
            //        if (file != null && file.Length > 0)
            //        {
            //            string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            //            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "BlogImageFiles", uniqueFileName);

            //            using (var stream = new FileStream(filePath, FileMode.Create))
            //            {
            //                file.CopyTo(stream);
            //            }

            //            return uniqueFileName;
            //        }

            //        return null;
            //    }



            public IActionResult DeleteBlog(int id)
        {
            var blogvalue = bm.TGetById(id);
            bm.TDelete(blogvalue);
            return RedirectToAction("BlogListByWriter", "Blog");
        }
        [HttpGet]
        public IActionResult EditBlog(int id)

        {

            List<SelectListItem> categoryvalues = (from x in cm.GetList()
                                                   select new SelectListItem
                                                   {
                                                       Text = x.CategoryName,
                                                       Value = x.CategoryID.ToString()
                                                   }).ToList();
            ViewBag.cv = categoryvalues;
            var blogvalue = bm.TGetById(id);
            return View(blogvalue);
        }
        [HttpPost]
        public IActionResult EditBlog(Blog p,AddBlogImage i)
        {
            var usermail = User.Identity.Name;

            var writerID = c.Writers.Where(x => x.WriterMail == usermail)
                 .Select(y => y.WriterID).FirstOrDefault();
            var blogvalue = bm.TGetById(p.BlogID);

            Blog b = new Blog();

            if (i.BlogImage != null)
            {
                var extension = Path.GetExtension(i.BlogImage.FileName);
                var newImageName = Guid.NewGuid().ToString() + extension;
                var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/BlogImageFiles/", newImageName);
                using (var stream = new FileStream(location, FileMode.Create))
                {
                    i.BlogImage.CopyTo(stream);
                }
                b.BlogImage = newImageName;



            }
            else if (i.BlogThumbnailImage != null)
            {
                var extension = Path.GetExtension(i.BlogThumbnailImage.FileName);
                var newImageName = Guid.NewGuid().ToString() + extension;
                var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/BlogImageFiles/", newImageName);
                using (var stream = new FileStream(location, FileMode.Create))
                {
                    i.BlogThumbnailImage.CopyTo(stream);
                }
                b.BlogThumbnailImage = newImageName;

            }
           

            p.BlogStatus = true;
            p.WriterID = writerID;
            DateTime originalCreateDate = p.CreateDate; // var olan CreateDate değerini al
            p.CreateDate = DateTime.Parse(DateTime.Now.ToShortDateString()); // yeni CreateDate değerini atama
            p.CreateDate = originalCreateDate; // güncellenmiş nesnenin CreateDate değerini eski değerle değiştir
            bm.TUpdate(p);
       
            return RedirectToAction("BlogListByWriter");
        }

        [AllowAnonymous]
        public IActionResult CategoryList()
        {


            var values = cm.GetList();

            return View(values);
        }

        [AllowAnonymous]
        public IActionResult BlogListByCategory(int id, int page = 1)
        {
            ViewBag.i = id;
            var blogs = c.Blogs.Where(b => b.CategoryID == id).ToList().ToPagedList(page, 3);
            return View(blogs);

        }
    }
}
