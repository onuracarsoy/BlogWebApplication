using BussinesLayer.Concrete;
using BussinesLayer.ValidationRules;
using DataAccesLayer.Abstract;
using DataAccesLayer.Concrete;
using DataAccesLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.IdentityModel.Logging;
using MyAspNetCoreApp.Web.Models;

namespace MyAspNetCoreApp.Web.Controllers
{
    public class WriterController : Controller
    {
        WriterManager wm = new WriterManager(new EFWriterRepository());
        Context c = new Context();

        [Authorize]
        public IActionResult Index()
        {
            var usermail = User.Identity.Name;
            ViewBag.v = usermail;
            return View();
        }

        public PartialViewResult WriterNavbarPartial()
        {
            return PartialView();
        }

        public PartialViewResult WriterFooterPartial()
        {
            return PartialView();
        }



        [HttpGet]
        public IActionResult WriterEditProfile()
        {
            var usermail = User.Identity.Name;

            var writerID = c.Writers.Where(x => x.WriterMail == usermail)
                 .Select(y => y.WriterID).FirstOrDefault();
            var writervalues = wm.TGetById(writerID);
            return View(writervalues);

        }


        [HttpPost]
        public IActionResult WriterEditProfile(Writer v, AddProfileImage p)
        {

            WriterValidator wl = new WriterValidator();
            ValidationResult results = wl.Validate(v);

            var writer = wm.TGetById(v.WriterID);
            string passwordFromDb = writer.WriterPassword;
          

            if (results.IsValid)
            {
                if (p.WriterPassword != passwordFromDb)

                {

                    ModelState.AddModelError("WriterPassword", "Girilen şifre yanlış.");
                    return View();
                }

                else if (p.WriterPassword != p.WriterConfirmPassword)
                {
                    ModelState.AddModelError("WriterConfirmPassword", "Şifreler uyuşmuyor.");
                    return View();
                }

                else
                {
                    writer.WriterID = v.WriterID; 

         
                    writer.WriterMail = p.WriterMail;
                    writer.WriterName = p.WriterName;
                    writer.WriterStatus = true;
                    writer.WriterAbout = p.WriterAbout;

                    if (p.WriterImage != null)
                    {
                        var extension = Path.GetExtension(p.WriterImage.FileName);
                        var newImageName = Guid.NewGuid().ToString() + extension;
                        var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/WriterImageFiles/", newImageName);
                        using (var stream = new FileStream(location, FileMode.Create))
                        {
                            p.WriterImage.CopyTo(stream);
                        }
                        writer.WriterImage = newImageName;
                    }
                    else
                    {
                        // Yeni resim yüklenmemişse, mevcut resmi güncelle
                        writer.WriterImage = writer.WriterImage;
                    }

                    // Yazarı güncelleyin
                    wm.TUpdate(writer);
                    return RedirectToAction("WriterEditProfile", "Writer");



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



        }



        [HttpGet]
        public IActionResult WriterAdd()
        {
            return View();
        }


        [HttpPost]
        public IActionResult WriterAdd(AddProfileImage p)
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
            return RedirectToAction("WriterEditProfile", "Writer");
        }
        [HttpGet]
        public IActionResult WriterPasswordUpdate()
        {
            var usermail = User.Identity.Name;

            var writerID = c.Writers.Where(x => x.WriterMail == usermail)
                 .Select(y => y.WriterID).FirstOrDefault();
            var writervalues = wm.TGetById(writerID);
            return View(writervalues);
        }
        [HttpPost]
        public IActionResult WriterPasswordUpdate(Writer p)
        {

            WriterPasswordValidator wl = new WriterPasswordValidator();
            ValidationResult results = wl.Validate(p);

            var writer = wm.TGetById(p.WriterID);
            string passwordFromDb = writer.WriterPassword;

            if (results.IsValid)
            {
                if (p.WriterPassword != p.WriterConfirmPassword)
                {
                    ModelState.AddModelError("WriterConfirmPassword", "Şifreler uyuşmuyor.");
                    return View();
                }
                else
                {
                    writer.WriterPassword = p.WriterPassword;
                    writer.WriterConfirmPassword = p.WriterConfirmPassword;

                    try
                    {
                        wm.TUpdate(writer);
                        TempData["SuccessMessage"] = "Sifreniz basariyla guncellendi.";
                        return RedirectToAction("WriterPasswordUpdate", "Writer");
                    }
                    catch (Exception)
                    {
                        TempData["ErrorMessage"] = "Sifre guncellenirken bir hata olustu.";
                        return RedirectToAction("WriterPasswordUpdate", "Writer");
                    }
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




                //WriterValidator wl = new WriterValidator();
                //ValidationResult results = wl.Validate(p);

                //var writer = wm.TGetById(p.WriterID);

                //if (results.IsValid)
                //{
                //    if (p.WriterPassword != p.WriterConfirmPassword)
                //    {
                //        ModelState.AddModelError("WriterConfirmPassword", "Şifreler uyuşmuyor.");
                //        return View();
                //    }
                //    else
                //    {
                //        Writer w = new Writer
                //        {
                //            WriterID = writer.WriterID,
                //            WriterMail = writer.WriterMail,
                //            WriterName = writer.WriterName,
                //            WriterStatus = writer.WriterStatus,
                //            WriterAbout = writer.WriterAbout,
                //            WriterImage = writer.WriterImage,
                //            WriterPassword = p.WriterPassword,
                //            WriterConfirmPassword = p.WriterConfirmPassword
                //        };

                //        try
                //        {
                //            wm.TUpdate(w);
                //            TempData["SuccessMessage"] = "Şifreniz başarıyla güncellendi.";
                //            return RedirectToAction("WriterPasswordUpdate", "Writer");
                //        }
                //        catch (Exception)
                //        {
                //            TempData["ErrorMessage"] = "Şifre güncellenirken bir hata oluştu.";
                //            return RedirectToAction("WriterPasswordUpdate", "Writer");
                //        }
                //    }
                //}
                //else
                //{
                //    foreach (var item in results.Errors)
                //    {
                //        ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                //    }
                //}

                //return View();

                //WriterValidator wl = new WriterValidator();
                //ValidationResult results = wl.Validate(p);


                //var writer = wm.TGetById(p.WriterID);
                //if (results.IsValid)
                //{


                //    if (p.WriterPassword != p.WriterConfirmPassword)
                //    {
                //        ModelState.AddModelError("WriterConfirmPassword", "Şifreler uyuşmuyor.");
                //        return View();
                //    }

                //    else
                //    {

                //        // Yazarın mevcut değerlerini kullanarak yeni bir Writer nesnesi oluşturun
                //        Writer w = new Writer
                //        {
                //            WriterID = writer.WriterID,
                //            WriterMail = writer.WriterMail,
                //            WriterName = writer.WriterName,
                //            WriterStatus = writer.WriterStatus,
                //            WriterAbout = writer.WriterAbout, // Değerin aynı kalmasını sağlayın
                //            WriterImage = writer.WriterImage,
                //            WriterPassword = p.WriterPassword,
                //            WriterConfirmPassword = p.WriterConfirmPassword
                //        };


                //        wm.TUpdate(w);
                //        return RedirectToAction("WriterPasswordUpdate", "Writer");
                //    }

                //}
                //else
                //{
                //    foreach (var item in results.Errors)
                //    {
                //        ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                //    }
                //}
                //return View();

            }



        }
}



