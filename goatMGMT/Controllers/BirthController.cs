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
        public ActionResult Index(Int32 id)
        {
            List<BirthViewModel> bvmList = new List<BirthViewModel>();
            int userID = (int)Membership.GetUser().ProviderUserKey;
            var births = db.Births.Include(a => a.Animal.UserProfile).Where(b => b.breed_id == id);
            foreach (Birth birth in births)
            {
                BirthViewModel bvm = new BirthViewModel();
                bvm.birth = birth;
                bvm.offspring_tag = birth.Animal.tag;
                bvm.father_tag = db.Animals.Find(db.Breedings.Find(id).father_id).tag;
                bvm.mother_tag = db.Animals.Find(db.Breedings.Find(id).mother_id).tag;
                bvmList.Add(bvm);
            }
            BirthViewModel bvmFinal = new BirthViewModel();
            bvmFinal.ien = bvmList;
            bvmFinal.birth = new Birth();
            bvmFinal.birth.breed_id = id;
            return View(bvmFinal);
        }

        //
        // GET: /Birth/Details/5
        public ActionResult Details(Int32 id)
        {
            Birth birth = db.Births.FirstOrDefault(m => m.id == id);
            if (birth == null)
            {
                return HttpNotFound();
            }
            BirthViewModel bvm = new BirthViewModel();
            bvm.birth = birth;
            bvm.offspring_tag = birth.Animal.tag;
            bvm.father_tag = db.Animals.Find(db.Breedings.Find(birth.breed_id).father_id).tag;
            bvm.mother_tag = db.Animals.Find(db.Breedings.Find(birth.breed_id).mother_id).tag;
            return View(bvm);
        }

        //
        // GET: /Birth/Create
        public ActionResult Create(Int32 id)
        {
            BirthViewModel bvm = new BirthViewModel();
            int userID = (int)Membership.GetUser().ProviderUserKey;
            var births = db.Animals.Include(a => a.UserProfile).Where(m => m.owner == userID && m.isChild == true);
            bvm.birth = new Birth();
            bvm.birth.breed_id = id;
            bvm.offspring = new List<System.Web.Mvc.SelectListItem>();
            bvm.offspring.Add(new System.Web.Mvc.SelectListItem { Text = "Select Offspring", Value = "" + -1 });
            foreach (Animal birth in births)
            {
                if (db.Births.Where(m => m.child_id == birth.id).Count() < 1)
                {
                    bvm.offspring.Add(new System.Web.Mvc.SelectListItem { Text = birth.tag, Value = "" + birth.id });
                }
            }
            @ViewBag.offspringDrop = bvm.offspring;
            return View("Create", bvm);
        }

        //
        // POST: /Birth/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BirthViewModel birthViewModel)
        {
            if (ModelState.IsValid && birthViewModel.offspringChoice != -1)
            {
                birthViewModel.birth.Animal = db.Animals.Find(birthViewModel.offspringChoice);
                db.Births.Add(birthViewModel.birth);
                try
                {
                    db.SaveChanges();
                }
                catch
                {
                    return RedirectToAction("Error", "Home");
                }
                return RedirectToAction("Index", new { id = birthViewModel.birth.breed_id });
            }

            BirthViewModel bvm = new BirthViewModel();
            bvm.birth = birthViewModel.birth;
            int userID = (int)Membership.GetUser().ProviderUserKey;
            var births = db.Animals.Include(a => a.UserProfile).Where(m => m.owner == userID && m.isChild == true);
            bvm.offspring = new List<System.Web.Mvc.SelectListItem>();
            bvm.offspring.Add(new System.Web.Mvc.SelectListItem { Text = "Select Offspring", Value = "" + -1 });
            foreach (Animal eachBirth in births)
            {
                bvm.offspring.Add(new System.Web.Mvc.SelectListItem { Text = eachBirth.tag, Value = "" + eachBirth.id });
            }
            @ViewBag.offspringDrop = bvm.offspring;
            if (birthViewModel.offspringChoice == 0)
            {
                ModelState.AddModelError("", "Please choose an offspring (must be an animal in your herd");
            }
            return View(bvm);
        }

        //
        // GET: /Birth/Edit/5
        public ActionResult Edit(Int32 id)
        {
            Birth birth = db.Births.FirstOrDefault(m => m.id == id);
            if (birth == null)
            {
                return HttpNotFound();
            }
            BirthViewModel bvm = new BirthViewModel();
            bvm.birth = birth;
            bvm.offspring_tag = birth.Animal.tag;
            bvm.father_tag = db.Animals.Find(db.Breedings.Find(birth.breed_id).father_id).tag;
            bvm.mother_tag = db.Animals.Find(db.Breedings.Find(birth.breed_id).mother_id).tag;
            return View(bvm);
        }

        //
        // POST: /Birth/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Birth birth)
        {
            birth.Animal = db.Animals.Find(birth.child_id);
            if (ModelState.IsValid)
            {
                db.Entry(birth).State = EntityState.Modified;
                try
                {
                    db.SaveChanges();
                }
                catch
                {
                    return RedirectToAction("Error", "Home");
                }
                return RedirectToAction("Index", new { id = birth.breed_id });
            }
            BirthViewModel bvm = new BirthViewModel();
            bvm.birth = birth;
            bvm.offspring_tag = birth.Animal.tag;
            bvm.father_tag = db.Animals.Find(db.Breedings.Find(birth.breed_id).father_id).tag;
            bvm.mother_tag = db.Animals.Find(db.Breedings.Find(birth.breed_id).mother_id).tag;

            return View(birth);
        }

        //
        // GET: /Birth/Delete/5
        public ActionResult Delete(Int32 id)
        {
            Birth birth = db.Births.FirstOrDefault(m => m.id == id);
            if (birth == null)
            {
                return HttpNotFound();
            }
            BirthViewModel bvm = new BirthViewModel();
            bvm.birth = birth;
            bvm.offspring_tag = birth.Animal.tag;
            bvm.father_tag = db.Animals.Find(db.Breedings.Find(birth.breed_id).father_id).tag;
            bvm.mother_tag = db.Animals.Find(db.Breedings.Find(birth.breed_id).mother_id).tag;
            return View(bvm);
        }

        //
        // POST: /Birth/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Int32 id)
        {
            Birth birth = db.Births.FirstOrDefault(m => m.id == id);
            db.Births.Remove(birth);
            try
            {
                db.SaveChanges();
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
            return RedirectToAction("Index", new { id = birth.breed_id });
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
