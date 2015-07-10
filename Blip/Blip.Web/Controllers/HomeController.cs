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
            string userName = User.Identity.Name;
            HomeIndexViewModel hiVM = new HomeIndexViewModel();

            hiVM.Messages = db.Messages
                .Include(m => m.Receivers)
                .Select(m => new HomeIndexViewModel.MessageIC
                {
                    MessageID = m.MessageID,
                    Title = m.Title,
                    DateTime = m.DateTime,
                    Body = m.Body,
                    Sender = m.Sender.UserName,
                    Receivers = m.Receivers.Select(r => r.UserName).ToList()
                }).Where(m => m.Sender == userName || m.Receivers.Contains(userName))
                .ToList<HomeIndexViewModel.MessageIC>();

            return View(hiVM);
        }

        public ActionResult Message()
        {
            string sender = User.Identity.Name;
            HomeMessageViewModel hmVM = new HomeMessageViewModel();

            hmVM.Receivers = db.Users
                .Where(u => u.Active == true && u.UserName != sender)
                .Select(u => new HomeMessageViewModel.ReceiverIC
                {
                    UserID = u.UserID,
                    UserName = u.UserName
                }).ToList();

            hmVM.Message = new HomeMessageViewModel.MessageIC();
            hmVM.Message.Sender = sender;

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
                    Title = hmVM.Message.Title,
                    DateTime = DateTime.Today,
                    Body = hmVM.Message.Body,
                    Sender = db.Users
                        .Where(u => u.UserName == hmVM.Message.Sender)
                        .SingleOrDefault<User>(),
                    Receivers = db.Users
                        .Where(u => hmVM.Message.Receivers.Any(r => r == u.UserID))
                        .ToList<User>()
                };

                db.Messages.Add(message);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            // If the ModelState is invalid, repopulate Receivers for the multiselect list
            hmVM.Receivers = db.Users
                .Where(u => u.Active == true)
                .Select(u => new HomeMessageViewModel.ReceiverIC
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