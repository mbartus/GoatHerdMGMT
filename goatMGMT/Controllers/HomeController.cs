using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using goatMGMT.Models;
using System.Web.Security;
using OfficeOpenXml;

namespace goatMGMT.Controllers
{
    public class HomeController : Controller
    {
        private goatDBEntities db = new goatDBEntities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "What do we do?";

            return View();
        }

        [HttpGet]
        public ActionResult FeedbackConfirm()
        {
            return View();
        }

        [HttpGet] 
        public ActionResult Contact()
        {
            ViewBag.Message = "";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(Comment com)
        {
            if (ModelState.IsValid)
            {
                com.date_sent = System.DateTime.Now;
                db.Comments.Add(com);
                try
                {
                    db.SaveChanges();
                }
                catch
                {
                    return RedirectToAction("Error", "Home");
                }
                return Redirect("~/Home/FeedbackConfirm");
            }
            return View(com);
        }

        public ActionResult Pricing()
        {
            return View();
        }

        [Authorize]
        public ActionResult Dashboard()
        {
            if (User.IsInRole("admin"))
            {
                ViewBag.Title = "Admin Dashboard";
            }
            else
            {
                ViewBag.Title = "Dashboard";
            }
            return View();
        }

        [Authorize(Roles = "admin, user")]
        public ActionResult Summary()
        {
            int userID = (int)Membership.GetUser().ProviderUserKey;
            int maleBWCount = 0;
            int femaleBWCount = 0;
            int maleWWCount = 0;
            int femaleWWCount = 0;
            int allADGWCount = 0;
            int allMaleADGWCount = 0;
            int allFemaleADGWCount = 0;
            int allADGPWCount = 0;
            int allMaleADGPWCount = 0;
            int allFemaleADGPWCount = 0;
            int ADGWCount = 0;
            int MaleADGWCount = 0;
            int FemaleADGWCount = 0;
            int ADGPWCount = 0;
            int MaleADGPWCount = 0;
            int FemaleADGPWCount = 0;
            SummaryViewModel svm = new SummaryViewModel();
            var myAnimalList = db.Animals.Where(m => m.owner == userID).ToList();
            svm.totalSire = db.Animals.Where(m => m.owner == userID && m.sex == false).Count();
            svm.totalDam = db.Animals.Where(m => m.owner == userID && m.sex == true).Count();
            svm.activeSire = db.Animals.Where(m => m.owner == userID && m.sex == false && m.status_code == "Active").Count();
            svm.activeDam = db.Animals.Where(m => m.owner == userID && m.sex == true && m.status_code == "Active").Count();
            svm.currentYear = System.DateTime.Now.Year;
            svm.lastYear = svm.currentYear - 1;
            foreach (Animal animal in myAnimalList)
            {
                if (animal.birth_weight != null && animal.dob != null && (animal.weaning_date != null && animal.weaning_weight != null) || (animal.post_weaning_date != null && animal.post_weaning_weight != null))
                {
                    if (animal.sex)
                    {
                        if (animal.weaning_date != null && animal.weaning_weight != null)
                        {
                            svm.maleADGWeaning = (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                            svm.ADGWeaning = (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                            ADGWCount++;
                            MaleADGWCount++;
                        }
                        if (animal.post_weaning_date != null && animal.post_weaning_weight != null)
                        {
                            svm.maleADGPostWeaning = (double)(animal.post_weaning_weight - animal.birth_weight) / ((DateTime)animal.post_weaning_date - animal.dob).Days;
                            svm.ADGPostWeaning = (double)(animal.post_weaning_weight - animal.birth_weight) / ((DateTime)animal.post_weaning_date - animal.dob).Days;
                            ADGPWCount++;
                            MaleADGPWCount++;
                        }
                    }
                    else
                    {
                        if (animal.weaning_date != null && animal.weaning_weight != null)
                        {
                            svm.femaleADGWeaning = (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                            svm.ADGWeaning = (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                            ADGWCount++;
                            FemaleADGWCount++;
                        }
                        if (animal.post_weaning_date != null && animal.post_weaning_weight != null)
                        {
                            svm.femaleADGPostWeaning = (double)(animal.post_weaning_weight - animal.birth_weight) / ((DateTime)animal.post_weaning_date - animal.dob).Days;
                            svm.ADGPostWeaning = (double)(animal.post_weaning_weight - animal.birth_weight) / ((DateTime)animal.post_weaning_date - animal.dob).Days;
                            ADGPWCount++;
                            FemaleADGPWCount++;
                        }
                    }
                }
                if (animal.weaning_date != null)
                {
                    if (animal.sex)
                    {
                        svm.malesWeaned++;
                        svm.totalWeaned++;
                        if (animal.birth_weight != null)
                        {
                            svm.maleAvgBW += (double)animal.birth_weight;
                            maleBWCount++;
                        }
                        if (animal.weaning_weight != null)
                        {
                            svm.maleAvgWW += (double)animal.weaning_weight;
                            maleWWCount++;
                        }
                    }
                    else
                    {
                        svm.femalesWeaned++;
                        svm.totalWeaned++;
                        if (animal.birth_weight != null)
                        {
                            svm.femaleAvgBW += (double)animal.birth_weight;
                            femaleBWCount++;
                        }
                        if (animal.weaning_weight != null)
                        {
                            svm.femaleAvgWW += (double)animal.weaning_weight;
                            femaleWWCount++;
                        }
                    }
                }
                foreach (Breeding breeding in db.Breedings.Where(m => m.father_id == animal.id))
                {
                    svm.matingCount++;
                    svm.kiddingCount += breeding.Births.Count();
                    svm.kiddingPercentage = svm.matingCount / svm.kiddingCount;
                    if (breeding.alive == null)
                    {
                        breeding.alive = 0;
                    }
                    if (breeding.born == null)
                    {
                        breeding.born = 0;
                    }
                    svm.kidsAliveCount += breeding.alive == null ? 0 : (int)breeding.alive;
                    svm.kidsBornCount += breeding.born == null ? 0 : (int)breeding.born;
                    if (svm.kidsAliveCount == 0)
                    {
                        svm.kidsAliveCount = 1;
                    }
                    svm.kidsAlivePercentage = svm.kidsBornCount / svm.kidsAliveCount;
                    foreach (Birth birth in breeding.Births)
                    {
                        if (birth.parity == 1)
                        {
                            if (db.Animals.Find(birth.child_id).birth_weight != null)
                            {
                                svm.damParity1Count++;
                                svm.damParity1BW += (double)db.Animals.Find(birth.child_id).birth_weight;
                            }
                        }
                        else if (birth.parity == 2)
                        {
                            if (db.Animals.Find(birth.child_id).birth_weight != null)
                            {
                                svm.damParity2Count++;
                                svm.damParity2BW += (double)db.Animals.Find(birth.child_id).birth_weight;
                            }
                        }
                        else if (birth.parity == 3)
                        {
                            if (db.Animals.Find(birth.child_id).birth_weight != null)
                            {
                                svm.damParity3Count++;
                                svm.damParity3BW += (double)db.Animals.Find(birth.child_id).birth_weight;
                            }
                        }
                        else if (birth.parity > 3)
                        {
                            if (db.Animals.Find(birth.child_id).birth_weight != null)
                            {
                                svm.damParity4Count++;
                                svm.damParity4BW += (double)db.Animals.Find(birth.child_id).birth_weight;
                            }
                        }
                        if (db.Animals.Find(birth.child_id).sex)
                        {
                            svm.malesBorn++;
                            svm.totalBorn++;
                        }
                        if (!db.Animals.Find(birth.child_id).sex)
                        {
                            svm.femalesBorn++;
                            svm.totalBorn++;
                        }
                        if ((int)birth.Breeding.born == 1)
                        {
                            svm.singleBirthCount++;
                            if (db.Animals.Find(birth.child_id).birth_weight != null)
                            {
                                svm.singleBWAvg += (double)db.Animals.Find(birth.child_id).birth_weight;
                            }
                            if (db.Animals.Find(birth.child_id).weaning_date != null)
                            {
                                svm.singleWWAvg += (double)db.Animals.Find(birth.child_id).weaning_weight;
                            }
                        }
                        if ((int)birth.Breeding.born == 2)
                        {
                            svm.twinBirthCount++;
                            if (db.Animals.Find(birth.child_id).birth_weight != null)
                            {
                                svm.twinBWAvg += (double)db.Animals.Find(birth.child_id).birth_weight;
                            }
                            if (db.Animals.Find(birth.child_id).weaning_date != null)
                            {
                                svm.twinWWAvg += (double)db.Animals.Find(birth.child_id).weaning_weight;
                            }
                        }
                        if ((int)birth.Breeding.born == 3)
                        {
                            svm.tripletBirthCount++;
                            if (db.Animals.Find(birth.child_id).birth_weight != null)
                            {
                                svm.tripletBWAvg += (double)db.Animals.Find(birth.child_id).birth_weight;
                            }
                            if (db.Animals.Find(birth.child_id).weaning_date != null)
                            {
                                svm.tripletWWAvg += (double)db.Animals.Find(birth.child_id).weaning_weight;
                            }
                        }
                        if ((int)birth.Breeding.born > 3)
                        {
                            svm.quadBirthCount++;
                            if (db.Animals.Find(birth.child_id).birth_weight != null)
                            {
                                svm.quadBWAvg += (double)db.Animals.Find(birth.child_id).birth_weight;
                            }
                            if (db.Animals.Find(birth.child_id).weaning_date != null)
                            {
                                svm.quadWWAvg += (double)db.Animals.Find(birth.child_id).weaning_weight;
                            }
                        }
                    }
                }
            }
            foreach (Animal a in db.Animals)
            {
                if (a.birth_weight != null && a.dob != null && (a.weaning_date != null && a.weaning_weight != null) || (a.post_weaning_date != null && a.post_weaning_weight != null))
                {
                    if (a.sex)
                    {
                        if (a.weaning_date != null && a.weaning_weight != null)
                        {
                            svm.allMaleADGWeaning = (double)(a.weaning_weight - a.birth_weight) / ((DateTime)a.weaning_date - a.dob).Days;
                            svm.allADGWeaning = (double)(a.weaning_weight - a.birth_weight) / ((DateTime)a.weaning_date - a.dob).Days;
                            allADGWCount++;
                            allMaleADGWCount++;
                        }
                        if (a.post_weaning_date != null && a.post_weaning_weight != null)
                        {
                            svm.allMaleADGPostWeaning = (double)(a.post_weaning_weight - a.birth_weight) / ((DateTime)a.post_weaning_date - a.dob).Days;
                            svm.allADGPostWeaning = (double)(a.post_weaning_weight - a.birth_weight) / ((DateTime)a.post_weaning_date - a.dob).Days;
                            allADGPWCount++;
                            allMaleADGPWCount++;
                        }
                    }
                    else
                    {
                        if (a.weaning_date != null && a.weaning_weight != null)
                        {
                            svm.allFemaleADGWeaning = (double)(a.weaning_weight - a.birth_weight) / ((DateTime)a.weaning_date - a.dob).Days;
                            svm.allADGWeaning = (double)(a.weaning_weight - a.birth_weight) / ((DateTime)a.weaning_date - a.dob).Days;
                            allADGWCount++;
                            allFemaleADGWCount++;
                        }
                        if (a.post_weaning_date != null && a.post_weaning_weight != null)
                        {
                            svm.allFemaleADGPostWeaning = (double)(a.post_weaning_weight - a.birth_weight) / ((DateTime)a.post_weaning_date - a.dob).Days;
                            svm.allADGPostWeaning = (double)(a.post_weaning_weight - a.birth_weight) / ((DateTime)a.post_weaning_date - a.dob).Days;
                            allADGPWCount++;
                            allFemaleADGPWCount++;
                        }
                    }
                }
            }
            svm.maleAvgBW = svm.maleAvgBW / maleBWCount;
            svm.femaleAvgBW = svm.femaleAvgBW / femaleBWCount;
            svm.maleAvgWW = svm.maleAvgWW / maleWWCount;
            svm.femaleAvgWW = svm.femaleAvgWW / femaleWWCount;
            svm.avgBW = (svm.maleAvgBW + svm.femaleAvgBW) / 2;
            svm.avgWW = (svm.maleAvgWW + svm.femaleAvgWW) / 2;
            svm.singleBWAvg = svm.singleBWAvg / svm.singleBirthCount;
            svm.singleWWAvg = svm.singleWWAvg / svm.singleBirthCount;
            svm.twinBWAvg = svm.twinBWAvg / svm.twinBirthCount;
            svm.twinWWAvg = svm.twinWWAvg / svm.twinBirthCount;
            svm.tripletBWAvg = svm.tripletBWAvg / svm.tripletBirthCount;
            svm.tripletWWAvg = svm.tripletWWAvg / svm.tripletBirthCount;
            svm.quadBWAvg = svm.quadBWAvg / svm.quadBirthCount;
            svm.quadWWAvg = svm.quadWWAvg / svm.quadBirthCount;
            svm.damParity1BW = svm.damParity1BW / svm.damParity1Count;
            svm.damParity2BW = svm.damParity2BW / svm.damParity2Count;
            svm.damParity3BW = svm.damParity3BW / svm.damParity3Count;
            svm.damParity4BW = svm.damParity4BW / svm.damParity4Count;
            svm.allADGWeaning = svm.allADGWeaning / allADGWCount;
            svm.allADGPostWeaning = svm.allADGPostWeaning / allADGPWCount;
            svm.allMaleADGWeaning = svm.allMaleADGWeaning / allMaleADGWCount;
            svm.allMaleADGPostWeaning = svm.allMaleADGPostWeaning / allMaleADGPWCount;
            svm.allFemaleADGWeaning = svm.allFemaleADGWeaning / allFemaleADGWCount;
            svm.allFemaleADGPostWeaning = svm.allFemaleADGPostWeaning / allFemaleADGPWCount;
            svm.ADGWeaning = svm.ADGWeaning / ADGWCount;
            svm.ADGPostWeaning = svm.ADGPostWeaning / ADGPWCount;
            svm.maleADGWeaning = svm.maleADGWeaning / MaleADGWCount;
            svm.maleADGPostWeaning = svm.maleADGPostWeaning / MaleADGPWCount;
            svm.femaleADGWeaning = svm.femaleADGWeaning / FemaleADGWCount;
            svm.femaleADGPostWeaning = svm.femaleADGPostWeaning / FemaleADGPWCount;
            return View(svm);
        }

        [Authorize(Roles="admin, user")]
        public ActionResult Export()
        {
            ExcelPackage package = new ExcelPackage();
            int userID = (int)Membership.GetUser().ProviderUserKey;
            //Create Animals
            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Animals");
            worksheet.Cells[1, 1].Value = "Name";
            worksheet.Cells[1, 2].Value = "Tag";
            worksheet.Cells[1, 3].Value = "Date of Birth";
            worksheet.Cells[1, 4].Value = "Sex";
            worksheet.Cells[1, 5].Value = "Breed Code";
            worksheet.Cells[1, 6].Value = "Species";
            worksheet.Cells[1, 7].Value = "Status";
            worksheet.Cells[1, 8].Value = "Child";
            worksheet.Cells[1, 9].Value = "Regulation Number";
            worksheet.Cells[1, 10].Value = "Microchip ID";
            worksheet.Cells[1, 11].Value = "Premise ID";
            worksheet.Cells[1, 12].Value = "Herd ID";
            worksheet.Cells[1, 13].Value = "Breed Registry";
            worksheet.Cells[1, 14].Value = "Weight at Birth";
            worksheet.Cells[1, 15].Value = "Weight at Weaning";
            worksheet.Cells[1, 16].Value = "Weaning Date";
            worksheet.Cells[1, 17].Value = "Post-Weaning Weight";
            worksheet.Cells[1, 18].Value = "Post-Weaning Date";
            worksheet.Cells[1, 19].Value = "Market Weight";
            worksheet.Cells[1, 20].Value = "Market Date";
            worksheet.Cells[1, 21].Value = "Disposal Date";
            worksheet.Cells[1, 22].Value = "Comments";

            // populate a row for each animal
            List<Animal> animals = db.Animals.Where(animal => animal.owner == userID).ToList();
            int row = 2;
            foreach (Animal animal in animals)
            {
                worksheet.Cells[row, 1].Value = animal.name;
                worksheet.Cells[row, 2].Value = animal.tag;
                worksheet.Cells[row, 3].Value = animal.dob.ToShortDateString();
                if (animal.sex) {
                    worksheet.Cells[row, 4].Value = "Male";
                }
                else {
                    worksheet.Cells[row, 4].Value = "Female";
                }
                worksheet.Cells[row, 5].Value = animal.breed_code;
                worksheet.Cells[row, 6].Value = animal.species;
                worksheet.Cells[row, 7].Value = animal.status_code;
                worksheet.Cells[row, 8].Value = animal.isChild;
                worksheet.Cells[row, 9].Value = animal.regulation_no;
                worksheet.Cells[row, 10].Value = animal.microchip_id;
                worksheet.Cells[row, 11].Value = animal.premise_id;
                worksheet.Cells[row, 12].Value = animal.herd_id_code;
                worksheet.Cells[row, 13].Value = animal.breed_registry;
                worksheet.Cells[row, 14].Value = animal.birth_weight;
                worksheet.Cells[row, 15].Value = animal.weaning_weight;
                worksheet.Cells[row, 16].Value = animal.weaning_date == null ? "" : ((DateTime)animal.weaning_date).ToShortDateString();
                worksheet.Cells[row, 17].Value = animal.post_weaning_weight;
                worksheet.Cells[row, 18].Value = animal.post_weaning_date == null ? "" : ((DateTime)animal.post_weaning_date).ToShortDateString();
                worksheet.Cells[row, 19].Value = animal.market_weight;
                worksheet.Cells[row, 20].Value = animal.market_date == null ? "" : ((DateTime)animal.market_date).ToShortDateString();
                worksheet.Cells[row, 21].Value = animal.disposal_date == null ? "" : ((DateTime)animal.disposal_date).ToShortDateString();
                worksheet.Cells[row, 22].Value = animal.remarks;
                row++;
            }
            worksheet.Cells.AutoFitColumns(0);

            //create Treatments
            worksheet = package.Workbook.Worksheets.Add("Health_Records");
            worksheet.Cells[1, 1].Value = "Animal's Tag";
            worksheet.Cells[1, 2].Value = "Animal's Name";
            worksheet.Cells[1, 3].Value = "Date of Treatment";
            worksheet.Cells[1, 4].Value = "Type of Treatment";
            worksheet.Cells[1, 5].Value = "Dosage";
            worksheet.Cells[1, 6].Value = "Product";
            worksheet.Cells[1, 7].Value = "Remarks";

            List<Treatment> treatments = db.Treatments.Where(treatment => treatment.Animal.owner == userID).ToList();
            row = 2;
            foreach (Treatment treatment in treatments)
            {
                worksheet.Cells[row, 1].Value = treatment.Animal.tag;
                worksheet.Cells[row, 2].Value = treatment.Animal.name;
                worksheet.Cells[row, 3].Value = treatment.date == null ? "" : ((DateTime)treatment.date).ToShortDateString();
                worksheet.Cells[row, 4].Value = treatment.item_type;
                worksheet.Cells[row, 5].Value = treatment.dosage;
                worksheet.Cells[row, 6].Value = treatment.product;
                worksheet.Cells[row, 7].Value = treatment.remarks;
                row++;
            }
            worksheet.Cells.AutoFitColumns(0);

            //create Transactions
            worksheet = package.Workbook.Worksheets.Add("Transactions");
            worksheet.Cells[1, 1].Value = "Type";
            worksheet.Cells[1, 2].Value = "Name";
            worksheet.Cells[1, 3].Value = "Date";
            worksheet.Cells[1, 4].Value = "Quantity";
            worksheet.Cells[1, 5].Value = "Unit Price";
            worksheet.Cells[1, 6].Value = "Total";
            worksheet.Cells[1, 7].Value = "Notes";

            List<Transaction> transactions = db.Transactions.Where(transaction => transaction.userid == userID).ToList();
            row = 2;
            foreach (Transaction transaction in transactions)
            {
                if (transaction.type)
                {
                    worksheet.Cells[row, 1].Value = "Income";
                }
                else
                {
                    worksheet.Cells[row, 1].Value = "Expense";
                }
                worksheet.Cells[row, 2].Value = transaction.item_type;
                worksheet.Cells[row, 3].Value = transaction.date == null ? "" : ((DateTime)transaction.date).ToShortDateString();
                worksheet.Cells[row, 4].Value = transaction.quantity;
                worksheet.Cells[row, 5].Value = transaction.unit_price;
                worksheet.Cells[row, 6].Value = transaction.total_payment;
                worksheet.Cells[row, 7].Value = transaction.notes;
                row++;
            }
            worksheet.Cells.AutoFitColumns(0);

            //create Associates
            worksheet = package.Workbook.Worksheets.Add("Associates");
            worksheet.Cells[1, 1].Value = "Name";
            worksheet.Cells[1, 2].Value = "Street";
            worksheet.Cells[1, 3].Value = "City";
            worksheet.Cells[1, 4].Value = "State";
            worksheet.Cells[1, 5].Value = "Zip";
            worksheet.Cells[1, 6].Value = "Phone";
            worksheet.Cells[1, 7].Value = "Fax";
            worksheet.Cells[1, 8].Value = "Email";
            worksheet.Cells[1, 9].Value = "Notes";

            List<Associate> associates = db.Associates.Where(associate => associate.userid == userID).ToList();
            row = 2;
            foreach (Associate associate in associates)
            {
                worksheet.Cells[row, 1].Value = associate.name;
                worksheet.Cells[row, 2].Value = associate.street;
                worksheet.Cells[row, 3].Value = associate.city;
                worksheet.Cells[row, 4].Value = associate.state;
                worksheet.Cells[row, 5].Value = associate.zip;
                worksheet.Cells[row, 6].Value = associate.telephone;
                worksheet.Cells[row, 7].Value = associate.fax;
                worksheet.Cells[row, 8].Value = associate.email;
                worksheet.Cells[row, 9].Value = associate.notes;
                row++;
            }
            worksheet.Cells.AutoFitColumns(0);

            //create Breedings
            worksheet = package.Workbook.Worksheets.Add("Breedings");
            worksheet.Cells[1, 1].Value = "Mother's Tag";
            worksheet.Cells[1, 2].Value = "Father's Tag";
            worksheet.Cells[1, 3].Value = "Breeding Date";
            worksheet.Cells[1, 4].Value = "Pregnancy Check";
            worksheet.Cells[1, 5].Value = "Expected Kidding Date";
            worksheet.Cells[1, 6].Value = "Remarks";

            List<Breeding> breedings = db.Breedings.Where(breeding => breeding.Animal.owner == userID).ToList();
            row = 2;
            foreach (Breeding breeding in breedings)
            {
                worksheet.Cells[row, 1].Value = db.Animals.Find(breeding.mother_id).tag;
                worksheet.Cells[row, 2].Value = db.Animals.Find(breeding.father_id).tag;
                worksheet.Cells[row, 3].Value = breeding.date == null ? "" : ((DateTime)breeding.date).ToShortDateString(); ;
                worksheet.Cells[row, 4].Value = breeding.pregnancy_check;
                worksheet.Cells[row, 5].Value = breeding.expected_kidding_date == null ? "" : ((DateTime)breeding.expected_kidding_date).ToShortDateString(); 
                worksheet.Cells[row, 6].Value = breeding.remarks;
                row++;
            }
            worksheet.Cells.AutoFitColumns(0);

            //create Births
            worksheet = package.Workbook.Worksheets.Add("Birth_Records");
            worksheet.Cells[1, 1].Value = "Offspring's Tag";
            worksheet.Cells[1, 2].Value = "Mother's Tag";
            worksheet.Cells[1, 3].Value = "Father's Tag";
            worksheet.Cells[1, 4].Value = "Date of Birth";
            worksheet.Cells[1, 5].Value = "Score";
            worksheet.Cells[1, 6].Value = "Alive";
            worksheet.Cells[1, 7].Value = "Born";
            worksheet.Cells[1, 8].Value = "Notes";

            List<Birth> births = db.Births.Where(birth => birth.Animal.owner == userID).ToList();
            row = 2;
            foreach (Birth birth in births)
            {
                worksheet.Cells[row, 1].Value = db.Animals.Find(birth.child_id).tag;
                worksheet.Cells[row, 2].Value = db.Animals.Find(db.Breedings.Find(birth.breed_id).mother_id).tag;
                worksheet.Cells[row, 3].Value = db.Animals.Find(db.Breedings.Find(birth.breed_id).father_id).tag;
                worksheet.Cells[row, 4].Value = db.Animals.Find(birth.child_id).dob;
                worksheet.Cells[row, 5].Value = birth.score;
                worksheet.Cells[row, 6].Value = db.Breedings.Find(birth.breed_id).born;
                worksheet.Cells[row, 7].Value = db.Breedings.Find(birth.breed_id).alive;
                worksheet.Cells[row, 8].Value = birth.notes;
                row++;
            }
            worksheet.Cells.AutoFitColumns(0);

            // wrap it up
            package.Workbook.Properties.Title = "GoatMGMT: " + DateTime.Now;
            package.Workbook.Properties.Author = db.UserProfiles.Find(userID).Username; //TODO switch to First_name + Last_name

            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment; filename=FarmsDatabaseReport.xlsx");
            Response.BinaryWrite(package.GetAsByteArray());
            Response.End();
            
            return RedirectToAction("Summary");
        }
        
        
        public ActionResult Error()
        {
            return View();
        }
    }
}