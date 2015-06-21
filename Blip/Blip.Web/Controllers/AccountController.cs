using Blip.Web.DAL;
using Blip.Web.Models;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.Security;

namespace Blip.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private BlipContext db = new BlipContext();

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(AccountLoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                using (BlipContext bContext = new BlipContext())
                {
                    string username = model.UserName;
                    string password = model.Password;

                    bool userValid = bContext.Users.Any(user => user.UserName == username && user.Password == password);

                    if (userValid)
                    {
                        FormsAuthentication.SetAuthCookie(username, false);
                        if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                            && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                        {
                            return Redirect(returnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "The user name or password provided is incorrect.");
                    }
                }
            }

            return View(model);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(AccountRegisterViewModel userVM)
        {
            if (ModelState.IsValid)
            {
                User user = new Models.User()
                {
                    UserName = userVM.UserName,
                    Password = userVM.Password,
                    Role = nameof(UserRoles.user),
                    Active = true,
                    ActiveDate = DateTime.Today
                };

                db.Users.Add(user);
                db.SaveChanges();

                bool userValid = db.Users.Any(u => u.UserID == user.UserID);

                if (userValid)
                {
                    FormsAuthentication.SetAuthCookie(user.UserName, false);
                }
                else
                {
                    ModelState.AddModelError("", "Registration failed.");
                    return View();
                }

                return RedirectToAction("Index", "Home");
            }

            return View(userVM);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            var userVM = db.Users.ToList()
                //.Where(u => u.Active == true)
                .Select(u => new AccountIndexViewModel
                {
                    UserID = u.UserID,
                    UserName = u.UserName,
                    Password = u.Password,
                    Role = u.Role,
                    Active = u.Active,
                    ActiveDate = u.ActiveDate,
                });
            return View(userVM);
        }

        public ActionResult Details(String userName)
        {
            if (userName == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var user = db.Users
                .Where(u => u.UserName == userName)
                .Select(u => new AccountDetailsViewModel
                {
                    UserID = u.UserID,
                    UserName = u.UserName,
                    Password = u.Password,
                    Role = u.Role,
                    Active = u.Active,
                    ActiveDate = u.ActiveDate,
                }).SingleOrDefault<AccountDetailsViewModel>();

            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AccountCreateViewModel userVM)
        {
            if (ModelState.IsValid)
            {
                User user = new Models.User()
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

            return View(userVM);
        }

        //[Authorize(Roles = "admin")]
        public ActionResult Edit(string userName)
        {
            if (userName == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //User user = db.Users.Find(userID);
            var user = db.Users
                .Where(u => u.UserName == userName)
                .Select(u => new AccountEditViewModel
                {
                    UserID = u.UserID,
                    UserName = u.UserName,
                    Password = u.Password,
                    Role = u.Role,
                    Active = u.Active,
                    ActiveDate = u.ActiveDate,
                }).SingleOrDefault<AccountEditViewModel>();

            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost]
        //[Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AccountEditViewModel userVM)
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
                if(!wasActive && userM.Active)
                    userM.ActiveDate = DateTime.Today;
                else
                    userM.ActiveDate = userVM.ActiveDate;

                db.Entry(userM).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userVM);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Delete(String userName)
        {
            if (userName == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.SingleOrDefault<User>(u => u.UserName == userName);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(String userName)
        {
            User user = db.Users.SingleOrDefault<User>(u => u.UserName == userName);
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

        public enum UserRoles
        {
            admin,
            user
        }
    }

}


