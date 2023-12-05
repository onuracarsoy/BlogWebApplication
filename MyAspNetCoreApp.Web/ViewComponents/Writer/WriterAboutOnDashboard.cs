
using BussinesLayer.Concrete;
using DataAccesLayer.Concrete;
using DataAccesLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace MyAspNetCoreApp.Web.ViewComponents.Writer
{


    public class WriterAboutOnDashboard : ViewComponent
    {
        WriterManager wm = new WriterManager(new EFWriterRepository());


        Context c = new Context();


        public  IViewComponentResult Invoke()
        {


            var usermail = User.Identity.Name;

            var writerID = c.Writers.Where(x => x.WriterMail == usermail)
                 .Select(y => y.WriterID).FirstOrDefault();

            var values = wm.GetWriterById(writerID);
            return View(values);
        }
    }
}
