using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using goatMGMT.Models;
using System.Web.Security;

namespace goatMGMT.Controllers
{
    [Authorize]
    public class BreedingController : Controller
    {
        private goatDBEntities db = new goatDBEntities();

        //
        // GET: /Breeding/
        public ActionResult Index()
        {
            List<BreedingViewModel> beList = new List<BreedingViewModel>();
            int userID = (int)Membership.GetUser().ProviderUserKey;
            var breedings = db.Breedings.Include(a => a.Animal.UserProfile).Where(m => m.Animal.owner == userID);
            if (User.IsInRole("admin"))
            {
                breedings = db.Breedings.Include(a => a.Animal.UserProfile);
            }
            double damsBirthed = 0;
            double damsBred = 0;
            double offBorn = 0;
            double offAlive = 0;
            foreach (Breeding bre in breedings)
            {
                BreedingViewModel be = new BreedingViewModel();
                be.breeding = bre;
                be.father_name = db.Animals.Find(bre.father_id).name;
                be.mother_name = db.Animals.Find(bre.mother_id).name;
                be.father_tag = db.Animals.Find(bre.father_id).tag;
                be.mother_tag = db.Animals.Find(bre.mother_id).tag;
                beList.Add(be);
                if (bre.born != null)
                {
                    offBorn += (int)bre.born;
                }
                if (bre.alive != null)
                {
                    offAlive += (int)bre.alive;
                }
                if (bre.pregnancy_check)
                {
                        damsBirthed++;
                }
                damsBred++;
            }
            BreedingViewModel bvmFinal = new BreedingViewModel();
            bvmFinal.ien = beList;
            if (offBorn == 0)
            {
                bvmFinal.mortalityRate = 0;
            }
            else
            {
                bvmFinal.mortalityRate = (int)((1 - (offAlive / offBorn)) * 100);
            }
            if (damsBred == 0)
            {
                damsBred++;
            }
            bvmFinal.conceptionRate = (int) ((damsBirthed / damsBred) * 100);
            bvmFinal.totAlive = (int)offAlive;
            bvmFinal.totBorn = (int)offBorn;
            return View(bvmFinal);
        }

        //
        // GET: /Breeding/Details/5
        public ActionResult Details(Int32 id)
        {
            Breeding breeding = db.Breedings.Find(id);
            int userID = (int)Membership.GetUser().ProviderUserKey;
            if (breeding == null)
            {
                return HttpNotFound();
            }
            breeding.Animal = db.Animals.Find(breeding.father_id);
            if ((!User.IsInRole("admin")) && breeding.Animal.UserProfile.UserId != userID)
            {
                return HttpNotFound();
            }
            BreedingViewModel be = new BreedingViewModel();
            be.breeding = breeding;
            be.father_name = db.Animals.Find(breeding.father_id).name;
            be.mother_name = db.Animals.Find(breeding.mother_id).name;
            be.father_tag = db.Animals.Find(breeding.father_id).tag;
            be.mother_tag = db.Animals.Find(breeding.mother_id).tag;
            return View(be);
        }

        //
        // GET: /Breeding/Create
        public ActionResult Create()
        {
            BreedingViewModel bvm = new BreedingViewModel();
            int userID = (int)Membership.GetUser().ProviderUserKey;
            bvm.maleList = db.Animals.Include(a => a.UserProfile).Where(m => m.owner == userID && m.isChild == false && m.sex == true);
            bvm.femaleList = db.Animals.Include(a => a.UserProfile).Where(m => m.owner == userID && m.isChild == false && m.sex == false);
            List<SelectListItem> mlist = new List<SelectListItem>();
            List<SelectListItem> flist = new List<SelectListItem>();
            mlist.Add(new SelectListItem { Text = "Select Sire", Value = "0" });
            flist.Add(new SelectListItem { Text = "Select Dam", Value = "0" });
            for (int i = 1; i <= bvm.maleList.Count(); i++)
            {
                mlist.Add(new SelectListItem { Text = bvm.maleList.ElementAt(i - 1).tag, Value = "" + i });
            }
            for (int i = 1; i <= bvm.femaleList.Count(); i++)
            {
                flist.Add(new SelectListItem { Text = bvm.femaleList.ElementAt(i - 1).tag, Value = "" + i });
            }
            @ViewBag.flist = flist;
            @ViewBag.mlist = mlist;
            return View(bvm);
        }

