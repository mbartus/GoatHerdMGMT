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
    public class LicenseController : Controller
    {
        private goatDBEntities db = new goatDBEntities();

        //
        // GET: /License/Edit/5
        [Authorize(Roles = "admin")]
        public ActionResult Edit(Int32 id)
        {
            License license = db.Licenses.Find(id);
            if (license == null)
            {
                return HttpNotFound();
            }
            return View(license);
        }

        //
        // POST: /License/Edit/5
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(License license)
        {
            if (ModelState.IsValid)
            {
                db.Entry(license).State = EntityState.Modified;
                try
                {
                    db.SaveChanges();
                }
                catch
                {
                    return RedirectToAction("Error", "Home");
                }
                return RedirectToAction("Dashboard", "Home");
            }
            return View(license);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
