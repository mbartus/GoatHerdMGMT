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
    public class BreedingsController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Breedings
        public ActionResult Index()
        {
            return View(db.Breedings.ToList());
        }

        // GET: Breedings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Breedings breedings = db.Breedings.Find(id);
            if (breedings == null)
            {
                return HttpNotFound();
            }
            return View(breedings);
        }

        // GET: Breedings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Breedings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,mother_id,father_id,date,remarks")] Breedings breedings)
        {
            if (ModelState.IsValid)
            {
                db.Breedings.Add(breedings);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(breedings);
        }

        // GET: Breedings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Breedings breedings = db.Breedings.Find(id);
            if (breedings == null)
            {
                return HttpNotFound();
            }
            return View(breedings);
        }

        // POST: Breedings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,mother_id,father_id,date,remarks")] Breedings breedings)
        {
            if (ModelState.IsValid)
            {
                db.Entry(breedings).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(breedings);
        }

        // GET: Breedings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Breedings breedings = db.Breedings.Find(id);
            if (breedings == null)
            {
                return HttpNotFound();
            }
            return View(breedings);
        }

        // POST: Breedings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Breedings breedings = db.Breedings.Find(id);
            db.Breedings.Remove(breedings);
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
