using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using goatMGMT.Models;

namespace goatMGMT.Controllers
{
    public class HomeController : Controller
    {
        private goatDBEntities db = new goatDBEntities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "What do we do?";

            return View();
        }

        [HttpGet]
        public ActionResult FeedbackConfirm()
        {
            return View();
        }

        [HttpGet] 
        public ActionResult Contact()
        {
            ViewBag.Message = "";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(Comment com)
        {
            if (ModelState.IsValid)
            {
                com.date_sent = System.DateTime.Now;
                db.Comments.Add(com);
                try
                {
                    db.SaveChanges();
                }
                catch
                {
                    return RedirectToAction("Error", "Home");
                }
                return Redirect("~/Home/FeedbackConfirm");
            }
            return View(com);
        }

        public ActionResult Pricing()
        {
            return View();
        }

        [Authorize]
        public ActionResult Dashboard()
        {
            if (User.IsInRole("admin"))
            {
                ViewBag.Title = "Admin Dashboard";
            }
            else
            {
                ViewBag.Title = "Dashboard";
            }
            return View();
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}