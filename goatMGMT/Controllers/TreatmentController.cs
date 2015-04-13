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
    public class TreatmentController : Controller
    {
        private goatDBEntities db = new goatDBEntities();

        //
        // GET: /Treatment/
        public ActionResult Index()
        {
            List<TreatmentViewModel> tvalist = new List<TreatmentViewModel>();
            int userID = (int)Membership.GetUser().ProviderUserKey;
            var transactions = db.Treatments.Include(a => a.Animal.UserProfile).Where(m => m.Animal.owner == userID);
            if (User.IsInRole("admin"))
            {
                transactions = db.Treatments.Include(a => a.Animal.UserProfile);
            }
            foreach (Treatment tra in transactions)
            {
                TreatmentViewModel tvm = new TreatmentViewModel();
                tvm.animal_name = tra.Animal.name;
                tvm.animal_tag = tra.Animal.tag;
                tvm.treatment = tra;
                tvalist.Add(tvm);
            }
            return View(tvalist);
        }

        //
        // GET: /Treatment/Details/5
        public ActionResult Details(Int32 id, Int32 id2)
        {
            Treatment treatment = db.Treatments.Find(id, id2);
            int userID = (int)Membership.GetUser().ProviderUserKey;
            if (treatment == null)
            {
                return HttpNotFound();
            }
            treatment.Animal = db.Animals.Find(treatment.animal_id);
            if ((!User.IsInRole("admin")) && treatment.Animal.UserProfile.UserId != userID)
            {
                return HttpNotFound();
            }
            TreatmentViewModel tvm = new TreatmentViewModel();
            tvm.treatment = treatment;
            tvm.animal_name = treatment.Animal.name;
            tvm.animal_tag = treatment.Animal.tag;
            return View(tvm);
        }

        //
        // GET: /Treatment/Create
        public ActionResult Create()
        {
            TreatmentViewModel tvm = new TreatmentViewModel();
            int userID = (int)Membership.GetUser().ProviderUserKey;
            tvm.animalList = db.Animals.Include(a => a.UserProfile).Where(m => m.owner == userID);
            List<SelectListItem> alist = new List<SelectListItem>();
            alist.Add(new SelectListItem { Text = "Select Animal", Value = "0" });
            for (int i = 1; i <= tvm.animalList.Count(); i++)
            {
                alist.Add(new SelectListItem { Text = tvm.animalList.ElementAt(i - 1).name, Value = "" + i });
            }
            @ViewBag.alist = alist;
            return View();
        }

        //
        // POST: /Treatment/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Treatment treatment)
        {
            TreatmentViewModel tvm = new TreatmentViewModel();
            int userID = (int)Membership.GetUser().ProviderUserKey;
            tvm.animalList = db.Animals.Include(a => a.UserProfile).Where(m => m.owner == userID);
            tvm.treatment = treatment;
            if (ModelState.IsValid && tvm.treatment.animal_id != 0)
            {
                tvm.treatment.Animal = db.Animals.Find(tvm.animalList.ElementAt(tvm.treatment.animal_id - 1).id);
                tvm.treatment.animal_id = tvm.treatment.Animal.id;
                db.Treatments.Add(tvm.treatment);
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
            List<SelectListItem> alist = new List<SelectListItem>();
            alist.Add(new SelectListItem { Text = "Select Animal", Value = "0" });
            for (int i = 1; i <= tvm.animalList.Count(); i++)
            {
                alist.Add(new SelectListItem { Text = tvm.animalList.ElementAt(i - 1).name, Value = "" + i });
            }
            @ViewBag.alist = alist;
            ModelState.AddModelError("", "Please select an animal.");
            return View(tvm);
        }

        //
        // GET: /treatment/Edit/5
        public ActionResult Edit(Int32 id, Int32 id2)
        {
            Treatment treatment = db.Treatments.Find(id, id2);
            int userID = (int)Membership.GetUser().ProviderUserKey;
            if (treatment == null)
            {
                return HttpNotFound();
            }
            treatment.Animal = db.Animals.Find(treatment.animal_id);
            if ((!User.IsInRole("admin")) && treatment.Animal.UserProfile.UserId != userID)
            {
                return HttpNotFound();
            }
            TreatmentViewModel tvm = new TreatmentViewModel();
            tvm.treatment = treatment;
            tvm.animal_name = tvm.treatment.Animal.name;
            return View(tvm);
        }

        //
        // POST: /treatment/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Treatment treatment)
        {
            int userID = (int)Membership.GetUser().ProviderUserKey;
            if (ModelState.IsValid)
            {
                db.Entry(treatment).State = EntityState.Modified;
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
            TreatmentViewModel tvm = new TreatmentViewModel();
            tvm.treatment = treatment;
            tvm.animal_name = db.Animals.Find(treatment.animal_id).name;
            return View(tvm);
        }

        //
        // GET: /treatment/Delete/5
        public ActionResult Delete(Int32 id, Int32 id2)
        {
            Treatment treatment = db.Treatments.Find(id, id2);
            int userID = (int)Membership.GetUser().ProviderUserKey;
            if (treatment == null)
            {
                return HttpNotFound();
            }
            treatment.Animal = db.Animals.Find(treatment.animal_id);
            if ((!User.IsInRole("admin")) && treatment.Animal.UserProfile.UserId != userID)
            {
                return HttpNotFound();
            }
            TreatmentViewModel tvm = new TreatmentViewModel();
            tvm.treatment = treatment;
            tvm.animal_name = treatment.Animal.name;
            tvm.animal_tag = treatment.Animal.tag;
            return View(tvm);
        }

        //
        // POST: /treatment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Int32 id, Int32 id2)
        {
            Treatment treatment = db.Treatments.Find(id, id2);
            db.Treatments.Remove(treatment);
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
