using BussinesLayer.Concrete;
using BussinesLayer.ValidationRules;
using DataAccesLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;
namespace MyAspNetCoreApp.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AllowAnonymous]
    public class CategoryController : Controller
    {
        CategoryManager cm = new CategoryManager(new EFCategoryRepository());


        public IActionResult Index(int page = 1)
        {
            var values = cm.GetList().ToPagedList(page, 7);
            return View(values);
        }
        [HttpGet]
        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCategory(Category p)
        {

            CategoryValidator cv = new CategoryValidator();
            ValidationResult results = cv.Validate(p);

            if (results.IsValid)
            {
                if (results.IsValid)
                {


                    //Burdan admine onaylatılabilir
                    p.CategoryStatus = true;


                    cm.TAdd(p);

                }
                return RedirectToAction("Index", "Category");



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
        public IActionResult CategoryDelete(int id) 
        {
            var value= cm.TGetById(id);
            cm.TDelete(value);
            return RedirectToAction("Index");   
        }

        public IActionResult CategoryStatusF(int id)
        {
            var category = cm.TGetById(id);
            category.CategoryStatus = false;
            cm.TUpdate(category);
            return RedirectToAction("Index");
        }
        public IActionResult CategoryStatusT(int id)
        {
            var category = cm.TGetById(id);
            category.CategoryStatus = true;
            cm.TUpdate(category);
            return RedirectToAction("Index");
        }
    }
}
