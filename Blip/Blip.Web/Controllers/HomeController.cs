using Blip.Web.DAL;
using Blip.Web.Models;
using System;
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
            var hiVM = db.Messages
                .Include(m => m.Receivers)
                .Select(m => new HomeIndexViewModel
                {
                    MessageID = m.MessageID,
                    Title = m.Title,
                    DateTime = m.DateTime,
                    Body = m.Body,
                    Sender = m.Sender.UserName,
                    Receivers = m.Receivers.Select(r => r.UserName).ToList()
                });

            return View(hiVM);
        }

        public ActionResult Message(string sender)
        {
            sender = (sender == User.Identity.Name) ? sender : User.Identity.Name;
            HomeMessageViewModel hmVM = new HomeMessageViewModel();

            hmVM.usersVM = db.Users
                .Where(u => u.Active == true && u.UserName != sender)
                .Select(u => new HomeMessageUserViewModel
                {
                    UserID = u.UserID,
                    UserName = u.UserName
                }).ToList();

            hmVM.messageVM = new HomeMessageMessageViewModel();
            hmVM.messageVM.Sender = sender;

            return View(hmVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Message(HomeMessageViewModel hmVM)
        {
            if(ModelState.IsValid)
            {
                Message message = new Message()
                {
                    Title = hmVM.messageVM.Title,
                    DateTime = DateTime.Today,
                    Body = hmVM.messageVM.Body,
                    Sender = db.Users
                        .Where(u => u.UserName == hmVM.messageVM.Sender)
                        .SingleOrDefault<User>(),
                    Receivers = db.Users
                        .Where(u => hmVM.messageVM.Receivers.Any(r => r == u.UserID))
                        .ToList<User>()
                };

                db.Messages.Add(message);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            // If the ModelState is invalid, repopulate usersVM for the multiselect list
            hmVM.usersVM = db.Users
                .Where(u => u.Active == true)
                .Select(u => new HomeMessageUserViewModel
                {
                    UserID = u.UserID,
                    UserName = u.UserName
                }).ToList();

            return View(hmVM);
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