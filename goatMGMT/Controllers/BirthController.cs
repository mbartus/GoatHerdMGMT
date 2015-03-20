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
            var births = db.Births.Include(a => a.Animal.UserProfile).Where(b => b.Animal1.id == id2 && b.Animal2.id == id3);
            foreach (Birth birth in births)
            {
                BirthViewModel bvm = new BirthViewModel();
                bvm.birth = birth;
                bvm.offspring_tag = birth.Animal.tag;
                bvm.father_tag = birth.Animal1.tag;
                bvm.mother_tag = birth.Animal2.tag;
                bvmList.Add(bvm);
            }
            BirthViewModel bvmFinal = new BirthViewModel();
            bvmFinal.ien = bvmList;
            bvmFinal.birth = new Birth();
            bvmFinal.birth.father_id = id2;
            bvmFinal.birth.mother_id = id3;
            return View(bvmFinal);
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
        public ActionResult Create(Int32 id2, Int32 id3)
        {
            BirthViewModel bvm = new BirthViewModel();
            int userID = (int)Membership.GetUser().ProviderUserKey;
            var births = db.Animals.Include(a => a.UserProfile).Where(m => m.owner == userID && m.isChild == true);
            bvm.birth = new Birth();
            bvm.birth.father_id = id2;
            bvm.birth.mother_id = id3;
            bvm.offspring = new List<System.Web.Mvc.SelectListItem>();
            bvm.offspring.Add(new System.Web.Mvc.SelectListItem { Text = "Select Offspring", Value = "" + -1 });
            foreach (Animal birth in births)
            {
                bvm.offspring.Add(new System.Web.Mvc.SelectListItem { Text = birth.tag, Value = "" + birth.id });
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
                birthViewModel.birth.Animal1 = db.Animals.Find(birthViewModel.birth.father_id);
                birthViewModel.birth.Animal2 = db.Animals.Find(birthViewModel.birth.mother_id);
                db.Births.Add(birthViewModel.birth);
                db.SaveChanges();
                return RedirectToAction("Index", new { id2 = birthViewModel.birth.father_id, id3 = birthViewModel.birth.mother_id});
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
        public ActionResult Edit(Int32 id, Int32 id2, Int32 id3)
        {
            Birth birth = db.Births.FirstOrDefault(m => m.id == id);
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
        public ActionResult Edit(Birth birth)
        {
            if (ModelState.IsValid)
            {
                db.Entry(birth).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //bvm.offspring_tag = bvm.birth.Animal.tag;
            //bvm.father_tag = bvm.birth.Animal1.tag;
            //bvm.mother_tag = bvm.birth.Animal2.tag;
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
