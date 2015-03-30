using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using goatMGMT.Models;
using System.Web.Security;

namespace goatMGMT.Controllers
{
    [Authorize]
    public class AssociateController : Controller
    {
        private goatDBEntities db = new goatDBEntities();

        //
        // GET: /Associate/
        public ActionResult Index()
        {
            int userID = (int)Membership.GetUser().ProviderUserKey;
            var myAssocaites = db.Associates.Where(m => m.userid == userID).ToList();
            if (User.IsInRole("admin"))
            {
                myAssocaites = db.Associates.ToList();
            }
            return View(myAssocaites);
        }

        //
        // GET: /Associate/Details/5
        public ActionResult Details(Int32 id)
        {
            Associate associate = db.Associates.Find(id);
            if (associate == null)
            {
                return HttpNotFound();
            }
            return View(associate);
        }

        //
        // GET: /Associate/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Associate/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Associate associate)
        {
            if (ModelState.IsValid)
            {
                db.Associates.Add(associate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(associate);
        }

        //
        // GET: /Associate/Edit/5
        public ActionResult Edit(Int32 id)
        {
            Associate associate = db.Associates.Find(id);
            if (associate == null)
            {
                return HttpNotFound();
            }
            return View(associate);
        }

        //
        // POST: /Associate/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Associate associate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(associate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(associate);
        }

        //
        // GET: /Associate/Delete/5
        public ActionResult Delete(Int32 id)
        {
            Associate associate = db.Associates.Find(id);
            if (associate == null)
            {
                return HttpNotFound();
            }
            return View(associate);
        }

        //
        // POST: /Associate/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Int32 id)
        {
            Associate associate = db.Associates.Find(id);
            db.Associates.Remove(associate);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
