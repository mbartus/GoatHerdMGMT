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
    public class TreatmentController : Controller
    {
        private goatDBEntities db = new goatDBEntities();

        //
        // GET: /Treatment/
        public ActionResult Index()
        {
            var treatments = db.Treatments.Include(t => t.Animal);
            return View(treatments.ToList());
        }

        //
        // GET: /Treatment/Details/5
        public ActionResult Details(Int32 id)
        {
            Treatment treatment = db.Treatments.Find(id);
            if (treatment == null)
            {
                return HttpNotFound();
            }
            return View(treatment);
        }

        //
        // GET: /Treatment/Create
        public ActionResult Create()
        {
            ViewBag.animal_id = new SelectList(db.Animals, "id", "tag");
            return View();
        }

        //
        // POST: /Treatment/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Treatment treatment)
        {
            if (ModelState.IsValid)
            {
                db.Treatments.Add(treatment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.animal_id = new SelectList(db.Animals, "id", "tag", treatment.animal_id);
            return View(treatment);
        }

        //
        // GET: /Treatment/Edit/5
        public ActionResult Edit(Int32 id)
        {
            Treatment treatment = db.Treatments.Find(id);
            if (treatment == null)
            {
                return HttpNotFound();
            }
            ViewBag.animal_id = new SelectList(db.Animals, "id", "tag", treatment.animal_id);
            return View(treatment);
        }

        //
        // POST: /Treatment/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Treatment treatment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(treatment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.animal_id = new SelectList(db.Animals, "id", "tag", treatment.animal_id);
            return View(treatment);
        }

        //
        // GET: /Treatment/Delete/5
        public ActionResult Delete(Int32 id)
        {
            Treatment treatment = db.Treatments.Find(id);
            if (treatment == null)
            {
                return HttpNotFound();
            }
            return View(treatment);
        }

        //
        // POST: /Treatment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Int32 id)
        {
            Treatment treatment = db.Treatments.Find(id);
            db.Treatments.Remove(treatment);
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
