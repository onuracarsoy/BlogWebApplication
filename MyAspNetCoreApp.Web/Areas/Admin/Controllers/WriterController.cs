using BussinesLayer.Concrete;
using BussinesLayer.ValidationRules;
using DataAccesLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyAspNetCoreApp.Web.Models;
using X.PagedList;
namespace MyAspNetCoreApp.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AllowAnonymous]
    public class WriterController : Controller
    {
        WriterManager wm = new WriterManager(new EFWriterRepository());

  

        public IActionResult Index(int page = 1)
        {
            var values = wm.GetList().ToPagedList(page, 7);
            return View(values);
        }


       
        [HttpGet]
        public IActionResult AddWriter()

        {
            return View();
        }
     
        [HttpPost]
        public IActionResult AddWriter(Writer v,AddProfileImage p)
        {
            WriterValidator wv = new WriterValidator();
            ValidationResult results = wv.Validate(v);
            if (results.IsValid)
            {


                if (p.WriterPassword != p.WriterConfirmPassword)
                {
                    ModelState.AddModelError("WriterConfirmPassword", "Şifreler uyuşmuyor.");
                    return View();
                }
                else
                {
                    Writer w = new Writer();
                    if (p.WriterImage != null)
                    {
                        var extension = Path.GetExtension(p.WriterImage.FileName);
                        var newimagename = Guid.NewGuid() + extension;
                        var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/WriterImageFiles/", newimagename);
                        var stream = new FileStream(location, FileMode.Create);
                        p.WriterImage.CopyTo(stream);
                        w.WriterImage = newimagename;


                    }

                    w.WriterMail = p.WriterMail;
                    w.WriterName = p.WriterName;
                    w.WriterStatus = true;
                    w.WriterAbout = p.WriterAbout;
                    w.WriterPassword = p.WriterPassword;
                    w.WriterConfirmPassword = p.WriterConfirmPassword;

                    wm.TAdd(w);
                    return RedirectToAction("Index", "Writer");

                }
            



            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();


            //Writer w = new Writer();
            //if (p.WriterImage != null)
            //{
            //    var extension = Path.GetExtension(p.WriterImage.FileName);
            //    var newimagename = Guid.NewGuid() + extension;
            //    var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/WriterImageFiles/", newimagename);
            //    var stream = new FileStream(location, FileMode.Create);
            //    p.WriterImage.CopyTo(stream);
            //    w.WriterImage = newimagename;


            //}

            //w.WriterMail = p.WriterMail;
            //w.WriterName = p.WriterName;
            //w.WriterStatus = true;
            //w.WriterAbout = p.WriterAbout;
            //w.WriterPassword = p.WriterPassword;
            //w.WriterConfirmPassword = p.WriterConfirmPassword;

            //wm.TAdd(w);
            //return RedirectToAction("Index", "Writer");

        }

        public IActionResult WriterDelete(int id)
        {
            var value = wm.TGetById(id);
            wm.TDelete(value);
            return RedirectToAction("Index");
        }
        public IActionResult WriterStatusF(int id)
        {
            var writer = wm.TGetById(id);
            writer.WriterStatus = false;
            wm.TUpdate(writer);
            return RedirectToAction("Index");
        }
        public IActionResult WriterStatusT(int id)
        {
            var writer = wm.TGetById(id);
            writer.WriterStatus = true;
            wm.TUpdate(writer);
            return RedirectToAction("Index");
        }


    }
    }

