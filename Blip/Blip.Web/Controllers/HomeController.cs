using Blip.Web.DAL;
using Blip.Web.Models;
using System;
using System.Collections.Generic;
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
        public ActionResult Index(string sortOrder, string searchBy, string searchString)
        {
            string userName = User.Identity.Name;
            HomeIndexViewModel hiVM = new HomeIndexViewModel();

            hiVM.CurrentSearchBy = searchBy;
            hiVM.CurrentSearchString = searchString;
            hiVM.DateSortParm = String.IsNullOrEmpty(sortOrder) ? "date_asc" : "";
            hiVM.TitleSortParm = sortOrder == "Title" ? "title_desc" : "Title";
            hiVM.SenderSortParm = sortOrder == "Sender" ? "sender_desc" : "Sender";

            //hiVM.SearchByEnum = new List<string>() { "Title", "Sender", "Receivers", "Date", "Body" };
            hiVM.SearchByEnum = new List<string>() { "Title", "Sender", "Receivers", "Body" };
            hiVM.SearchBy = new List<SelectListItem>();

            hiVM.SearchBy.Add(new SelectListItem { Text = "", Value = "" });
            foreach (var sbEnum in hiVM.SearchByEnum)
            {
                if (sbEnum == searchBy)
                {
                    hiVM.SearchBy.Add(new SelectListItem { Text = sbEnum, Value = sbEnum, Selected = true });
                }
                else
                {
                    hiVM.SearchBy.Add(new SelectListItem { Text = sbEnum, Value = sbEnum });
                }
            }

            var hiVMBase = db.Messages
                .Include(m => m.Receivers)
                .Select(m => new HomeIndexViewModel.MessageIC
                {
                    MessageID = m.MessageID,
                    Title = m.Title,
                    DateTime = m.DateTime,
                    Body = m.Body,
                    Sender = m.Sender.UserName,
                    Receivers = m.Receivers.Select(r => r.UserName).ToList()
                }).Where(m => m.Sender == userName || m.Receivers.Contains(userName));

            if (!String.IsNullOrEmpty(searchString))
            {
                if (!String.IsNullOrEmpty(searchBy))
                {
                    switch (searchBy)
                    {
                        case "Title":
                            hiVMBase = hiVMBase.Where(m => m.Title.Contains(searchString));
                            break;
                        case "Sender":
                            hiVMBase = hiVMBase.Where(m => m.Sender.Contains(searchString));
                            break;
                        case "Receivers":
                            hiVMBase = hiVMBase.Where(m => m.Receivers.Contains(searchString));
                            break;
                        //case "Date":
                        //    hiVMBase = hiVMBase.Where(m => m.DateTime.Equals(searchString));
                        //    break;
                        case "Body":
                            hiVMBase = hiVMBase.Where(m => m.Body.Contains(searchString));
                            break;
                        default:
                            break;
                    }
                }              
            }

            switch (sortOrder)
            {
                case "Title":
                    hiVM.Messages = hiVMBase.OrderBy(m => m.Title).ToList<HomeIndexViewModel.MessageIC>();
                    break;
                case "title_desc":
                    hiVM.Messages = hiVMBase.OrderByDescending(m => m.Title).ToList<HomeIndexViewModel.MessageIC>();
                    break;
                case "Sender":
                    hiVM.Messages = hiVMBase.OrderBy(m => m.Sender).ToList<HomeIndexViewModel.MessageIC>();
                    break;
                case "sender_desc":
                    hiVM.Messages = hiVMBase.OrderByDescending(m => m.Sender).ToList<HomeIndexViewModel.MessageIC>();
                    break;
                case "date_asc":
                    hiVM.Messages = hiVMBase.OrderBy(m => m.DateTime).ToList<HomeIndexViewModel.MessageIC>();
                    break;
                default:
                    hiVM.Messages = hiVMBase.OrderByDescending(m => m.DateTime).ToList<HomeIndexViewModel.MessageIC>();
                    break;
            }

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
                    DateTime = DateTime.Now,
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