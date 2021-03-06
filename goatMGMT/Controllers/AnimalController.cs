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

            List<AnimalViewModel> avmList = new List<AnimalViewModel>();
            int sireCount = 0;
            int damCount = 0;
            int offCount = 0;
            foreach (Animal an in animals)
            {
                AnimalViewModel avm = new AnimalViewModel();
                avm.animal = an;
                avmList.Add(avm);
                if (an.sex && !an.isChild && an.status_code == "Active")
                {
                    sireCount++;
                }
                else if (!an.sex && !an.isChild && an.status_code == "Active")
                {
                    damCount++;
                }
                else if (an.isChild && (an.status_code == "Active" || an.status_code == "Unclassed"))
                {
                    offCount++;
                }
            }
            AnimalViewModel avmFinal = new AnimalViewModel();
            avmFinal.ien = avmList;
            avmFinal.numSires = sireCount;
            avmFinal.numDams = damCount;
            avmFinal.numOff = offCount;

            return View(avmFinal);
        }

        //
        // GET: /Animal/HealthRecordsIndex/5
        public ActionResult HealthRecordsIndex(Int32 id)
        {
            int userID = (int)Membership.GetUser().ProviderUserKey;
            Animal animal = db.Animals.Find(id);
            if (animal == null)
            {
                return HttpNotFound();
            }
            else if ((!User.IsInRole("admin")) && animal.UserProfile.UserId != userID)
            {
                return HttpNotFound();
            }
            return View(animal);
        }

        //
        // GET: /Animal/HealthRecordsUpdate/5
        public ActionResult HealthRecordsUpdate(Int32 id)
        {
            int userID = (int)Membership.GetUser().ProviderUserKey;
            Animal animal = db.Animals.Find(id);
            if (animal == null)
            {
                return HttpNotFound();
            }
            else if ((!User.IsInRole("admin")) && animal.UserProfile.UserId != userID)
            {
                return HttpNotFound();
            }
            return View(animal);
        }

        //
        // POST: /Animal/HealthRecordsUpdate/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HealthRecordsUpdate(Animal animal)
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
                return RedirectToAction("HealthRecordsIndex", new { id = animal.id });
            }
            return View(animal);
        }

        //
        // GET: /Animal/Details/5
        public ActionResult Details(Int32 id)
        {
            int userID = (int)Membership.GetUser().ProviderUserKey;
            Animal animal = db.Animals.Find(id);
            if (animal == null)
            {
                return HttpNotFound();
            }
            else if ((!User.IsInRole("admin")) && animal.UserProfile.UserId != userID)
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
                new SelectListItem() { Text = "Other", Value = "Other"}
            };
            @ViewBag.speciesList = speciesList;
            List<SelectListItem> statusList = new List<SelectListItem>() {
                new SelectListItem() { Text = "Active", Value = "Active"},
                new SelectListItem() { Text = "Sold for meat", Value = "Sold for meat"},
                new SelectListItem() { Text = "Sold for breeding", Value = "Sold for breeding"},
                new SelectListItem() { Text = "Died", Value = "Died"},
                new SelectListItem() { Text = "Culled", Value = "Culled"},
                new SelectListItem() { Text = "Unclassed (for offspring)", Value = "Unclassed"},
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
            List<SelectListItem> speciesList = new List<SelectListItem>() {
                new SelectListItem() { Text= "Goat (Meat)", Value = "Goat (Meat)"},
                new SelectListItem() { Text = "Other", Value = "Other"}
            };
            @ViewBag.speciesList = speciesList;
            List<SelectListItem> statusList = new List<SelectListItem>() {
                new SelectListItem() { Text = "Active", Value = "Active"},
                new SelectListItem() { Text = "Sold for meat", Value = "Sold for meat"},
                new SelectListItem() { Text = "Sold for breeding", Value = "Sold for breeding"},
                new SelectListItem() { Text = "Died", Value = "Died"},
                new SelectListItem() { Text = "Culled", Value = "Culled"},
                new SelectListItem() { Text = "Unclassed (for offspring)", Value = "Unclassed"},
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
            if (ModelState.IsValid)
            {
                if (animal.weaning_date != null)
                {
                    if (animal.dob.CompareTo(animal.weaning_date) > 0)
                    {
                        ModelState.AddModelError("", "Weaning Date must be after Date of Birth");
                        return View(animal);
                    }
                }
                if (animal.post_weaning_date != null && animal.weaning_date != null)
                {
                    if (((DateTime)animal.post_weaning_date).CompareTo((DateTime)animal.weaning_date) < 0)
                    {
                        ModelState.AddModelError("", "Post-Weaning Date must be after Weaning Date");
                        return View(animal);
                    }
                }
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
            return View(animal);
        }

        public ActionResult Compare()
        {
            int userID = (int)Membership.GetUser().ProviderUserKey;
            var myAnimals = db.Animals.Where(m => m.owner == userID).ToList();
            double bw = 0;
            int bwcount = 0;
            double bwall = 0;
            int bwallcount = 0;
            double ww = 0;
            int wwcount = 0;
            double wwall = 0;
            int wwallcount = 0;
            double pw = 0;
            int pwcount = 0;
            double pwall = 0;
            int pwallcount = 0;
            foreach (Animal animal in myAnimals)
            {
                if (animal.birth_weight != null)
                {
                    bw += (double)animal.birth_weight;
                    bwcount++;
                }
                if (animal.weaning_weight != null)
                {
                    ww += (double)animal.weaning_weight;
                    wwcount++;
                }
                if (animal.post_weaning_weight != null)
                {
                    pw += (double)animal.post_weaning_weight;
                    pwcount++;
                }
            }
            foreach (Animal animal in db.Animals)
            {
                if (animal.birth_weight != null)
                {
                    bwall += (double)animal.birth_weight;
                    bwallcount++;
                }
                if (animal.weaning_weight != null)
                {
                    wwall += (double)animal.weaning_weight;
                    wwallcount++;
                }
                if (animal.post_weaning_weight != null)
                {
                    pwall += (double)animal.post_weaning_weight;
                    pwallcount++;
                }
            }

            if (bwcount == 0)
            {
                bwcount++;
            }
            double temp1 = bw / bwcount;
            temp1 = temp1 * 1000;
            temp1 = Math.Truncate(temp1);
            temp1 = temp1 / 1000;

            if (bwallcount == 0)
            {
                bwallcount++;
            }
            double temp2 = bwall / bwallcount;
            temp2 = temp2 * 1000;
            temp2 = Math.Truncate(temp2);
            temp2 = temp2 / 1000;

            if (wwcount == 0)
            {
                wwcount++;
            }
            double temp3 = ww / wwcount;
            temp3 = temp3 * 1000;
            temp3 = Math.Truncate(temp3);
            temp3 = temp3 / 1000;

            if (wwallcount == 0)
            {
                wwallcount++;
            }
            double temp4 = wwall / wwallcount;
            temp4 = temp4 * 1000;
            temp4 = Math.Truncate(temp4);
            temp4 = temp4 / 1000;

            if (pwcount == 0)
            {
                pwcount++;
            }
            double temp5 = pw / pwcount;
            temp5 = temp5 * 1000;
            temp5 = Math.Truncate(temp5);
            temp5 = temp5 / 1000;

            if (pwallcount == 0)
            {
                pwallcount++;
            }
            double temp6 = pwall / pwallcount;
            temp6 = temp6 * 1000;
            temp6 = Math.Truncate(temp6);
            temp6 = temp6 / 1000;
            GraphViewModel gvm = new GraphViewModel()
            {
                birthweight = temp1,
                birthweightall = temp2,
                weaningweight = temp3,
                weaningweightall = temp4,
                postweaningweight = temp5,
                postweaningweightall = temp6
            };
            return View(gvm);
        }

        public ActionResult Offspring()
        {
            int userID = (int)Membership.GetUser().ProviderUserKey;
            List<Kid> kids = new List<Kid>();
            var animalList = db.Animals.Where(m => m.owner == userID);
            if (User.IsInRole("admin"))
            {
                animalList = db.Animals;
            }
            foreach (Animal currentAnimal in animalList)
            {
                if (currentAnimal.birth_weight != null && ((currentAnimal.weaning_date != null && currentAnimal.weaning_weight != null) || (currentAnimal.post_weaning_date != null && currentAnimal.post_weaning_weight != null)))
                {
                    Kid kid = new Kid()
                    {
                        animal = currentAnimal
                    };
                    if (currentAnimal.weaning_date != null && currentAnimal.weaning_weight != null)
                    {
                        kid.ageAtWeaning = ((DateTime)currentAnimal.weaning_date - currentAnimal.dob).Days;
                        if (kid.ageAtWeaning == 0)
                        {
                            kid.ageAtWeaning = 1;
                        }
                        double temp1 = (double)(currentAnimal.weaning_weight - currentAnimal.birth_weight) / kid.ageAtWeaning;
                        temp1 = temp1 * 1000;
                        temp1 = Math.Truncate(temp1);
                        temp1 = temp1 / 1000;
                        kid.averageDailyGainWeaning = temp1;
                    }
                    else
                    {
                        kid.averageDailyGainWeaning = -1;
                        kid.ageAtWeaning = -1;
                    }
                    if (currentAnimal.post_weaning_date != null && currentAnimal.post_weaning_weight != null && currentAnimal.weaning_date != null)
                    {
                        kid.ageAtPostWeaning = ((DateTime)currentAnimal.post_weaning_date - ((DateTime)currentAnimal.weaning_date)).Days;
                        if (kid.ageAtPostWeaning == 0)
                        {
                            kid.ageAtPostWeaning = 1;
                        }
                        double temp2 = (double)(currentAnimal.post_weaning_weight - currentAnimal.weaning_weight) / kid.ageAtPostWeaning;
                        temp2 = temp2 * 1000;
                        temp2 = Math.Truncate(temp2);
                        temp2 = temp2 / 1000;
                        kid.averageDailyGainPostWeaning = temp2;
                    }
                    else
                    {
                        kid.ageAtPostWeaning = -1;
                        kid.averageDailyGainPostWeaning = -1;
                    }
                    kids.Add(kid);
                }
            }
            return View(kids);
        }

        //
        // GET: /Animal/Edit/5
        public ActionResult Edit(Int32 id)
        {
            int userID = (int)Membership.GetUser().ProviderUserKey;
            Animal animal = db.Animals.Find(id);
            if (animal == null)
            {
                return HttpNotFound();
            }
            else if ((!User.IsInRole("admin")) && animal.UserProfile.UserId != userID)
            {
                return HttpNotFound();
            }
            List<SelectListItem> statusList = new List<SelectListItem>() {
                new SelectListItem() { Text = "Active", Value = "Active"},
                new SelectListItem() { Text = "Sold for meat", Value = "Sold for meat"},
                new SelectListItem() { Text = "Sold for breeding", Value = "Sold for breeding"},
                new SelectListItem() { Text = "Died", Value = "Died"},
                new SelectListItem() { Text = "Culled", Value = "Culled"},
                new SelectListItem() { Text = "Unclassed (for offspring)", Value = "Unclassed"},
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
            ViewBag.owner = new SelectList(db.UserProfiles, "UserId", "Username", animal.owner);
            List<SelectListItem> statusList = new List<SelectListItem>() {
                new SelectListItem() { Text = "Active", Value = "Active"},
                new SelectListItem() { Text = "Sold for meat", Value = "Sold for meat"},
                new SelectListItem() { Text = "Sold for breeding", Value = "Sold for breeding"},
                new SelectListItem() { Text = "Died", Value = "Died"},
                new SelectListItem() { Text = "Culled", Value = "Culled"},
                new SelectListItem() { Text = "Unclassed (for offspring)", Value = "Unclassed"},
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
            if (ModelState.IsValid)
            {
                if (animal.weaning_date != null)
                {
                    if (animal.dob.CompareTo(animal.weaning_date) > 0)
                    {
                        ModelState.AddModelError("", "Weaning Date must be after Date of Birth");
                        return View(animal);
                    }
                }
                if (animal.post_weaning_date != null && animal.weaning_date != null)
                {
                    if (((DateTime)animal.post_weaning_date).CompareTo((DateTime)animal.weaning_date) < 0)
                    {
                        ModelState.AddModelError("", "Post-Weaning Date must be after Weaning Date");
                        return View(animal);
                    }
                }
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
            return View(animal);
        }

        //
        // GET: /Animal/Delete/5
        public ActionResult Delete(Int32 id)
        {
            int userID = (int)Membership.GetUser().ProviderUserKey;
            Animal animal = db.Animals.Find(id);
            if (animal == null)
            {
                return HttpNotFound();
            }
            else if ((!User.IsInRole("admin")) && animal.UserProfile.UserId != userID)
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
            foreach (Treatment tre in treatments)
            {
                db.Treatments.Remove(tre);
            }
            foreach (Breeding bre in breedings)
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
