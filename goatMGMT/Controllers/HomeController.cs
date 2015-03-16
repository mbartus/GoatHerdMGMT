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
        public ActionResult Contact()
        {
            ViewBag.Message = "";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(CommentViewModel cvm)
        {
            if (ModelState.IsValid)
            {
                Comment com = new Comment();
                com.name = cvm.name;
                com.email = cvm.email;
                com.subject = cvm.subject;
                com.comment1 = cvm.comment;
                com.date_sent = System.DateTime.Now;
                db.Comments.Add(com);
              
                    db.SaveChanges();
           

                return View();
            }
            return View(cvm);
        }

        public ActionResult Pricing()
        {
            return View();
        }

        [Authorize]
        public ActionResult Dashboard()
        {
            return View();
        }
    }
}