using Blip.Web.DAL;
using Blip.Web.Models;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace Blip.Web.Controllers
{
    public class AccountController : Controller
    {
        private BlipContext db = new BlipContext();

        // GET: Account
        public ActionResult Index()
        {
            var userVM = db.Users.ToList()
                .Where(u => u.Active == true)
                .Select(u => new AccountIndexViewModel
                {
                    UserName = u.UserName,
                    Password = u.Password,
                    Role = u.Role
                });
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
    }
}