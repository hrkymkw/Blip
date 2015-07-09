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

        #region Log
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #endregion

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

                    bool userValid = bContext.Users.Any(user => user.UserName == username && user.Password == password && user.Active == true);

                    if (userValid)
                    {
                        log.Info("User [" + username + "] logged in successfully");

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
                        log.Debug("Login failed: The user name or password provided is incorrect.");
                        ModelState.AddModelError("", "The user name or password provided is incorrect.");
                    }
                }
            }

            return View(model);
        }

        public ActionResult LogOff()
        {
            log.Info("User [" + User.Identity.Name + "] logged off.");
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
                if(db.Users.Any(u => u.UserName == userVM.UserName))
                {
                    ViewBag.ErrorMessage = "* User Name \"" + userVM.UserName + "\" already exists.";
                    return View(userVM);
                }
                else
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
            }

            return View(userVM);
        }

        public ActionResult Index()
        {
            string userName = User.Identity.Name;

            var user = db.Users
                .Where(u => u.UserName == userName)
                .Select(u => new AccountIndexViewModel
                {
                    UserName = u.UserName,
                    Password = u.Password,
                    Role = u.Role,
                    Active = u.Active,
                    ActiveDate = u.ActiveDate,
                }).SingleOrDefault<AccountIndexViewModel>();

            if (user == null)
            {
                log.Error("User [" + userName + "] does not exist in the system.");
                return HttpNotFound();
            }
            return View(user);
        }

        public ActionResult Edit()
        {
            string userName = User.Identity.Name;

            var user = db.Users
                .Where(u => u.UserName == userName)
                .Select(u => new AccountEditViewModel
                {
                    UserID = u.UserID,
                    Password = u.Password,
                }).SingleOrDefault<AccountEditViewModel>();

            if (user == null)
            {
                log.Error("User [" + userName + "] does not exist in the system.");
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

                userM.Password = userVM.Password;

                db.Entry(userM).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userVM);
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


