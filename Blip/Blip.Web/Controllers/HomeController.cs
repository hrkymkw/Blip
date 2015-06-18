using Blip.Web.DAL;
using Blip.Web.Models;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace Blip.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private BlipContext db = new BlipContext();

        //[AllowAnonymous]
        public ActionResult Index()
        {
            var messageVM = db.Messages
                .Include(m => m.Receivers)
                .ToList()
                .Select(m => new HomeIndexViewModel
                {
                    MessageID = m.MessageID,
                    Title = m.Title,
                    DateTime = m.DateTime,
                    Body = m.Body,
                    Sender = m.Sender.UserName,
                    Receivers = m.Receivers.Select(r => r.UserName).ToList().ToArray()
                });
            return View(messageVM);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}