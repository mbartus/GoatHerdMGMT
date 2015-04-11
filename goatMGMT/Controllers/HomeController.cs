using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using goatMGMT.Models;
using System.Web.Security;

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

        [Authorize(Roles = "admin, user")]
        public ActionResult Summary()
        {
            int userID = (int)Membership.GetUser().ProviderUserKey;
            SummaryViewModel svm = new SummaryViewModel();
            var myAnimalList = db.Animals.Where(m => m.owner == userID).ToList();
            svm.totalSire = db.Animals.Where(m => m.owner == userID && m.sex == false).Count();
            svm.totalDam = db.Animals.Where(m => m.owner == userID && m.sex == true).Count();
            svm.activeSire = svm.totalSire = db.Animals.Where(m => m.owner == userID && m.sex == false && m.status_code == "Active").Count();
            svm.activeDam = svm.totalSire = db.Animals.Where(m => m.owner == userID && m.sex == true && m.status_code == "Active").Count();
            svm.currentYear = System.DateTime.Now.Year;
            svm.lastYear = svm.currentYear - 1;
            foreach (Birth birth in db.Births)
            {
                foreach (Animal animal in myAnimalList.Where(m => m.sex == false))
                {
                    if (birth.Breeding.mother_id == animal.id)
                    {
                        if (birth.parity == 1)
                        {
                            svm.damParity1Count++;
                        }
                        else if (birth.parity == 2)
                        {
                            svm.damParity2Count++;
                        }
                        else if (birth.parity == 3)
                        {
                            svm.damParity3Count++;
                        }
                        else 
                        {
                            svm.damParity4Count++;
                        }
                    }
                }
            }
            return View(svm);
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}