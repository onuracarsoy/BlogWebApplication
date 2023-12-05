using BussinesLayer.Concrete;
using DataAccesLayer.Concrete;
using DataAccesLayer.EntityFramework;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Mvc;
using System;

namespace MyAspNetCoreApp.Web.ViewComponents.Writer
{
   

    public class WriterNavbarAbout : ViewComponent
    {
        WriterManager wm = new WriterManager(new EFWriterRepository());
        Context c = new Context();
        public IViewComponentResult Invoke(int id)
        {
            var usermail = User.Identity.Name;

            var writerID = c.Writers.Where(x => x.WriterMail == usermail)
                 .Select(y => y.WriterID).FirstOrDefault();
            var values = wm.TGetById(writerID);
            return View(values);
        }
    }
}
