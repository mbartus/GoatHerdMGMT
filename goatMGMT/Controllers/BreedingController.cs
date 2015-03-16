using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using goatMGMT.Models;

namespace goatMGMT.Controllers
{
    public class BreedingController : Controller
    {
        private goatDBEntities db = new goatDBEntities();

        //
        // GET: /Breeding/
        public ActionResult Index()
        {
            return View(db.Breedings.ToList());
        }

        //
        // GET: /Breeding/Details/5
        public ActionResult Details(Int32 id)
        {
            Breeding breeding = db.Breedings.Find(id);
            if (breeding == null)
            {
                return HttpNotFound();
            }
            return View(breeding);
        }

        //
        // GET: /Breeding/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Breeding/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Breeding breeding)
        {
            if (ModelState.IsValid)
            {
                db.Breedings.Add(breeding);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(breeding);
        }

        //
        // GET: /Breeding/Edit/5
        public ActionResult Edit(Int32 id)
        {
            Breeding breeding = db.Breedings.Find(id);
            if (breeding == null)
            {
                return HttpNotFound();
            }
            return View(breeding);
        }

        //
        // POST: /Breeding/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Breeding breeding)
        {
            if (ModelState.IsValid)
            {
                db.Entry(breeding).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(breeding);
        }

        //
        // GET: /Breeding/Delete/5
        public ActionResult Delete(Int32 id)
        {
            Breeding breeding = db.Breedings.Find(id);
            if (breeding == null)
            {
                return HttpNotFound();
            }
            return View(breeding);
        }

        //
        // POST: /Breeding/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Int32 id)
        {
            Breeding breeding = db.Breedings.Find(id);
            db.Breedings.Remove(breeding);
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
