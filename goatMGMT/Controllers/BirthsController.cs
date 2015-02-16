using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using goatMGMT.DAL;
using goatMGMT.Models;

namespace goatMGMT.Controllers
{
    public class BirthsController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Births
        public ActionResult Index()
        {
            return View(db.Births.ToList());
        }

        // GET: Births/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Births births = db.Births.Find(id);
            if (births == null)
            {
                return HttpNotFound();
            }
            return View(births);
        }

        // GET: Births/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Births/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,mother_id,father_id,date,birth_type,birth_parity,remarks")] Births births)
        {
            if (ModelState.IsValid)
            {
                db.Births.Add(births);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(births);
        }

        // GET: Births/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Births births = db.Births.Find(id);
            if (births == null)
            {
                return HttpNotFound();
            }
            return View(births);
        }

        // POST: Births/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,mother_id,father_id,date,birth_type,birth_parity,remarks")] Births births)
        {
            if (ModelState.IsValid)
            {
                db.Entry(births).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(births);
        }

        // GET: Births/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Births births = db.Births.Find(id);
            if (births == null)
            {
                return HttpNotFound();
            }
            return View(births);
        }

        // POST: Births/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Births births = db.Births.Find(id);
            db.Births.Remove(births);
            db.SaveChanges();
            return RedirectToAction("Index");
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
