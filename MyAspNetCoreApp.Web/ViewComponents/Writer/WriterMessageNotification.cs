using BussinesLayer.Concrete;
using DataAccesLayer.Concrete;
using DataAccesLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace MyAspNetCoreApp.Web.ViewComponents.Writer
{
   
    public class WriterMessageNotification : ViewComponent
    {
        Message2Manager mm = new Message2Manager(new EFMessage2Repository());
        public IViewComponentResult Invoke()
        {
            Context c = new Context();

            var usermail = User.Identity.Name;

            var writerID = c.Writers.Where(x => x.WriterMail == usermail)
                 .Select(y => y.WriterID).FirstOrDefault();
           
            var values = mm.GetInboxListByWriter(writerID);
            return View(values);
        }
    }
}