        //
        // POST: /Breeding/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Breeding breeding)
        {
            BreedingViewModel bvm = new BreedingViewModel();
            int userID = (int)Membership.GetUser().ProviderUserKey;
            bvm.maleList = db.Animals.Include(a => a.UserProfile).Where(m => m.owner == userID && m.isChild == false && m.sex == true);
            bvm.femaleList = db.Animals.Include(a => a.UserProfile).Where(m => m.owner == userID && m.isChild == false && m.sex == false);
            List<SelectListItem> mlist = new List<SelectListItem>();
            List<SelectListItem> flist = new List<SelectListItem>();
            if (ModelState.IsValid && breeding.father_id != 0 && breeding.mother_id != 0)
            {
                if (breeding.actual_birthing_date != null && breeding.date != null)
                {
                    if (((DateTime)breeding.actual_birthing_date).CompareTo(((DateTime)breeding.date)) < 0)
                    {
                        ModelState.AddModelError("", "Birthing date cannot be after breeding date.");
                        mlist = new List<SelectListItem>();
                        flist = new List<SelectListItem>();
                        mlist.Add(new SelectListItem { Text = "Select Sire", Value = "0" });
                        flist.Add(new SelectListItem { Text = "Select Dam", Value = "0" });
                        for (int i = 1; i <= bvm.maleList.Count(); i++)
                        {
                            mlist.Add(new SelectListItem { Text = bvm.maleList.ElementAt(i - 1).tag, Value = "" + i });
                        }
                        for (int i = 1; i <= bvm.femaleList.Count(); i++)
                        {
                            flist.Add(new SelectListItem { Text = bvm.femaleList.ElementAt(i - 1).tag, Value = "" + i });
                        }
                        @ViewBag.flist = flist;
                        @ViewBag.mlist = mlist;
                        return View(bvm);
                    }
                }
                breeding.Animal = db.Animals.Find(bvm.maleList.ElementAt(breeding.father_id - 1).id);
                breeding.Animal1 = db.Animals.Find(bvm.femaleList.ElementAt(breeding.mother_id - 1).id);
                breeding.father_id = breeding.Animal.id;
                breeding.mother_id = breeding.Animal1.id;
                db.Breedings.Add(breeding);
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
            mlist.Add(new SelectListItem { Text = "Select Sire", Value = "0" });
            flist.Add(new SelectListItem { Text = "Select Dam", Value = "0" });
            for (int i = 1; i <= bvm.maleList.Count(); i++)
            {
                mlist.Add(new SelectListItem { Text = bvm.maleList.ElementAt(i - 1).tag, Value = "" + i });
            }
            for (int i = 1; i <= bvm.femaleList.Count(); i++)
            {
                flist.Add(new SelectListItem { Text = bvm.femaleList.ElementAt(i - 1).tag, Value = "" + i });
            }
            @ViewBag.flist = flist;
            @ViewBag.mlist = mlist;
            ModelState.AddModelError("", "Both a male and female must be selected.");
            return View(bvm);
        }

        //
        // GET: /Breeding/Edit/5
        public ActionResult Edit(Int32 id)
        {
            Breeding breeding = db.Breedings.Find(id);
            int userID = (int)Membership.GetUser().ProviderUserKey;
            if (breeding == null)
            {
                return HttpNotFound();
            }
            breeding.Animal = db.Animals.Find(breeding.father_id);
            if ((!User.IsInRole("admin")) && breeding.Animal.UserProfile.UserId != userID)
            {
                return HttpNotFound();
            }
            BreedingViewModel bvm = new BreedingViewModel();
            bvm.breeding = breeding;
            bvm.father_name = bvm.breeding.Animal.name;
            bvm.mother_name = bvm.breeding.Animal1.name;
            bvm.father_tag = db.Animals.Find(breeding.father_id).tag;
            bvm.mother_tag = db.Animals.Find(breeding.mother_id).tag;
            return View(bvm);
        }

        //
        // POST: /Breeding/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Breeding breeding)
        {
            int userID = (int)Membership.GetUser().ProviderUserKey;
            if (ModelState.IsValid)
            {
                db.Entry(breeding).State = EntityState.Modified;
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
            BreedingViewModel bvm = new BreedingViewModel();
            bvm.breeding = breeding;
            bvm.father_name = db.Animals.Find(breeding.father_id).name;
            bvm.mother_name = db.Animals.Find(breeding.mother_id).name;
            bvm.father_tag = db.Animals.Find(breeding.father_id).tag;
            bvm.mother_tag = db.Animals.Find(breeding.mother_id).tag;
            return View(bvm);
        }

        //
        // GET: /Breeding/Delete/5
        public ActionResult Delete(Int32 id)
        {
            Breeding breeding = db.Breedings.Find(id);
            int userID = (int)Membership.GetUser().ProviderUserKey;
            if (breeding == null)
            {
                return HttpNotFound();
            }
            breeding.Animal = db.Animals.Find(breeding.father_id);
            if ((!User.IsInRole("admin")) && breeding.Animal.UserProfile.UserId != userID)
            {
                return HttpNotFound();
            }
            BreedingViewModel be = new BreedingViewModel();
            be.breeding = breeding;
            be.father_name = db.Animals.Find(breeding.father_id).name;
            be.mother_name = db.Animals.Find(breeding.mother_id).name;
            be.father_tag = db.Animals.Find(breeding.father_id).tag;
            be.mother_tag = db.Animals.Find(breeding.mother_id).tag;
            return View(be);
        }

        //
        // POST: /Breeding/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Int32 id)
        {
            Breeding breeding = db.Breedings.Find(id);
            var births = db.Births.Include(a => a.Animal.UserProfile).Where(b => b.breed_id == id);
            db.Breedings.Remove(breeding);
            foreach (Birth bi in births)
            {
                db.Births.Remove(bi);
            }
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
