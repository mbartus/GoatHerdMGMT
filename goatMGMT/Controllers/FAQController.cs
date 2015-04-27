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
    [Authorize(Roles="admin")]
    public class FAQController : Controller
    {
        private goatDBEntities db = new goatDBEntities();

        //
        // GET: /FAQ
        public ActionResult Index()
        {
            return View(db.FAQs.ToList());
        }

        //
        // GET: /FAQ/Details/5
        public ActionResult Details(Int32 id)
        {
            FAQ faq = db.FAQs.Find(id);
            if (faq == null)
            {
                return HttpNotFound();
            }
            return View(faq);
        }

        //
        // GET: /FAQ/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /FAQ/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FAQ faq)
        {
            if (ModelState.IsValid)
            {
                db.FAQs.Add(faq);
                try
                {
                    db.SaveChanges();
                }
                catch
                {
                    return RedirectToAction("Error", "Home");
                }
                return RedirectToAction("Index");
            }

            return View(faq);
        }

        //
        // GET: /FAQ/Edit/5
        public ActionResult Edit(Int32 id)
        {
            FAQ faq = db.FAQs.Find(id);
            if (faq == null)
            {
                return HttpNotFound();
            }
            return View(faq);
        }

        //
        // POST: /FAQ/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FAQ faq)
        {
            if (ModelState.IsValid)
            {
                db.Entry(faq).State = EntityState.Modified;
                try
                {
                    db.SaveChanges();
                }
                catch
                {
                    return RedirectToAction("Error", "Home");
                }
                return RedirectToAction("Index");
            }
            return View(faq);
        }

        //
        // GET: /FAQ/Delete/5
        public ActionResult Delete(Int32 id)
        {
            FAQ faq = db.FAQs.Find(id);
            if (faq == null)
            {
                return HttpNotFound();
            }
            return View(faq);
        }

        //
        // POST: /FAQ/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Int32 id)
        {
            FAQ faq = db.FAQs.Find(id);
            db.FAQs.Remove(faq);
            try
            {
                db.SaveChanges();
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
