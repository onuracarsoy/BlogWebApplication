using BussinesLayer.Concrete;
using DataAccesLayer.Concrete;
using DataAccesLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;

namespace MyAspNetCoreApp.Web.Controllers
{
   
    public class MessageController : Controller
    {
        Message2Manager mm = new Message2Manager(new EFMessage2Repository());

        public IActionResult InBox(int page=1)
        {
            Context c = new Context();
            var usermail = User.Identity.Name;

            var writerID = c.Writers.Where(x => x.WriterMail == usermail)
                 .Select(y => y.WriterID).FirstOrDefault();

            var values = mm.GetInboxListByWriter(writerID).ToPagedList(page, 5);
            return View(values);
        }

        public IActionResult MessageDetails(int id)

        {

            var value = mm.TGetById(id);
            return View(value);
        }
        public IActionResult SendBox(int page = 1)
        {
            Context c = new Context();
            var usermail = User.Identity.Name;

            var writerID = c.Writers.Where(x => x.WriterMail == usermail)
                 .Select(y => y.WriterID).FirstOrDefault();

            var values = mm.GetSendBoxListByWriter(writerID).ToPagedList(page, 5);
            return View(values);
        }



        [HttpGet]
        public IActionResult SendMessage()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SendMessage(Message2 p)
        {
            Context c = new Context();
            var usermail = User.Identity.Name;

            var writerID = c.Writers.Where(x => x.WriterMail == usermail)
                 .Select(y => y.WriterID).FirstOrDefault();
            p.SenderID = writerID;
            p.ReceiverID = 26;
            p.MessageStatus = true;
            p.MessageDate=Convert.ToDateTime(DateTime.Now.ToShortDateString());
            mm.TAdd(p);
            return RedirectToAction("InBox","Message");
        }
    }
}
