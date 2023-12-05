using BussinesLayer.Concrete;
using BussinesLayer.ValidationRules;
using DataAccesLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyAspNetCoreApp.Web.Models;

namespace MyAspNetCoreApp.Web.Controllers
{
    [AllowAnonymous]

    public class RegisterUserController : Controller
    {
        WriterManager wm = new WriterManager(new EFWriterRepository());

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Index(Writer p)
        {
            WriterValidator wv = new WriterValidator();
            ValidationResult results = wv.Validate(p);

            if (results.IsValid)
            {

                if (p.WriterPassword != p.WriterConfirmPassword)
                {
                    ModelState.AddModelError("WriterConfirmPassword", "Şifreler uyuşmuyor.");
                    return View();
                }
                else
                {


                    p.WriterAbout = "Default";
                    p.WriterStatus = true;
                    p.WriterImage = "defaultwriterpp.png";
                    wm.TUpdate(p);
                    return RedirectToAction("Index", "Dashboard");
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


    }
}

