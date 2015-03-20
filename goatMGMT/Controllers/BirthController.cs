using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using goatMGMT.Models;
using System.Web.Security;
using System.Web.WebPages.Html;

namespace goatMGMT.Controllers
{
    public class BirthController : Controller
    {
        private goatDBEntities db = new goatDBEntities();

        //
        // GET: /Birth/
        public ActionResult Index(Int32 id2, Int32 id3)
        {
            List<BirthViewModel> bvmList = new List<BirthViewModel>();
            int userID = (int)Membership.GetUser().ProviderUserKey;
            var births = db.Births.Include(b => b.Animal1.id == id2 && b.Animal2.id == id3).Where(m => m.Animal.owner == userID);
            foreach (Birth birth in births)
            {
                BirthViewModel bvm = new BirthViewModel();
                bvm.birth = birth;
                bvm.offspring_tag = birth.Animal.tag;
                bvm.father_tag = birth.Animal1.tag;
                bvm.mother_tag = birth.Animal2.tag;
                bvmList.Add(bvm);
            }
            return View(bvmList);
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
            BirthViewModel bvm = new BirthViewModel();
            bvm.birth = birth;
            bvm.offspring_tag = birth.Animal.tag;
            bvm.father_tag = birth.Animal1.tag;
            bvm.mother_tag = birth.Animal2.tag;
            return View(bvm);
        }

        //
        // GET: /Birth/Create
        public ActionResult Create(Int32 id, Int32 id2, Int32 id3)
        {
            BirthViewModel bvm = new BirthViewModel();
            int userID = (int)Membership.GetUser().ProviderUserKey;
            var births = db.Births.Include(a => a.Animal.UserProfile).Where(m => m.Animal.owner == userID && m.Animal.isChild == true);
            bvm.offspring.Add(new System.Web.WebPages.Html.SelectListItem { Text = "Select Offspring", Value = "" + -1 });
            foreach (Birth birth in births)
            {
                bvm.offspring.Add(new System.Web.WebPages.Html.SelectListItem { Text = birth.Animal.tag, Value = "" + birth.Animal.id });
            }
            return View(bvm);
        }

        //
        // POST: /Birth/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BirthViewModel birth)
        {
            if (ModelState.IsValid && birth.offspringChoice != -1)
            {
                birth.birth.Animal = db.Animals.Find(birth.offspringChoice);
                db.Births.Add(birth.birth);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            BirthViewModel bvm = new BirthViewModel();
            bvm.birth = birth.birth;
            int userID = (int)Membership.GetUser().ProviderUserKey;
            var births = db.Births.Include(a => a.Animal.UserProfile).Where(m => m.Animal.owner == userID && m.Animal.isChild == true);
            bvm.offspring.Add(new System.Web.WebPages.Html.SelectListItem { Text = "Select Offspring", Value = "" + 0 });
            foreach (Birth eachBirth in births)
            {
                bvm.offspring.Add(new System.Web.WebPages.Html.SelectListItem { Text = eachBirth.Animal.tag, Value = "" + eachBirth.Animal.id });
            }
            if (birth.offspringChoice == 0)
            {
                ModelState.AddModelError("", "Please choose an offspring (must be an animal in your herd");
            }
            return View(bvm);
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
            BirthViewModel bvm = new BirthViewModel();
            bvm.birth = birth;
            bvm.offspring_tag = birth.Animal.tag;
            bvm.father_tag = birth.Animal1.tag;
            bvm.mother_tag = birth.Animal2.tag;
            return View(bvm);
        }

        //
        // POST: /Birth/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BirthViewModel bvm)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bvm.birth).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            bvm.offspring_tag = bvm.birth.Animal.tag;
            bvm.father_tag = bvm.birth.Animal1.tag;
            bvm.mother_tag = bvm.birth.Animal2.tag;
            return View(bvm);
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
            BirthViewModel bvm = new BirthViewModel();
            bvm.birth = birth;
            bvm.offspring_tag = birth.Animal.tag;
            bvm.father_tag = birth.Animal1.tag;
            bvm.mother_tag = birth.Animal2.tag;
            return View(bvm);
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
