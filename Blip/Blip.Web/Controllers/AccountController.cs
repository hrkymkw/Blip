using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Blip.Web.DAL;
using Blip.Web.Models;

namespace Blip.Web.Controllers
{
    public class AccountController : Controller
    {
        private BlipContext db = new BlipContext();

        // GET: Account
        public ActionResult Index()
        {
            var viewModel = db.Users.ToList()
                .Where(u => u.Active == true)
                .Select(u => new UserViewModel
                {
                    UserName = u.UserName,
                    Password = u.Password,
                    Role = u.Role
                });
            return View(viewModel);
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
