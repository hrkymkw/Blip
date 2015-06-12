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
    public class HomeController : Controller
    {
        private BlipContext db = new BlipContext();

        // GET: Home
        public ActionResult Index()
        {
            var messageVM = db.Messages.ToList()
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