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
    public class BirthController : Controller
    {
        private goatDBEntities db = new goatDBEntities();

        //
        // GET: /Birth/
        public ActionResult Index()
        {
            var births = db.Births.Include(b => b.Animal).Include(b => b.Animal1).Include(b => b.Animal2);
            return View(births.ToList());
        }

        //
        // GET: /Birth/Details/5
        public ActionResult Details(Int32 id)
        {
            Birth birth = db.Births.Find(id);
            if (birth == null)
            {
                return HttpNotFound();
            }
            return View(birth);
        }

        //
        // GET: /Birth/Create
        public ActionResult Create()
        {
            ViewBag.child_id = new SelectList(db.Animals, "id", "tag");
            ViewBag.father_id = new SelectList(db.Animals, "id", "tag");
            ViewBag.mother_id = new SelectList(db.Animals, "id", "tag");
            return View();
        }

        //
        // POST: /Birth/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Birth birth)
        {
            if (ModelState.IsValid)
            {
                db.Births.Add(birth);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.child_id = new SelectList(db.Animals, "id", "tag", birth.child_id);
            ViewBag.father_id = new SelectList(db.Animals, "id", "tag", birth.father_id);
            ViewBag.mother_id = new SelectList(db.Animals, "id", "tag", birth.mother_id);
            return View(birth);
        }

        //
        // GET: /Birth/Edit/5
        public ActionResult Edit(Int32 id)
        {
            Birth birth = db.Births.Find(id);
            if (birth == null)
            {
                return HttpNotFound();
            }
            ViewBag.child_id = new SelectList(db.Animals, "id", "tag", birth.child_id);
            ViewBag.father_id = new SelectList(db.Animals, "id", "tag", birth.father_id);
            ViewBag.mother_id = new SelectList(db.Animals, "id", "tag", birth.mother_id);
            return View(birth);
        }

        //
        // POST: /Birth/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Birth birth)
        {
            if (ModelState.IsValid)
            {
                db.Entry(birth).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.child_id = new SelectList(db.Animals, "id", "tag", birth.child_id);
            ViewBag.father_id = new SelectList(db.Animals, "id", "tag", birth.father_id);
            ViewBag.mother_id = new SelectList(db.Animals, "id", "tag", birth.mother_id);
            return View(birth);
        }

        //
        // GET: /Birth/Delete/5
        public ActionResult Delete(Int32 id)
        {
            Birth birth = db.Births.Find(id);
            if (birth == null)
            {
                return HttpNotFound();
            }
            return View(birth);
        }

        //
        // POST: /Birth/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Int32 id)
        {
            Birth birth = db.Births.Find(id);
            db.Births.Remove(birth);
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
