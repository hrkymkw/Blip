using Blip.Web.DAL;
using Blip.Web.Areas.Admin.Models;
using System.Linq;
using System.Web.Mvc;
using System.Net;
using Blip.Web.Models;
using System;
using System.Data.Entity;

namespace Blip.Web.Areas.Admin.Controllers
{
    [Authorize]
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private BlipContext db = new BlipContext();

        public ActionResult Index()
        {
            AdminIndexViewModel aiVM = new AdminIndexViewModel();
            aiVM.ListOfUsers = db.Users
                //.Where(u => u.Active == true)
                .Select(u => new AdminIndexViewModel.UserIC
                {
                    UserID = u.UserID,
                    UserName = u.UserName,
                    Password = u.Password,
                    Role = u.Role,
                    Active = u.Active,
                    ActiveDate = u.ActiveDate,
                }).ToList<AdminIndexViewModel.UserIC>();
            return View(aiVM);
        }

        public ActionResult Details(string userName)
        {
            if (userName == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var user = db.Users
                .Where(u => u.UserName == userName)
                .Select(u => new AdminDetailsViewModel
                {
                    UserID = u.UserID,
                    UserName = u.UserName,
                    Password = u.Password,
                    Role = u.Role,
                    Active = u.Active,
                    ActiveDate = u.ActiveDate,
                }).SingleOrDefault<AdminDetailsViewModel>();

            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AdminCreateViewModel userVM)
        {
            if (ModelState.IsValid)
            {
                if (db.Users.Any(u => u.UserName == userVM.UserName))
                {
                    ViewBag.ErrorMessage = "* User Name \"" + userVM.UserName + "\" already exists.";
                    return View(userVM);
                }
                else
                {
                    User user = new Blip.Web.Models.User()
                    {
                        UserName = userVM.UserName,
                        Password = userVM.Password,
                        Role = userVM.Role,
                        Active = true,
                        ActiveDate = DateTime.Today
                    };

                    db.Users.Add(user);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            return View(userVM);
        }

        public ActionResult Edit(string userName)
        {
            if (userName == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //User user = db.Users.Find(userID);
            var user = db.Users
                .Where(u => u.UserName == userName)
                .Select(u => new AdminEditViewModel
                {
                    UserID = u.UserID,
                    UserName = u.UserName,
                    Password = u.Password,
                    Role = u.Role,
                    Active = u.Active,
                    ActiveDate = u.ActiveDate,
                }).SingleOrDefault<AdminEditViewModel>();

            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AdminEditViewModel userVM)
        {
            if (userVM == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            if (ModelState.IsValid)
            {
                User userM = db.Users.Find(userVM.UserID);
                bool wasActive = userM.Active;

                userM.UserName = userVM.UserName;
                userM.Password = userVM.Password;
                userM.Role = userVM.Role;
                userM.Active = userVM.Active;
                if (!wasActive && userM.Active)
                    userM.ActiveDate = DateTime.Today;
                else
                    userM.ActiveDate = userVM.ActiveDate;

                db.Entry(userM).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userVM);
        }

        public ActionResult Delete(string userName)
        {
            if (userName == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = db.Users
                .Where(u => u.UserName == userName && u.Active == true)
                .Select(u => new AdminDeleteViewModel
                {
                    UserID = u.UserID,
                    UserName = u.UserName,
                    Password = u.Password,
                    Role = u.Role,
                    Active = u.Active,
                    ActiveDate = u.ActiveDate,
                }).SingleOrDefault<AdminDeleteViewModel>();
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string userName)
        {
            User user = db.Users.SingleOrDefault<User>(u => u.UserName == userName);
            if (db.Messages.Any(m => m.Sender.UserID == user.UserID || m.Receivers.Any(r => r.UserID == user.UserID)))
            {
                user.Active = false;
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
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