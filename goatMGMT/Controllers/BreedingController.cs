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
            List<BreedingExtended> beList = new List<BreedingExtended>();
            int userID = (int)Membership.GetUser().ProviderUserKey;
            var breedings = db.Breedings.Include(a => a.Animal.UserProfile).Where(m => m.Animal.owner == userID);
            foreach (Breeding bre in breedings)
            {
                BreedingExtended be = new BreedingExtended();
                be.breed = bre;
                be.father_name = db.Animals.Find(bre.father_id).name;
                be.mother_name = db.Animals.Find(bre.mother_id).name;
                be.father_tag = db.Animals.Find(bre.father_id).tag;
                be.mother_tag = db.Animals.Find(bre.mother_id).tag;
                beList.Add(be);
            }
            return View(beList);
        }

        //
        // GET: /Breeding/Details/5
        public ActionResult Details(Int32 id, Int32 id2, Int32 id3)
        {
            Breeding breeding = db.Breedings.Find(id, id2, id3);
            if (breeding == null)
            {
                return HttpNotFound();
            }
            return View(breeding);
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
            mlist.Add(new SelectListItem { Text = "Select Father", Value = "0" });
            flist.Add(new SelectListItem { Text = "Select Mother", Value = "0" });
            for (int i = 1; i <= bvm.maleList.Count(); i++)
            {
                mlist.Add(new SelectListItem { Text = bvm.maleList.ElementAt(i - 1).name, Value = "" + i });
            }
            for (int i = 1; i <= bvm.femaleList.Count(); i++)
            {
                flist.Add(new SelectListItem { Text = bvm.femaleList.ElementAt(i - 1).name, Value = "" + i });
            }
            @ViewBag.flist = flist;
            @ViewBag.mlist = mlist;
            return View();
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
            
            if (ModelState.IsValid && breeding.father_id != 0 && breeding.mother_id != 0)
            {

                breeding.Animal = db.Animals.Find(bvm.maleList.ElementAt(breeding.father_id - 1).id);
                breeding.Animal1 = db.Animals.Find(bvm.femaleList.ElementAt(breeding.mother_id - 1).id);
                breeding.father_id = breeding.Animal.id;
                breeding.mother_id = breeding.Animal1.id;
                db.Breedings.Add(breeding);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        //
        // GET: /Breeding/Edit/5
        public ActionResult Edit(Int32 id, Int32 id2, Int32 id3)
        {
            Breeding breeding = db.Breedings.Find(id, id2, id3);
            if (breeding == null)
            {
                return HttpNotFound();
            }
            return View(breeding);
        }

        //
        // POST: /Breeding/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Breeding breeding)
        {
            if (ModelState.IsValid)
            {
                db.Entry(breeding).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(breeding);
        }

        //
        // GET: /Breeding/Delete/5
        public ActionResult Delete(Int32 id, Int32 id2, Int32 id3)
        {
            Breeding breeding = db.Breedings.Find(id, id2, id3);
            if (breeding == null)
            {
                return HttpNotFound();
            }
            return View(breeding);
        }

        //
        // POST: /Breeding/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Int32 id, Int32 id2, Int32 id3)
        {
            Breeding breeding = db.Breedings.Find(id, id2, id3);
            db.Breedings.Remove(breeding);
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
