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
            List<SelectListItem> breedList = new List<SelectListItem>() {
                new SelectListItem() { Text = "Purbred Boer", Value = "Purbred Boer"},
                new SelectListItem() { Text = "Percentage-87.5 Boer", Value = "Percentage-87.5 Boer"},
                new SelectListItem() { Text = "Percentage-75 Boer", Value = "Percentage-75 Boer"},
                new SelectListItem() { Text = "Spanish", Value = "Spanish"},
                new SelectListItem() { Text = "Nubian", Value = "Nubian"},
                new SelectListItem() { Text = "Boer x Spanish", Value = "Boer x Spanish"},
                new SelectListItem() { Text = "Pure Kiko", Value = "Pure Kiko"},
                new SelectListItem() { Text = "Percentage-87.5 Kiko", Value = "Percentage-87.5 Kiko"},
                new SelectListItem() { Text = "Percentage-75 Kiko", Value = "Percentage-75 Kiko"},
                new SelectListItem() { Text = "Kiko x Spanish", Value = "Kiko x Spanish"},
                new SelectListItem() { Text = "Kiko x Boer", Value = "Kiko x Boer"},
                new SelectListItem() { Text = "Boer X Nubian", Value = "Boer X Nubian"},
                new SelectListItem() { Text = "Boer x Spanish x Nubian", Value = "Boer x Spanish x Nubian"},
                new SelectListItem() { Text = "Savanna", Value = "Savanna"},
                new SelectListItem() { Text = "Percentag-87.5 Savanna", Value = "Percentag-87.5 Savanna"},
                new SelectListItem() { Text = "Percentage-75 Savanna", Value = "Percentage-75 Savanna"},
                new SelectListItem() { Text = "Savanna x Boer", Value = "Savanna x Boer"},
                new SelectListItem() { Text = "Savanna x Kiko", Value = "Savanna x Kiko"},
                new SelectListItem() { Text = "Nubian x Spanish", Value = "Nubian x Spanish"},
                new SelectListItem() { Text = "Savanna x Boer x Kiko", Value = "Savanna x Boer x Kiko"},
                new SelectListItem() { Text = "Angora", Value = "Angora"},
                new SelectListItem() { Text = "Boer x Angora", Value = "Boer x Angora"},
                new SelectListItem() { Text = "Kiko x Angora", Value = "Kiko x Angora"},
                new SelectListItem() { Text = "UNKNOWN CROSS", Value = "UNKNOWN CROSS"},
                new SelectListItem() { Text = "OTHERS", Value = "OTHERS"},
            };
            @ViewBag.breedList = breedList;
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
            List<SelectListItem> breedList = new List<SelectListItem>() {
                new SelectListItem() { Text = "Purbred Boer", Value = "Purbred Boer"},
                new SelectListItem() { Text = "Percentage-87.5 Boer", Value = "Percentage-87.5 Boer"},
                new SelectListItem() { Text = "Percentage-75 Boer", Value = "Percentage-75 Boer"},
                new SelectListItem() { Text = "Spanish", Value = "Spanish"},
                new SelectListItem() { Text = "Nubian", Value = "Nubian"},
                new SelectListItem() { Text = "Boer x Spanish", Value = "Boer x Spanish"},
                new SelectListItem() { Text = "Pure Kiko", Value = "Pure Kiko"},
                new SelectListItem() { Text = "Percentage-87.5 Kiko", Value = "Percentage-87.5 Kiko"},
                new SelectListItem() { Text = "Percentage-75 Kiko", Value = "Percentage-75 Kiko"},
                new SelectListItem() { Text = "Kiko x Spanish", Value = "Kiko x Spanish"},
                new SelectListItem() { Text = "Kiko x Boer", Value = "Kiko x Boer"},
                new SelectListItem() { Text = "Boer X Nubian", Value = "Boer X Nubian"},
                new SelectListItem() { Text = "Boer x Spanish x Nubian", Value = "Boer x Spanish x Nubian"},
                new SelectListItem() { Text = "Savanna", Value = "Savanna"},
                new SelectListItem() { Text = "Percentag-87.5 Savanna", Value = "Percentag-87.5 Savanna"},
                new SelectListItem() { Text = "Percentage-75 Savanna", Value = "Percentage-75 Savanna"},
                new SelectListItem() { Text = "Savanna x Boer", Value = "Savanna x Boer"},
                new SelectListItem() { Text = "Savanna x Kiko", Value = "Savanna x Kiko"},
                new SelectListItem() { Text = "Nubian x Spanish", Value = "Nubian x Spanish"},
                new SelectListItem() { Text = "Savanna x Boer x Kiko", Value = "Savanna x Boer x Kiko"},
                new SelectListItem() { Text = "Angora", Value = "Angora"},
                new SelectListItem() { Text = "Boer x Angora", Value = "Boer x Angora"},
                new SelectListItem() { Text = "Kiko x Angora", Value = "Kiko x Angora"},
                new SelectListItem() { Text = "UNKNOWN CROSS", Value = "UNKNOWN CROSS"},
                new SelectListItem() { Text = "OTHERS", Value = "OTHERS"},
            };
            @ViewBag.breedList = breedList;
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
            List<SelectListItem> breedList = new List<SelectListItem>() {
                new SelectListItem() { Text = "Purbred Boer", Value = "Purbred Boer"},
                new SelectListItem() { Text = "Percentage-87.5 Boer", Value = "Percentage-87.5 Boer"},
                new SelectListItem() { Text = "Percentage-75 Boer", Value = "Percentage-75 Boer"},
                new SelectListItem() { Text = "Spanish", Value = "Spanish"},
                new SelectListItem() { Text = "Nubian", Value = "Nubian"},
                new SelectListItem() { Text = "Boer x Spanish", Value = "Boer x Spanish"},
                new SelectListItem() { Text = "Pure Kiko", Value = "Pure Kiko"},
                new SelectListItem() { Text = "Percentage-87.5 Kiko", Value = "Percentage-87.5 Kiko"},
                new SelectListItem() { Text = "Percentage-75 Kiko", Value = "Percentage-75 Kiko"},
                new SelectListItem() { Text = "Kiko x Spanish", Value = "Kiko x Spanish"},
                new SelectListItem() { Text = "Kiko x Boer", Value = "Kiko x Boer"},
                new SelectListItem() { Text = "Boer X Nubian", Value = "Boer X Nubian"},
                new SelectListItem() { Text = "Boer x Spanish x Nubian", Value = "Boer x Spanish x Nubian"},
                new SelectListItem() { Text = "Savanna", Value = "Savanna"},
                new SelectListItem() { Text = "Percentag-87.5 Savanna", Value = "Percentag-87.5 Savanna"},
                new SelectListItem() { Text = "Percentage-75 Savanna", Value = "Percentage-75 Savanna"},
                new SelectListItem() { Text = "Savanna x Boer", Value = "Savanna x Boer"},
                new SelectListItem() { Text = "Savanna x Kiko", Value = "Savanna x Kiko"},
                new SelectListItem() { Text = "Nubian x Spanish", Value = "Nubian x Spanish"},
                new SelectListItem() { Text = "Savanna x Boer x Kiko", Value = "Savanna x Boer x Kiko"},
                new SelectListItem() { Text = "Angora", Value = "Angora"},
                new SelectListItem() { Text = "Boer x Angora", Value = "Boer x Angora"},
                new SelectListItem() { Text = "Kiko x Angora", Value = "Kiko x Angora"},
                new SelectListItem() { Text = "UNKNOWN CROSS", Value = "UNKNOWN CROSS"},
                new SelectListItem() { Text = "OTHERS", Value = "OTHERS"},
            };
            @ViewBag.breedList = breedList;
            return View(animal);
        }

        //
        // POST: /Animal/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Animal animal)
        {
            if (ModelState.IsValid)
            {
                animal.owner = (int)Membership.GetUser().ProviderUserKey;
                db.Entry(animal).State = EntityState.Modified;
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
            ViewBag.owner = new SelectList(db.UserProfiles, "UserId", "Username", animal.owner);
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
            List<SelectListItem> breedList = new List<SelectListItem>() {
                new SelectListItem() { Text = "Purbred Boer", Value = "Purbred Boer"},
                new SelectListItem() { Text = "Percentage-87.5 Boer", Value = "Percentage-87.5 Boer"},
                new SelectListItem() { Text = "Percentage-75 Boer", Value = "Percentage-75 Boer"},
                new SelectListItem() { Text = "Spanish", Value = "Spanish"},
                new SelectListItem() { Text = "Nubian", Value = "Nubian"},
                new SelectListItem() { Text = "Boer x Spanish", Value = "Boer x Spanish"},
                new SelectListItem() { Text = "Pure Kiko", Value = "Pure Kiko"},
                new SelectListItem() { Text = "Percentage-87.5 Kiko", Value = "Percentage-87.5 Kiko"},
                new SelectListItem() { Text = "Percentage-75 Kiko", Value = "Percentage-75 Kiko"},
                new SelectListItem() { Text = "Kiko x Spanish", Value = "Kiko x Spanish"},
                new SelectListItem() { Text = "Kiko x Boer", Value = "Kiko x Boer"},
                new SelectListItem() { Text = "Boer X Nubian", Value = "Boer X Nubian"},
                new SelectListItem() { Text = "Boer x Spanish x Nubian", Value = "Boer x Spanish x Nubian"},
                new SelectListItem() { Text = "Savanna", Value = "Savanna"},
                new SelectListItem() { Text = "Percentag-87.5 Savanna", Value = "Percentag-87.5 Savanna"},
                new SelectListItem() { Text = "Percentage-75 Savanna", Value = "Percentage-75 Savanna"},
                new SelectListItem() { Text = "Savanna x Boer", Value = "Savanna x Boer"},
                new SelectListItem() { Text = "Savanna x Kiko", Value = "Savanna x Kiko"},
                new SelectListItem() { Text = "Nubian x Spanish", Value = "Nubian x Spanish"},
                new SelectListItem() { Text = "Savanna x Boer x Kiko", Value = "Savanna x Boer x Kiko"},
                new SelectListItem() { Text = "Angora", Value = "Angora"},
                new SelectListItem() { Text = "Boer x Angora", Value = "Boer x Angora"},
                new SelectListItem() { Text = "Kiko x Angora", Value = "Kiko x Angora"},
                new SelectListItem() { Text = "UNKNOWN CROSS", Value = "UNKNOWN CROSS"},
                new SelectListItem() { Text = "OTHERS", Value = "OTHERS"},
            };
            @ViewBag.breedList = breedList;
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
            var treatments = db.Treatments.Include(a => a.Animal.UserProfile).Where(b => b.animal_id == animal.id);
            var breedings = db.Breedings.Include(a => a.Animal.UserProfile).Where(b => b.father_id == animal.id || b.mother_id == animal.id);
            foreach(Treatment tre in treatments)
            {
                db.Treatments.Remove(tre);
            }
            foreach(Breeding bre in breedings)
            {
                var births = db.Births.Include(a => a.Animal.UserProfile).Where(b => b.breed_id == bre.id);
                foreach (Birth bi in births)
                {
                    db.Births.Remove(bi);
                }
                db.Breedings.Remove(bre);
            }
            db.Animals.Remove(animal);
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
