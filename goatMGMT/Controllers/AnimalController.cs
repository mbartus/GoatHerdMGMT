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
    public class AnimalController : Controller
    {
        private goatDBEntities db = new goatDBEntities();

        //
        // GET: /Animal/
        public ActionResult Index()
        {
            int userID = (int)Membership.GetUser().ProviderUserKey;
            var animals = db.Animals.Include(a => a.UserProfile).Where(m => m.owner == userID).ToList();
            if (User.IsInRole("admin"))
            {
                animals = db.Animals.ToList();
            }
            return View(animals);
        }

        //
        // GET: /Animal/Details/5
        public ActionResult Details(Int32 id)
        {
            Animal animal = db.Animals.Find(id);
            if (animal == null)
            {
                return HttpNotFound();
            }
            return View(animal);
        }

        //
        // GET: /Animal/Create
        public ActionResult Create()
        {
            List<SelectListItem> speciesList = new List<SelectListItem>() {
                new SelectListItem() { Text= "Goat (Meat)", Value = "Goat (Meat)"},
                new SelectListItem() { Text= "Goat (Milk)", Value = "Goat (Milk)"},
                new SelectListItem() { Text = "Sheep", Value = "Sheep"},
                new SelectListItem() { Text = "Other", Value = "Other"}
            };
            @ViewBag.speciesList = speciesList;
            List<SelectListItem> statusList = new List<SelectListItem>() {
                new SelectListItem() { Text = "Active", Value = "Active"},
                new SelectListItem() { Text = "Sold for meat", Value = "Sold for meat"},
                new SelectListItem() { Text = "Sold for breeding", Value = "Sold for breeding"},
                new SelectListItem() { Text = "Died", Value = "Died"},
                new SelectListItem() { Text = "Culled", Value = "Culled"},
                new SelectListItem() { Text = "Unclassed", Value = "Unclassed"},
                new SelectListItem() { Text = "Herd Replacement", Value = "Herd Replacement"}
            };
            
            @ViewBag.statusList = statusList;
            return View();
        }

        //
        // POST: /Animal/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Animal animal)
        {

            if (ModelState.IsValid)
            {
                animal.owner = (int)Membership.GetUser().ProviderUserKey;
                db.Animals.Add(animal);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(animal);
        }

        //
        // GET: /Animal/Edit/5
        public ActionResult Edit(Int32 id)
        {
            Animal animal = db.Animals.Find(id);
            if (animal == null)
            {
                return HttpNotFound();
            }
            List<SelectListItem> statusList = new List<SelectListItem>() {
                new SelectListItem() { Text = "Active", Value = "Active"},
                new SelectListItem() { Text = "Sold for meat", Value = "Sold for meat"},
                new SelectListItem() { Text = "Sold for breeding", Value = "Sold for breeding"},
                new SelectListItem() { Text = "Died", Value = "Died"},
                new SelectListItem() { Text = "Culled", Value = "Culled"},
                new SelectListItem() { Text = "Unclassed", Value = "Unclassed"},
                new SelectListItem() { Text = "Herd Replacement", Value = "Herd Replacement"}
            };
            @ViewBag.statusList = statusList;
            return View(animal);
        }

        //
        // POST: /Animal/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Animal animal, bool sex2)
        {
            if (ModelState.IsValid)
            {
                if (!sex2 && !animal.sex)
                {
                    ModelState.AddModelError("", "Please select sex.");
                    return View(animal);
                }
                animal.owner = (int)Membership.GetUser().ProviderUserKey;
                db.Entry(animal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.owner = new SelectList(db.UserProfiles, "UserId", "Username", animal.owner);
            return View(animal);
        }

        //
        // GET: /Animal/Delete/5
        public ActionResult Delete(Int32 id)
        {
            Animal animal = db.Animals.Find(id);
            if (animal == null)
            {
                return HttpNotFound();
            }
            return View(animal);
        }

        //
        // POST: /Animal/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Int32 id)
        {
            Animal animal = db.Animals.Find(id);
            db.Animals.Remove(animal);
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
