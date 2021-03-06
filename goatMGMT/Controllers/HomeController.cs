﻿using System;
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

        public ActionResult FAQ()
        {
            
            return View(db.FAQs.ToList());
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

        [Authorize]
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
            int numberBornForWeaningGraph = 0;
            int numberWeanedForWeaningGraph = 0;
            int parBW1Count = 0;
            int parWW1Count = 0;
            int parBW2Count = 0;
            int parWW2Count = 0;
            int parBW3Count = 0;
            int parWW3Count = 0;
            int parBW4Count = 0;
            int parWW4Count = 0;

            int[,] mySumBreeds = new int[25, 12];
            int[,] allSumBreeds = new int[25, 12];

            SummaryViewModel svm = new SummaryViewModel();
            svm.myArray = new double[26, 12];
            svm.allArray = new double[26, 12];
            var myAnimalList = db.Animals.Where(m => m.owner == userID).ToList();
            svm.totalSire = db.Animals.Where(m => m.owner == userID && m.sex == true && m.isChild == false).Count();
            svm.totalDam = db.Animals.Where(m => m.owner == userID && m.sex == false && m.isChild == false).Count();
            svm.activeSire = db.Animals.Where(m => m.owner == userID && m.sex == true && m.status_code == "Active" && m.isChild == false).Count();
            svm.activeDam = db.Animals.Where(m => m.owner == userID && m.sex == false && m.status_code == "Active" && m.isChild == false).Count();
            if (User.IsInRole("admin"))
            {
                myAnimalList = db.Animals.ToList();
                svm.totalSire = db.Animals.Where(m => m.sex == true && m.isChild == false).Count();
                svm.totalDam = db.Animals.Where(m => m.sex == false && m.isChild == false).Count();
                svm.activeSire = db.Animals.Where(m => m.sex == true && m.status_code == "Active" && m.isChild == false).Count();
                svm.activeDam = db.Animals.Where(m => m.sex == false && m.status_code == "Active" && m.isChild == false).Count();
            }
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
                            if (((DateTime)animal.weaning_date - animal.dob).Days > 10)
                            {
                                svm.maleADGWeaning += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                svm.ADGWeaning += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                ADGWCount++;
                                MaleADGWCount++;
                                switch (animal.breed_code)
                                {
                                    case "Purbred Boer" : {
                                        svm.myArray[0, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                        svm.myArray[0, 1] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                        mySumBreeds[0, 0]++;
                                        mySumBreeds[0, 1]++;
                                        break;
                                    }
                                    case "Percentage-87.5 Boer" : {
                                        svm.myArray[1, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                        svm.myArray[1, 1] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                        mySumBreeds[1, 0]++;
                                        mySumBreeds[1, 1]++;
                                        break;
                                    }
                                    case "Percentage-75 Boer" : {
                                        svm.myArray[2, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                        svm.myArray[2, 1] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                        mySumBreeds[2, 0]++;
                                        mySumBreeds[2, 1]++;
                                        break;
                                    }
                                    case "Spanish" : {
                                        svm.myArray[3, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                        svm.myArray[3, 1] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                        mySumBreeds[3, 0]++;
                                        mySumBreeds[3, 1]++;
                                        break;
                                    }
                                    case "Nubian" : {
                                        svm.myArray[4, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                        svm.myArray[4, 1] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                        mySumBreeds[4, 0]++;
                                        mySumBreeds[4, 1]++;
                                        break;
                                    }
                                    case "Boer x Spanish" : {
                                        svm.myArray[5, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                        svm.myArray[5, 1] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                        mySumBreeds[5, 0]++;
                                        mySumBreeds[5, 1]++;
                                        break;
                                    }
                                    case "Pure Kiko" : {
                                        svm.myArray[6, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                        svm.myArray[6, 1] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                        mySumBreeds[6, 0]++;
                                        mySumBreeds[6, 1]++;
                                        break;
                                    }
                                    case "Percentage-87.5 Kiko" : {
                                        svm.myArray[7, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                        svm.myArray[7, 1] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                        mySumBreeds[7, 0]++;
                                        mySumBreeds[7, 1]++;
                                        break;
                                    }
                                    case "Percentage-75 Kiko" : {
                                        svm.myArray[8, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                        svm.myArray[8, 1] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                        mySumBreeds[8, 0]++;
                                        mySumBreeds[8, 1]++;
                                        break;
                                    }
                                    case "Kiko x Spanish" : {
                                        svm.myArray[9, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                        svm.myArray[9, 1] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                        mySumBreeds[9, 0]++;
                                        mySumBreeds[9, 1]++;
                                        break;
                                    }
                                    case  "Kiko x Boer" : {
                                        svm.myArray[10, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                        svm.myArray[10, 1] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                        mySumBreeds[10, 0]++;
                                        mySumBreeds[10, 1]++;
                                        break;
                                    }
                                    case "Boer X Nubian" : {
                                        svm.myArray[11, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                        svm.myArray[11, 1] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                        mySumBreeds[11, 0]++;
                                        mySumBreeds[11, 1]++;
                                        break;
                                    }
                                    case "Boer x Spanish x Nubian" : {
                                        svm.myArray[12, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                        svm.myArray[12, 1] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                        mySumBreeds[12, 0]++;
                                        mySumBreeds[12, 1]++;
                                        break;
                                    }
                                    case "Savanna" : {
                                        svm.myArray[13, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                        svm.myArray[13, 1] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                        mySumBreeds[13, 0]++;
                                        mySumBreeds[13, 1]++;
                                        break;
                                    }
                                    case "Percentag-87.5 Savanna" : {
                                        svm.myArray[14, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                        svm.myArray[14, 1] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                        mySumBreeds[14, 0]++;
                                        mySumBreeds[14, 1]++;
                                        break;
                                    }
                                    case "Percentage-75 Savanna" : {
                                        svm.myArray[15, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                        svm.myArray[15, 1] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                        mySumBreeds[15, 0]++;
                                        mySumBreeds[15, 1]++;
                                        break;
                                    }
                                    case "Savanna x Boer" : {
                                        svm.myArray[16, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                        svm.myArray[16, 1] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                        mySumBreeds[16, 0]++;
                                        mySumBreeds[16, 1]++;
                                        break;
                                    }
                                    case "Savanna x Kiko" : {
                                        svm.myArray[17, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                        svm.myArray[17, 1] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                        mySumBreeds[17, 0]++;
                                        mySumBreeds[17, 1]++;
                                        break;
                                    }
                                    case "Nubian x Spanish" : {
                                        svm.myArray[18, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                        svm.myArray[18, 1] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                        mySumBreeds[18, 0]++;
                                        mySumBreeds[18, 1]++;
                                        break;
                                    }
                                    case "Savanna x Boer x Kiko" : {
                                        svm.myArray[19, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                        svm.myArray[19, 1] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                        mySumBreeds[19, 0]++;
                                        mySumBreeds[19, 1]++;
                                        break;
                                    }
                                    case "Angora" : {
                                        svm.myArray[20, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                        svm.myArray[20, 1] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                        mySumBreeds[20, 0]++;
                                        mySumBreeds[20, 1]++;
                                        break;
                                    }
                                    case "Boer x Angora" : {
                                        svm.myArray[21, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                        svm.myArray[21, 1] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                        mySumBreeds[21, 0]++;
                                        mySumBreeds[21, 1]++;
                                        break;
                                    }
                                    case "Kiko x Angora" : {
                                        svm.myArray[22, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                        svm.myArray[22, 1] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                        mySumBreeds[22, 0]++;
                                        mySumBreeds[22, 1]++;
                                        break;
                                    }
                                    case "UNKNOWN CROSS" : {
                                        svm.myArray[23, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                        svm.myArray[23, 1] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                        mySumBreeds[23, 0]++;
                                        mySumBreeds[23, 1]++;
                                        break;
                                    }
                                    case "OTHERS" : {
                                        svm.myArray[24, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                        svm.myArray[24, 1] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                        mySumBreeds[24, 0]++;
                                        mySumBreeds[24, 1]++;
                                        break;
                                    }
                                }
                            }
                        }
                        if (animal.post_weaning_date != null && animal.post_weaning_weight != null && animal.weaning_weight != null)
                        {
                            if (((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days > 10)
                            {
                                svm.maleADGPostWeaning += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                svm.ADGPostWeaning += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                ADGPWCount++;
                                MaleADGPWCount++;
                                switch (animal.breed_code)
                                {
                                    case "Purbred Boer":
                                        {
                                            svm.myArray[0, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.myArray[0, 4] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            mySumBreeds[0, 3]++;
                                            mySumBreeds[0, 4]++;
                                            break;
                                        }
                                    case "Percentage-87.5 Boer":
                                        {
                                            svm.myArray[1, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.myArray[1, 4] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            mySumBreeds[1, 3]++;
                                            mySumBreeds[1, 4]++;
                                            break;
                                        }
                                    case "Percentage-75 Boer":
                                        {
                                            svm.myArray[2, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.myArray[2, 4] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            mySumBreeds[2, 3]++;
                                            mySumBreeds[2, 4]++;
                                            break;
                                        }
                                    case "Spanish":
                                        {
                                            svm.myArray[3, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.myArray[3, 4] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            mySumBreeds[3, 3]++;
                                            mySumBreeds[3, 4]++;
                                            break;
                                        }
                                    case "Nubian":
                                        {
                                            svm.myArray[4, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.myArray[4, 4] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            mySumBreeds[4, 3]++;
                                            mySumBreeds[4, 4]++;
                                            break;
                                        }
                                    case "Boer x Spanish":
                                        {
                                            svm.myArray[5, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.myArray[5, 4] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            mySumBreeds[5, 3]++;
                                            mySumBreeds[5, 4]++;
                                            break;
                                        }
                                    case "Pure Kiko":
                                        {
                                            svm.myArray[6, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.myArray[6, 4] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            mySumBreeds[6, 3]++;
                                            mySumBreeds[6, 4]++;
                                            break;
                                        }
                                    case "Percentage-87.5 Kiko":
                                        {
                                            svm.myArray[7, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.myArray[7, 4] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            mySumBreeds[7, 3]++;
                                            mySumBreeds[7, 4]++;
                                            break;
                                        }
                                    case "Percentage-75 Kiko":
                                        {
                                            svm.myArray[8, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.myArray[8, 4] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            mySumBreeds[8, 3]++;
                                            mySumBreeds[8, 4]++;
                                            break;
                                        }
                                    case "Kiko x Spanish":
                                        {
                                            svm.myArray[9, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.myArray[9, 4] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            mySumBreeds[9, 3]++;
                                            mySumBreeds[9, 4]++;
                                            break;
                                        }
                                    case "Kiko x Boer":
                                        {
                                            svm.myArray[10, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.myArray[10, 4] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            mySumBreeds[10, 3]++;
                                            mySumBreeds[10, 4]++;
                                            break;
                                        }
                                    case "Boer X Nubian":
                                        {
                                            svm.myArray[11, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.myArray[11, 4] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            mySumBreeds[11, 3]++;
                                            mySumBreeds[11, 4]++;
                                            break;
                                        }
                                    case "Boer x Spanish x Nubian":
                                        {
                                            svm.myArray[12, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.myArray[12, 4] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            mySumBreeds[12, 3]++;
                                            mySumBreeds[12, 4]++;
                                            break;
                                        }
                                    case "Savanna":
                                        {
                                            svm.myArray[13, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.myArray[13, 4] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            mySumBreeds[13, 3]++;
                                            mySumBreeds[13, 4]++;
                                            break;
                                        }
                                    case "Percentag-87.5 Savanna":
                                        {
                                            svm.myArray[14, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.myArray[14, 4] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            mySumBreeds[14, 3]++;
                                            mySumBreeds[14, 4]++;
                                            break;
                                        }
                                    case "Percentage-75 Savanna":
                                        {
                                            svm.myArray[15, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.myArray[15, 4] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            mySumBreeds[15, 3]++;
                                            mySumBreeds[15, 4]++;
                                            break;
                                        }
                                    case "Savanna x Boer":
                                        {
                                            svm.myArray[16, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.myArray[16, 4] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            mySumBreeds[16, 3]++;
                                            mySumBreeds[16, 4]++;
                                            break;
                                        }
                                    case "Savanna x Kiko":
                                        {
                                            svm.myArray[17, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.myArray[17, 4] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            mySumBreeds[17, 3]++;
                                            mySumBreeds[17, 4]++;
                                            break;
                                        }
                                    case "Nubian x Spanish":
                                        {
                                            svm.myArray[18, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.myArray[18, 4] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            mySumBreeds[18, 3]++;
                                            mySumBreeds[18, 4]++;
                                            break;
                                        }
                                    case "Savanna x Boer x Kiko":
                                        {
                                            svm.myArray[19, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.myArray[19, 4] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            mySumBreeds[19, 3]++;
                                            mySumBreeds[19, 4]++;
                                            break;
                                        }
                                    case "Angora":
                                        {
                                            svm.myArray[20, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.myArray[20, 4] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            mySumBreeds[20, 3]++;
                                            mySumBreeds[20, 4]++;
                                            break;
                                        }
                                    case "Boer x Angora":
                                        {
                                            svm.myArray[21, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.myArray[21, 4] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            mySumBreeds[21, 3]++;
                                            mySumBreeds[21, 4]++;
                                            break;
                                        }
                                    case "Kiko x Angora":
                                        {
                                            svm.myArray[22, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.myArray[22, 4] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            mySumBreeds[22, 3]++;
                                            mySumBreeds[22, 4]++;
                                            break;
                                        }
                                    case "UNKNOWN CROSS":
                                        {
                                            svm.myArray[23, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.myArray[23, 4] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            mySumBreeds[23, 3]++;
                                            mySumBreeds[23, 4]++;
                                            break;
                                        }
                                    case "OTHERS":
                                        {
                                            svm.myArray[24, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.myArray[24, 4] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            mySumBreeds[24, 3]++;
                                            mySumBreeds[24, 4]++;
                                            break;
                                        }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (animal.weaning_date != null && animal.weaning_weight != null && animal.weaning_date != null)
                        {
                            if (((DateTime)animal.weaning_date - animal.dob).Days > 10)
                            {
                                svm.femaleADGWeaning += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                svm.ADGWeaning += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                ADGWCount++;
                                FemaleADGWCount++;
                                switch (animal.breed_code)
                                {
                                    case "Purbred Boer":
                                        {
                                            svm.myArray[0, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            svm.myArray[0, 2] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            mySumBreeds[0, 0]++;
                                            mySumBreeds[0, 2]++;
                                            break;
                                        }
                                    case "Percentage-87.5 Boer":
                                        {
                                            svm.myArray[1, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            svm.myArray[1, 2] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            mySumBreeds[1, 0]++;
                                            mySumBreeds[1, 2]++;
                                            break;
                                        }
                                    case "Percentage-75 Boer":
                                        {
                                            svm.myArray[2, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            svm.myArray[2, 2] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            mySumBreeds[2, 0]++;
                                            mySumBreeds[2, 2]++;
                                            break;
                                        }
                                    case "Spanish":
                                        {
                                            svm.myArray[3, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            svm.myArray[3, 2] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            mySumBreeds[3, 0]++;
                                            mySumBreeds[3, 2]++;
                                            break;
                                        }
                                    case "Nubian":
                                        {
                                            svm.myArray[4, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            svm.myArray[4, 2] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            mySumBreeds[4, 0]++;
                                            mySumBreeds[4, 2]++;
                                            break;
                                        }
                                    case "Boer x Spanish":
                                        {
                                            svm.myArray[5, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            svm.myArray[5, 2] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            mySumBreeds[5, 0]++;
                                            mySumBreeds[5, 2]++;
                                            break;
                                        }
                                    case "Pure Kiko":
                                        {
                                            svm.myArray[6, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            svm.myArray[6, 2] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            mySumBreeds[6, 0]++;
                                            mySumBreeds[6, 2]++;
                                            break;
                                        }
                                    case "Percentage-87.5 Kiko":
                                        {
                                            svm.myArray[7, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            svm.myArray[7, 2] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            mySumBreeds[7, 0]++;
                                            mySumBreeds[7, 2]++;
                                            break;
                                        }
                                    case "Percentage-75 Kiko":
                                        {
                                            svm.myArray[8, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            svm.myArray[8, 2] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            mySumBreeds[8, 0]++;
                                            mySumBreeds[8, 2]++;
                                            break;
                                        }
                                    case "Kiko x Spanish":
                                        {
                                            svm.myArray[9, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            svm.myArray[9, 2] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            mySumBreeds[9, 0]++;
                                            mySumBreeds[9, 2]++;
                                            break;
                                        }
                                    case "Kiko x Boer":
                                        {
                                            svm.myArray[10, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            svm.myArray[10, 2] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            mySumBreeds[10, 0]++;
                                            mySumBreeds[10, 2]++;
                                            break;
                                        }
                                    case "Boer X Nubian":
                                        {
                                            svm.myArray[11, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            svm.myArray[11, 2] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            mySumBreeds[11, 0]++;
                                            mySumBreeds[11, 2]++;
                                            break;
                                        }
                                    case "Boer x Spanish x Nubian":
                                        {
                                            svm.myArray[12, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            svm.myArray[12, 2] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            mySumBreeds[12, 0]++;
                                            mySumBreeds[12, 2]++;
                                            break;
                                        }
                                    case "Savanna":
                                        {
                                            svm.myArray[13, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            svm.myArray[13, 2] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            mySumBreeds[13, 0]++;
                                            mySumBreeds[13, 2]++;
                                            break;
                                        }
                                    case "Percentag-87.5 Savanna":
                                        {
                                            svm.myArray[14, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            svm.myArray[14, 2] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            mySumBreeds[14, 0]++;
                                            mySumBreeds[14, 2]++;
                                            break;
                                        }
                                    case "Percentage-75 Savanna":
                                        {
                                            svm.myArray[15, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            svm.myArray[15, 2] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            mySumBreeds[15, 0]++;
                                            mySumBreeds[15, 2]++;
                                            break;
                                        }
                                    case "Savanna x Boer":
                                        {
                                            svm.myArray[16, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            svm.myArray[16, 2] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            mySumBreeds[16, 0]++;
                                            mySumBreeds[16, 2]++;
                                            break;
                                        }
                                    case "Savanna x Kiko":
                                        {
                                            svm.myArray[17, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            svm.myArray[17, 2] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            mySumBreeds[17, 0]++;
                                            mySumBreeds[17, 2]++;
                                            break;
                                        }
                                    case "Nubian x Spanish":
                                        {
                                            svm.myArray[18, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            svm.myArray[18, 2] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            mySumBreeds[18, 0]++;
                                            mySumBreeds[18, 2]++;
                                            break;
                                        }
                                    case "Savanna x Boer x Kiko":
                                        {
                                            svm.myArray[19, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            svm.myArray[19, 2] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            mySumBreeds[19, 0]++;
                                            mySumBreeds[19, 2]++;
                                            break;
                                        }
                                    case "Angora":
                                        {
                                            svm.myArray[20, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            svm.myArray[20, 2] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            mySumBreeds[20, 0]++;
                                            mySumBreeds[20, 2]++;
                                            break;
                                        }
                                    case "Boer x Angora":
                                        {
                                            svm.myArray[21, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            svm.myArray[21, 2] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            mySumBreeds[21, 0]++;
                                            mySumBreeds[21, 2]++;
                                            break;
                                        }
                                    case "Kiko x Angora":
                                        {
                                            svm.myArray[22, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            svm.myArray[22, 2] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            mySumBreeds[22, 0]++;
                                            mySumBreeds[22, 2]++;
                                            break;
                                        }
                                    case "UNKNOWN CROSS":
                                        {
                                            svm.myArray[23, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            svm.myArray[23, 2] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            mySumBreeds[23, 0]++;
                                            mySumBreeds[23, 2]++;
                                            break;
                                        }
                                    case "OTHERS":
                                        {
                                            svm.myArray[24, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            svm.myArray[24, 2] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            mySumBreeds[24, 0]++;
                                            mySumBreeds[24, 2]++;
                                            break;
                                        }
                                }
                            }
                            
                        }
                        if (animal.post_weaning_date != null && animal.post_weaning_weight != null && animal.weaning_date != null)
                        {
                            if (((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days > 10)
                            {
                                svm.femaleADGPostWeaning += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                svm.ADGPostWeaning += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                ADGPWCount++;
                                FemaleADGPWCount++;
                                switch (animal.breed_code)
                                {
                                    case "Purbred Boer":
                                        {
                                            svm.myArray[0, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.myArray[0, 5] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            mySumBreeds[0, 3]++;
                                            mySumBreeds[0, 5]++;
                                            break;
                                        }
                                    case "Percentage-87.5 Boer":
                                        {
                                            svm.myArray[1, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.myArray[1, 5] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            mySumBreeds[1, 3]++;
                                            mySumBreeds[1, 5]++;
                                            break;
                                        }
                                    case "Percentage-75 Boer":
                                        {
                                            svm.myArray[2, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.myArray[2, 5] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            mySumBreeds[2, 3]++;
                                            mySumBreeds[2, 5]++;
                                            break;
                                        }
                                    case "Spanish":
                                        {
                                            svm.myArray[3, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.myArray[3, 5] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            mySumBreeds[3, 3]++;
                                            mySumBreeds[3, 5]++;
                                            break;
                                        }
                                    case "Nubian":
                                        {
                                            svm.myArray[4, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.myArray[4, 5] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            mySumBreeds[4, 3]++;
                                            mySumBreeds[4, 5]++;
                                            break;
                                        }
                                    case "Boer x Spanish":
                                        {
                                            svm.myArray[5, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.myArray[5, 5] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            mySumBreeds[5, 3]++;
                                            mySumBreeds[5, 5]++;
                                            break;
                                        }
                                    case "Pure Kiko":
                                        {
                                            svm.myArray[6, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.myArray[6, 5] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            mySumBreeds[6, 3]++;
                                            mySumBreeds[6, 5]++;
                                            break;
                                        }
                                    case "Percentage-87.5 Kiko":
                                        {
                                            svm.myArray[7, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.myArray[7, 5] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            mySumBreeds[7, 3]++;
                                            mySumBreeds[7, 5]++;
                                            break;
                                        }
                                    case "Percentage-75 Kiko":
                                        {
                                            svm.myArray[8, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.myArray[8, 5] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            mySumBreeds[8, 3]++;
                                            mySumBreeds[8, 5]++;
                                            break;
                                        }
                                    case "Kiko x Spanish":
                                        {
                                            svm.myArray[9, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.myArray[9, 5] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            mySumBreeds[9, 3]++;
                                            mySumBreeds[9, 5]++;
                                            break;
                                        }
                                    case "Kiko x Boer":
                                        {
                                            svm.myArray[10, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.myArray[10, 5] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            mySumBreeds[10, 3]++;
                                            mySumBreeds[10, 5]++;
                                            break;
                                        }
                                    case "Boer X Nubian":
                                        {
                                            svm.myArray[11, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.myArray[11, 5] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            mySumBreeds[11, 3]++;
                                            mySumBreeds[11, 5]++;
                                            break;
                                        }
                                    case "Boer x Spanish x Nubian":
                                        {
                                            svm.myArray[12, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.myArray[12, 5] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            mySumBreeds[12, 3]++;
                                            mySumBreeds[12, 5]++;
                                            break;
                                        }
                                    case "Savanna":
                                        {
                                            svm.myArray[13, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.myArray[13, 5] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            mySumBreeds[13, 3]++;
                                            mySumBreeds[13, 5]++;
                                            break;
                                        }
                                    case "Percentag-87.5 Savanna":
                                        {
                                            svm.myArray[14, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.myArray[14, 5] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            mySumBreeds[14, 3]++;
                                            mySumBreeds[14, 5]++;
                                            break;
                                        }
                                    case "Percentage-75 Savanna":
                                        {
                                            svm.myArray[15, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.myArray[15, 5] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            mySumBreeds[15, 3]++;
                                            mySumBreeds[15, 5]++;
                                            break;
                                        }
                                    case "Savanna x Boer":
                                        {
                                            svm.myArray[16, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.myArray[16, 5] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            mySumBreeds[16, 3]++;
                                            mySumBreeds[16, 5]++;
                                            break;
                                        }
                                    case "Savanna x Kiko":
                                        {
                                            svm.myArray[17, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.myArray[17, 5] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            mySumBreeds[17, 3]++;
                                            mySumBreeds[17, 5]++;
                                            break;
                                        }
                                    case "Nubian x Spanish":
                                        {
                                            svm.myArray[18, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.myArray[18, 5] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            mySumBreeds[18, 3]++;
                                            mySumBreeds[18, 5]++;
                                            break;
                                        }
                                    case "Savanna x Boer x Kiko":
                                        {
                                            svm.myArray[19, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.myArray[19, 5] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            mySumBreeds[19, 3]++;
                                            mySumBreeds[19, 5]++;
                                            break;
                                        }
                                    case "Angora":
                                        {
                                            svm.myArray[20, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.myArray[20, 5] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            mySumBreeds[20, 3]++;
                                            mySumBreeds[20, 5]++;
                                            break;
                                        }
                                    case "Boer x Angora":
                                        {
                                            svm.myArray[21, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.myArray[21, 5] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            mySumBreeds[21, 3]++;
                                            mySumBreeds[21, 5]++;
                                            break;
                                        }
                                    case "Kiko x Angora":
                                        {
                                            svm.myArray[22, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.myArray[22, 5] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            mySumBreeds[22, 3]++;
                                            mySumBreeds[22, 5]++;
                                            break;
                                        }
                                    case "UNKNOWN CROSS":
                                        {
                                            svm.myArray[23, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.myArray[23, 5] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            mySumBreeds[23, 3]++;
                                            mySumBreeds[23, 5]++;
                                            break;
                                        }
                                    case "OTHERS":
                                        {
                                            svm.myArray[24, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.myArray[24, 5] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            mySumBreeds[24, 3]++;
                                            mySumBreeds[24, 5]++;
                                            break;
                                        }
                                }
                            }
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
                            switch (animal.breed_code)
                            {
                                case "Purbred Boer":
                                    {
                                        svm.myArray[0, 6] += (double)animal.birth_weight;
                                        svm.myArray[0, 7] += (double)animal.birth_weight;
                                        mySumBreeds[0, 6]++;
                                        mySumBreeds[0, 7]++;
                                        break;
                                    }
                                case "Percentage-87.5 Boer":
                                    {
                                        svm.myArray[1, 6] += (double)animal.birth_weight;
                                        svm.myArray[1, 7] += (double)animal.birth_weight;
                                        mySumBreeds[1, 6]++;
                                        mySumBreeds[1, 7]++;
                                        break;
                                    }
                                case "Percentage-75 Boer":
                                    {
                                        svm.myArray[2, 6] += (double)animal.birth_weight;
                                        svm.myArray[2, 7] += (double)animal.birth_weight;
                                        mySumBreeds[2, 6]++;
                                        mySumBreeds[2, 7]++;
                                        break;
                                    }
                                case "Spanish":
                                    {
                                        svm.myArray[3, 6] += (double)animal.birth_weight;
                                        svm.myArray[3, 7] += (double)animal.birth_weight;
                                        mySumBreeds[3, 6]++;
                                        mySumBreeds[3, 7]++;
                                        break;
                                    }
                                case "Nubian":
                                    {
                                        svm.myArray[4, 6] += (double)animal.birth_weight;
                                        svm.myArray[4, 7] += (double)animal.birth_weight;
                                        mySumBreeds[4, 6]++;
                                        mySumBreeds[4, 7]++;
                                        break;
                                    }
                                case "Boer x Spanish":
                                    {
                                        svm.myArray[5, 6] += (double)animal.birth_weight;
                                        svm.myArray[5, 7] += (double)animal.birth_weight;
                                        mySumBreeds[5, 6]++;
                                        mySumBreeds[5, 7]++;
                                        break;
                                    }
                                case "Pure Kiko":
                                    {
                                        svm.myArray[6, 6] += (double)animal.birth_weight;
                                        svm.myArray[6, 7] += (double)animal.birth_weight;
                                        mySumBreeds[6, 6]++;
                                        mySumBreeds[6, 7]++;
                                        break;
                                    }
                                case "Percentage-87.5 Kiko":
                                    {
                                        svm.myArray[7, 6] += (double)animal.birth_weight;
                                        svm.myArray[7, 7] += (double)animal.birth_weight;
                                        mySumBreeds[7, 6]++;
                                        mySumBreeds[7, 7]++;
                                        break;
                                    }
                                case "Percentage-75 Kiko":
                                    {
                                        svm.myArray[8, 6] += (double)animal.birth_weight;
                                        svm.myArray[8, 7] += (double)animal.birth_weight;
                                        mySumBreeds[8, 6]++;
                                        mySumBreeds[8, 7]++;
                                        break;
                                    }
                                case "Kiko x Spanish":
                                    {
                                        svm.myArray[9, 6] += (double)animal.birth_weight;
                                        svm.myArray[9, 7] += (double)animal.birth_weight;
                                        mySumBreeds[9, 6]++;
                                        mySumBreeds[9, 7]++;
                                        break;
                                    }
                                case "Kiko x Boer":
                                    {
                                        svm.myArray[10, 6] += (double)animal.birth_weight;
                                        svm.myArray[10, 7] += (double)animal.birth_weight;
                                        mySumBreeds[10, 6]++;
                                        mySumBreeds[10, 7]++;
                                        break;
                                    }
                                case "Boer X Nubian":
                                    {
                                        svm.myArray[11, 6] += (double)animal.birth_weight;
                                        svm.myArray[11, 7] += (double)animal.birth_weight;
                                        mySumBreeds[11, 6]++;
                                        mySumBreeds[11, 7]++;
                                        break;
                                    }
                                case "Boer x Spanish x Nubian":
                                    {
                                        svm.myArray[12, 6] += (double)animal.birth_weight;
                                        svm.myArray[12, 7] += (double)animal.birth_weight;
                                        mySumBreeds[12, 6]++;
                                        mySumBreeds[12, 7]++;
                                        break;
                                    }
                                case "Savanna":
                                    {
                                        svm.myArray[13, 6] += (double)animal.birth_weight;
                                        svm.myArray[13, 7] += (double)animal.birth_weight;
                                        mySumBreeds[13, 6]++;
                                        mySumBreeds[13, 7]++;
                                        break;
                                    }
                                case "Percentag-87.5 Savanna":
                                    {
                                        svm.myArray[14, 6] += (double)animal.birth_weight;
                                        svm.myArray[14, 7] += (double)animal.birth_weight;
                                        mySumBreeds[14, 6]++;
                                        mySumBreeds[14, 7]++;
                                        break;
                                    }
                                case "Percentage-75 Savanna":
                                    {
                                        svm.myArray[15, 6] += (double)animal.birth_weight;
                                        svm.myArray[15, 7] += (double)animal.birth_weight;
                                        mySumBreeds[15, 6]++;
                                        mySumBreeds[15, 7]++;
                                        break;
                                    }
                                case "Savanna x Boer":
                                    {
                                        svm.myArray[16, 6] += (double)animal.birth_weight;
                                        svm.myArray[16, 7] += (double)animal.birth_weight;
                                        mySumBreeds[16, 6]++;
                                        mySumBreeds[16, 7]++;
                                        break;
                                    }
                                case "Savanna x Kiko":
                                    {
                                        svm.myArray[17, 6] += (double)animal.birth_weight;
                                        svm.myArray[17, 7] += (double)animal.birth_weight;
                                        mySumBreeds[17, 6]++;
                                        mySumBreeds[17, 7]++;
                                        break;
                                    }
                                case "Nubian x Spanish":
                                    {
                                        svm.myArray[18, 6] += (double)animal.birth_weight;
                                        svm.myArray[18, 7] += (double)animal.birth_weight;
                                        mySumBreeds[18, 6]++;
                                        mySumBreeds[18, 7]++;
                                        break;
                                    }
                                case "Savanna x Boer x Kiko":
                                    {
                                        svm.myArray[19, 6] += (double)animal.birth_weight;
                                        svm.myArray[19, 7] += (double)animal.birth_weight;
                                        mySumBreeds[19, 6]++;
                                        mySumBreeds[19, 7]++;
                                        break;
                                    }
                                case "Angora":
                                    {
                                        svm.myArray[20, 6] += (double)animal.birth_weight;
                                        svm.myArray[20, 7] += (double)animal.birth_weight;
                                        mySumBreeds[20, 6]++;
                                        mySumBreeds[20, 7]++;
                                        break;
                                    }
                                case "Boer x Angora":
                                    {
                                        svm.myArray[21, 6] += (double)animal.birth_weight;
                                        svm.myArray[21, 7] += (double)animal.birth_weight;
                                        mySumBreeds[21, 6]++;
                                        mySumBreeds[21, 7]++;
                                        break;
                                    }
                                case "Kiko x Angora":
                                    {
                                        svm.myArray[22, 6] += (double)animal.birth_weight;
                                        svm.myArray[22, 7] += (double)animal.birth_weight;
                                        mySumBreeds[22, 6]++;
                                        mySumBreeds[22, 7]++;
                                        break;
                                    }
                                case "UNKNOWN CROSS":
                                    {
                                        svm.myArray[23, 6] += (double)animal.birth_weight;
                                        svm.myArray[23, 7] += (double)animal.birth_weight;
                                        mySumBreeds[23, 6]++;
                                        mySumBreeds[23, 7]++;
                                        break;
                                    }
                                case "OTHERS":
                                    {
                                        svm.myArray[24, 6] += (double)animal.birth_weight;
                                        svm.myArray[24, 7] += (double)animal.birth_weight;
                                        mySumBreeds[24, 6]++;
                                        mySumBreeds[24, 7]++;
                                        break;
                                    }
                            }
                        }
                        if (animal.weaning_weight != null)
                        {
                            svm.maleAvgWW += (double)animal.weaning_weight;
                            maleWWCount++;
                            switch (animal.breed_code)
                            {
                                case "Purbred Boer":
                                    {
                                        svm.myArray[0, 9] += (double)animal.weaning_weight;
                                        svm.myArray[0, 10] += (double)animal.weaning_weight;
                                        mySumBreeds[0, 9]++;
                                        mySumBreeds[0, 10]++;
                                        break;
                                    }
                                case "Percentage-87.5 Boer":
                                    {
                                        svm.myArray[1, 9] += (double)animal.weaning_weight;
                                        svm.myArray[1, 10] += (double)animal.weaning_weight;
                                        mySumBreeds[1, 9]++;
                                        mySumBreeds[1, 10]++;
                                        break;
                                    }
                                case "Percentage-75 Boer":
                                    {
                                        svm.myArray[2, 9] += (double)animal.weaning_weight;
                                        svm.myArray[2, 10] += (double)animal.weaning_weight;
                                        mySumBreeds[2, 9]++;
                                        mySumBreeds[2, 10]++;
                                        break;
                                    }
                                case "Spanish":
                                    {
                                        svm.myArray[3, 9] += (double)animal.weaning_weight;
                                        svm.myArray[3, 10] += (double)animal.weaning_weight;
                                        mySumBreeds[3, 9]++;
                                        mySumBreeds[3, 10]++;
                                        break;
                                    }
                                case "Nubian":
                                    {
                                        svm.myArray[4, 9] += (double)animal.weaning_weight;
                                        svm.myArray[4, 10] += (double)animal.weaning_weight;
                                        mySumBreeds[4, 9]++;
                                        mySumBreeds[4, 10]++;
                                        break;
                                    }
                                case "Boer x Spanish":
                                    {
                                        svm.myArray[5, 9] += (double)animal.weaning_weight;
                                        svm.myArray[5, 10] += (double)animal.weaning_weight;
                                        mySumBreeds[5, 9]++;
                                        mySumBreeds[5, 10]++;
                                        break;
                                    }
                                case "Pure Kiko":
                                    {
                                        svm.myArray[6, 9] += (double)animal.weaning_weight;
                                        svm.myArray[6, 10] += (double)animal.weaning_weight;
                                        mySumBreeds[6, 9]++;
                                        mySumBreeds[6, 10]++;
                                        break;
                                    }
                                case "Percentage-87.5 Kiko":
                                    {
                                        svm.myArray[7, 9] += (double)animal.weaning_weight;
                                        svm.myArray[7, 10] += (double)animal.weaning_weight;
                                        mySumBreeds[7, 9]++;
                                        mySumBreeds[7, 10]++;
                                        break;
                                    }
                                case "Percentage-75 Kiko":
                                    {
                                        svm.myArray[8, 9] += (double)animal.weaning_weight;
                                        svm.myArray[8, 10] += (double)animal.weaning_weight;
                                        mySumBreeds[8, 9]++;
                                        mySumBreeds[8, 10]++;
                                        break;
                                    }
                                case "Kiko x Spanish":
                                    {
                                        svm.myArray[9, 9] += (double)animal.weaning_weight;
                                        svm.myArray[9, 10] += (double)animal.weaning_weight;
                                        mySumBreeds[9, 9]++;
                                        mySumBreeds[9, 10]++;
                                        break;
                                    }
                                case "Kiko x Boer":
                                    {
                                        svm.myArray[10, 9] += (double)animal.weaning_weight;
                                        svm.myArray[10, 10] += (double)animal.weaning_weight;
                                        mySumBreeds[10, 9]++;
                                        mySumBreeds[10, 10]++;
                                        break;
                                    }
                                case "Boer X Nubian":
                                    {
                                        svm.myArray[11, 9] += (double)animal.weaning_weight;
                                        svm.myArray[11, 10] += (double)animal.weaning_weight;
                                        mySumBreeds[11, 9]++;
                                        mySumBreeds[11, 10]++;
                                        break;
                                    }
                                case "Boer x Spanish x Nubian":
                                    {
                                        svm.myArray[12, 9] += (double)animal.weaning_weight;
                                        svm.myArray[12, 10] += (double)animal.weaning_weight;
                                        mySumBreeds[12, 9]++;
                                        mySumBreeds[12, 10]++;
                                        break;
                                    }
                                case "Savanna":
                                    {
                                        svm.myArray[13, 9] += (double)animal.weaning_weight;
                                        svm.myArray[13, 10] += (double)animal.weaning_weight;
                                        mySumBreeds[13, 9]++;
                                        mySumBreeds[13, 10]++;
                                        break;
                                    }
                                case "Percentag-87.5 Savanna":
                                    {
                                        svm.myArray[14, 9] += (double)animal.weaning_weight;
                                        svm.myArray[14, 10] += (double)animal.weaning_weight;
                                        mySumBreeds[14, 9]++;
                                        mySumBreeds[14, 10]++;
                                        break;
                                    }
                                case "Percentage-75 Savanna":
                                    {
                                        svm.myArray[15, 9] += (double)animal.weaning_weight;
                                        svm.myArray[15, 10] += (double)animal.weaning_weight;
                                        mySumBreeds[15, 9]++;
                                        mySumBreeds[15, 10]++;
                                        break;
                                    }
                                case "Savanna x Boer":
                                    {
                                        svm.myArray[16, 9] += (double)animal.weaning_weight;
                                        svm.myArray[16, 10] += (double)animal.weaning_weight;
                                        mySumBreeds[16, 9]++;
                                        mySumBreeds[16, 10]++;
                                        break;
                                    }
                                case "Savanna x Kiko":
                                    {
                                        svm.myArray[17, 9] += (double)animal.weaning_weight;
                                        svm.myArray[17, 10] += (double)animal.weaning_weight;
                                        mySumBreeds[17, 9]++;
                                        mySumBreeds[17, 10]++;
                                        break;
                                    }
                                case "Nubian x Spanish":
                                    {
                                        svm.myArray[18, 9] += (double)animal.weaning_weight;
                                        svm.myArray[18, 10] += (double)animal.weaning_weight;
                                        mySumBreeds[18, 9]++;
                                        mySumBreeds[18, 10]++;
                                        break;
                                    }
                                case "Savanna x Boer x Kiko":
                                    {
                                        svm.myArray[19, 9] += (double)animal.weaning_weight;
                                        svm.myArray[19, 10] += (double)animal.weaning_weight;
                                        mySumBreeds[19, 9]++;
                                        mySumBreeds[19, 10]++;
                                        break;
                                    }
                                case "Angora":
                                    {
                                        svm.myArray[20, 9] += (double)animal.weaning_weight;
                                        svm.myArray[20, 10] += (double)animal.weaning_weight;
                                        mySumBreeds[20, 9]++;
                                        mySumBreeds[20, 10]++;
                                        break;
                                    }
                                case "Boer x Angora":
                                    {
                                        svm.myArray[21, 9] += (double)animal.weaning_weight;
                                        svm.myArray[21, 10] += (double)animal.weaning_weight;
                                        mySumBreeds[21, 9]++;
                                        mySumBreeds[21, 10]++;
                                        break;
                                    }
                                case "Kiko x Angora":
                                    {
                                        svm.myArray[22, 9] += (double)animal.weaning_weight;
                                        svm.myArray[22, 10] += (double)animal.weaning_weight;
                                        mySumBreeds[22, 9]++;
                                        mySumBreeds[22, 10]++;
                                        break;
                                    }
                                case "UNKNOWN CROSS":
                                    {
                                        svm.myArray[23, 9] += (double)animal.weaning_weight;
                                        svm.myArray[23, 10] += (double)animal.weaning_weight;
                                        mySumBreeds[23, 9]++;
                                        mySumBreeds[23, 10]++;
                                        break;
                                    }
                                case "OTHERS":
                                    {
                                        svm.myArray[24, 9] += (double)animal.weaning_weight;
                                        svm.myArray[24, 10] += (double)animal.weaning_weight;
                                        mySumBreeds[24, 9]++;
                                        mySumBreeds[24, 10]++;
                                        break;
                                    }
                            }
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
                            switch (animal.breed_code)
                            {
                                case "Purbred Boer":
                                    {
                                        svm.myArray[0, 6] += (double)animal.birth_weight;
                                        svm.myArray[0, 8] += (double)animal.birth_weight;
                                        mySumBreeds[0, 6]++;
                                        mySumBreeds[0, 8]++;
                                        break;
                                    }
                                case "Percentage-87.5 Boer":
                                    {
                                        svm.myArray[1, 6] += (double)animal.birth_weight;
                                        svm.myArray[1, 8] += (double)animal.birth_weight;
                                        mySumBreeds[1, 6]++;
                                        mySumBreeds[1, 8]++;
                                        break;
                                    }
                                case "Percentage-75 Boer":
                                    {
                                        svm.myArray[2, 6] += (double)animal.birth_weight;
                                        svm.myArray[2, 8] += (double)animal.birth_weight;
                                        mySumBreeds[2, 6]++;
                                        mySumBreeds[2, 8]++;
                                        break;
                                    }
                                case "Spanish":
                                    {
                                        svm.myArray[3, 6] += (double)animal.birth_weight;
                                        svm.myArray[3, 8] += (double)animal.birth_weight;
                                        mySumBreeds[3, 6]++;
                                        mySumBreeds[3, 8]++;
                                        break;
                                    }
                                case "Nubian":
                                    {
                                        svm.myArray[4, 6] += (double)animal.birth_weight;
                                        svm.myArray[4, 8] += (double)animal.birth_weight;
                                        mySumBreeds[4, 6]++;
                                        mySumBreeds[4, 8]++;
                                        break;
                                    }
                                case "Boer x Spanish":
                                    {
                                        svm.myArray[5, 6] += (double)animal.birth_weight;
                                        svm.myArray[5, 8] += (double)animal.birth_weight;
                                        mySumBreeds[5, 6]++;
                                        mySumBreeds[5, 8]++;
                                        break;
                                    }
                                case "Pure Kiko":
                                    {
                                        svm.myArray[6, 6] += (double)animal.birth_weight;
                                        svm.myArray[6, 8] += (double)animal.birth_weight;
                                        mySumBreeds[6, 6]++;
                                        mySumBreeds[6, 8]++;
                                        break;
                                    }
                                case "Percentage-87.5 Kiko":
                                    {
                                        svm.myArray[7, 6] += (double)animal.birth_weight;
                                        svm.myArray[7, 8] += (double)animal.birth_weight;
                                        mySumBreeds[7, 6]++;
                                        mySumBreeds[7, 8]++;
                                        break;
                                    }
                                case "Percentage-75 Kiko":
                                    {
                                        svm.myArray[8, 6] += (double)animal.birth_weight;
                                        svm.myArray[8, 8] += (double)animal.birth_weight;
                                        mySumBreeds[8, 6]++;
                                        mySumBreeds[8, 8]++;
                                        break;
                                    }
                                case "Kiko x Spanish":
                                    {
                                        svm.myArray[9, 6] += (double)animal.birth_weight;
                                        svm.myArray[9, 8] += (double)animal.birth_weight;
                                        mySumBreeds[9, 6]++;
                                        mySumBreeds[9, 8]++;
                                        break;
                                    }
                                case "Kiko x Boer":
                                    {
                                        svm.myArray[10, 6] += (double)animal.birth_weight;
                                        svm.myArray[10, 8] += (double)animal.birth_weight;
                                        mySumBreeds[10, 6]++;
                                        mySumBreeds[10, 8]++;
                                        break;
                                    }
                                case "Boer X Nubian":
                                    {
                                        svm.myArray[11, 6] += (double)animal.birth_weight;
                                        svm.myArray[11, 8] += (double)animal.birth_weight;
                                        mySumBreeds[11, 6]++;
                                        mySumBreeds[11, 8]++;
                                        break;
                                    }
                                case "Boer x Spanish x Nubian":
                                    {
                                        svm.myArray[12, 6] += (double)animal.birth_weight;
                                        svm.myArray[12, 8] += (double)animal.birth_weight;
                                        mySumBreeds[12, 6]++;
                                        mySumBreeds[12, 8]++;
                                        break;
                                    }
                                case "Savanna":
                                    {
                                        svm.myArray[13, 6] += (double)animal.birth_weight;
                                        svm.myArray[13, 8] += (double)animal.birth_weight;
                                        mySumBreeds[13, 6]++;
                                        mySumBreeds[13, 8]++;
                                        break;
                                    }
                                case "Percentag-87.5 Savanna":
                                    {
                                        svm.myArray[14, 6] += (double)animal.birth_weight;
                                        svm.myArray[14, 8] += (double)animal.birth_weight;
                                        mySumBreeds[14, 6]++;
                                        mySumBreeds[14, 8]++;
                                        break;
                                    }
                                case "Percentage-75 Savanna":
                                    {
                                        svm.myArray[15, 6] += (double)animal.birth_weight;
                                        svm.myArray[15, 8] += (double)animal.birth_weight;
                                        mySumBreeds[15, 6]++;
                                        mySumBreeds[15, 8]++;
                                        break;
                                    }
                                case "Savanna x Boer":
                                    {
                                        svm.myArray[16, 6] += (double)animal.birth_weight;
                                        svm.myArray[16, 8] += (double)animal.birth_weight;
                                        mySumBreeds[16, 6]++;
                                        mySumBreeds[16, 8]++;
                                        break;
                                    }
                                case "Savanna x Kiko":
                                    {
                                        svm.myArray[17, 6] += (double)animal.birth_weight;
                                        svm.myArray[17, 8] += (double)animal.birth_weight;
                                        mySumBreeds[17, 6]++;
                                        mySumBreeds[17, 8]++;
                                        break;
                                    }
                                case "Nubian x Spanish":
                                    {
                                        svm.myArray[18, 6] += (double)animal.birth_weight;
                                        svm.myArray[18, 8] += (double)animal.birth_weight;
                                        mySumBreeds[18, 6]++;
                                        mySumBreeds[18, 8]++;
                                        break;
                                    }
                                case "Savanna x Boer x Kiko":
                                    {
                                        svm.myArray[19, 6] += (double)animal.birth_weight;
                                        svm.myArray[19, 8] += (double)animal.birth_weight;
                                        mySumBreeds[19, 6]++;
                                        mySumBreeds[19, 8]++;
                                        break;
                                    }
                                case "Angora":
                                    {
                                        svm.myArray[20, 6] += (double)animal.birth_weight;
                                        svm.myArray[20, 8] += (double)animal.birth_weight;
                                        mySumBreeds[20, 6]++;
                                        mySumBreeds[20, 8]++;
                                        break;
                                    }
                                case "Boer x Angora":
                                    {
                                        svm.myArray[21, 6] += (double)animal.birth_weight;
                                        svm.myArray[21, 8] += (double)animal.birth_weight;
                                        mySumBreeds[21, 6]++;
                                        mySumBreeds[21, 8]++;
                                        break;
                                    }
                                case "Kiko x Angora":
                                    {
                                        svm.myArray[22, 6] += (double)animal.birth_weight;
                                        svm.myArray[22, 8] += (double)animal.birth_weight;
                                        mySumBreeds[22, 6]++;
                                        mySumBreeds[22, 8]++;
                                        break;
                                    }
                                case "UNKNOWN CROSS":
                                    {
                                        svm.myArray[23, 6] += (double)animal.birth_weight;
                                        svm.myArray[23, 8] += (double)animal.birth_weight;
                                        mySumBreeds[23, 6]++;
                                        mySumBreeds[23, 8]++;
                                        break;
                                    }
                                case "OTHERS":
                                    {
                                        svm.myArray[24, 6] += (double)animal.birth_weight;
                                        svm.myArray[24, 8] += (double)animal.birth_weight;
                                        mySumBreeds[24, 6]++;
                                        mySumBreeds[24, 8]++;
                                        break;
                                    }
                            }
                        }
                        if (animal.weaning_weight != null)
                        {
                            svm.femaleAvgWW += (double)animal.weaning_weight;
                            femaleWWCount++;
                            switch (animal.breed_code)
                            {
                                case "Purbred Boer":
                                    {
                                        svm.myArray[0, 9] += (double)animal.weaning_weight;
                                        svm.myArray[0, 11] += (double)animal.weaning_weight;
                                        mySumBreeds[0, 9]++;
                                        mySumBreeds[0, 11]++;
                                        break;
                                    }
                                case "Percentage-87.5 Boer":
                                    {
                                        svm.myArray[1, 9] += (double)animal.weaning_weight;
                                        svm.myArray[1, 11] += (double)animal.weaning_weight;
                                        mySumBreeds[1, 9]++;
                                        mySumBreeds[1, 11]++;
                                        break;
                                    }
                                case "Percentage-75 Boer":
                                    {
                                        svm.myArray[2, 9] += (double)animal.weaning_weight;
                                        svm.myArray[2, 11] += (double)animal.weaning_weight;
                                        mySumBreeds[2, 9]++;
                                        mySumBreeds[2, 11]++;
                                        break;
                                    }
                                case "Spanish":
                                    {
                                        svm.myArray[3, 9] += (double)animal.weaning_weight;
                                        svm.myArray[3, 11] += (double)animal.weaning_weight;
                                        mySumBreeds[3, 9]++;
                                        mySumBreeds[3, 11]++;
                                        break;
                                    }
                                case "Nubian":
                                    {
                                        svm.myArray[4, 9] += (double)animal.weaning_weight;
                                        svm.myArray[4, 11] += (double)animal.weaning_weight;
                                        mySumBreeds[4, 9]++;
                                        mySumBreeds[4, 11]++;
                                        break;
                                    }
                                case "Boer x Spanish":
                                    {
                                        svm.myArray[5, 9] += (double)animal.weaning_weight;
                                        svm.myArray[5, 11] += (double)animal.weaning_weight;
                                        mySumBreeds[5, 9]++;
                                        mySumBreeds[5, 11]++;
                                        break;
                                    }
                                case "Pure Kiko":
                                    {
                                        svm.myArray[6, 9] += (double)animal.weaning_weight;
                                        svm.myArray[6, 11] += (double)animal.weaning_weight;
                                        mySumBreeds[6, 9]++;
                                        mySumBreeds[6, 11]++;
                                        break;
                                    }
                                case "Percentage-87.5 Kiko":
                                    {
                                        svm.myArray[7, 9] += (double)animal.weaning_weight;
                                        svm.myArray[7, 11] += (double)animal.weaning_weight;
                                        mySumBreeds[7, 9]++;
                                        mySumBreeds[7, 11]++;
                                        break;
                                    }
                                case "Percentage-75 Kiko":
                                    {
                                        svm.myArray[8, 9] += (double)animal.weaning_weight;
                                        svm.myArray[8, 11] += (double)animal.weaning_weight;
                                        mySumBreeds[8, 9]++;
                                        mySumBreeds[8, 11]++;
                                        break;
                                    }
                                case "Kiko x Spanish":
                                    {
                                        svm.myArray[9, 9] += (double)animal.weaning_weight;
                                        svm.myArray[9, 11] += (double)animal.weaning_weight;
                                        mySumBreeds[9, 9]++;
                                        mySumBreeds[9, 11]++;
                                        break;
                                    }
                                case "Kiko x Boer":
                                    {
                                        svm.myArray[10, 9] += (double)animal.weaning_weight;
                                        svm.myArray[10, 11] += (double)animal.weaning_weight;
                                        mySumBreeds[10, 9]++;
                                        mySumBreeds[10, 11]++;
                                        break;
                                    }
                                case "Boer X Nubian":
                                    {
                                        svm.myArray[11, 9] += (double)animal.weaning_weight;
                                        svm.myArray[11, 11] += (double)animal.weaning_weight;
                                        mySumBreeds[11, 9]++;
                                        mySumBreeds[11, 11]++;
                                        break;
                                    }
                                case "Boer x Spanish x Nubian":
                                    {
                                        svm.myArray[12, 9] += (double)animal.weaning_weight;
                                        svm.myArray[12, 11] += (double)animal.weaning_weight;
                                        mySumBreeds[12, 9]++;
                                        mySumBreeds[12, 11]++;
                                        break;
                                    }
                                case "Savanna":
                                    {
                                        svm.myArray[13, 9] += (double)animal.weaning_weight;
                                        svm.myArray[13, 11] += (double)animal.weaning_weight;
                                        mySumBreeds[13, 9]++;
                                        mySumBreeds[13, 11]++;
                                        break;
                                    }
                                case "Percentag-87.5 Savanna":
                                    {
                                        svm.myArray[14, 9] += (double)animal.weaning_weight;
                                        svm.myArray[14, 11] += (double)animal.weaning_weight;
                                        mySumBreeds[14, 9]++;
                                        mySumBreeds[14, 11]++;
                                        break;
                                    }
                                case "Percentage-75 Savanna":
                                    {
                                        svm.myArray[15, 9] += (double)animal.weaning_weight;
                                        svm.myArray[15, 11] += (double)animal.weaning_weight;
                                        mySumBreeds[15, 9]++;
                                        mySumBreeds[15, 11]++;
                                        break;
                                    }
                                case "Savanna x Boer":
                                    {
                                        svm.myArray[16, 9] += (double)animal.weaning_weight;
                                        svm.myArray[16, 11] += (double)animal.weaning_weight;
                                        mySumBreeds[16, 9]++;
                                        mySumBreeds[16, 11]++;
                                        break;
                                    }
                                case "Savanna x Kiko":
                                    {
                                        svm.myArray[17, 9] += (double)animal.weaning_weight;
                                        svm.myArray[17, 11] += (double)animal.weaning_weight;
                                        mySumBreeds[17, 9]++;
                                        mySumBreeds[17, 11]++;
                                        break;
                                    }
                                case "Nubian x Spanish":
                                    {
                                        svm.myArray[18, 9] += (double)animal.weaning_weight;
                                        svm.myArray[18, 11] += (double)animal.weaning_weight;
                                        mySumBreeds[18, 9]++;
                                        mySumBreeds[18, 11]++;
                                        break;
                                    }
                                case "Savanna x Boer x Kiko":
                                    {
                                        svm.myArray[19, 9] += (double)animal.weaning_weight;
                                        svm.myArray[19, 11] += (double)animal.weaning_weight;
                                        mySumBreeds[19, 9]++;
                                        mySumBreeds[19, 11]++;
                                        break;
                                    }
                                case "Angora":
                                    {
                                        svm.myArray[20, 9] += (double)animal.weaning_weight;
                                        svm.myArray[20, 11] += (double)animal.weaning_weight;
                                        mySumBreeds[20, 9]++;
                                        mySumBreeds[20, 11]++;
                                        break;
                                    }
                                case "Boer x Angora":
                                    {
                                        svm.myArray[21, 9] += (double)animal.weaning_weight;
                                        svm.myArray[21, 11] += (double)animal.weaning_weight;
                                        mySumBreeds[21, 9]++;
                                        mySumBreeds[21, 11]++;
                                        break;
                                    }
                                case "Kiko x Angora":
                                    {
                                        svm.myArray[22, 9] += (double)animal.weaning_weight;
                                        svm.myArray[22, 11] += (double)animal.weaning_weight;
                                        mySumBreeds[22, 9]++;
                                        mySumBreeds[22, 11]++;
                                        break;
                                    }
                                case "UNKNOWN CROSS":
                                    {
                                        svm.myArray[23, 9] += (double)animal.weaning_weight;
                                        svm.myArray[23, 11] += (double)animal.weaning_weight;
                                        mySumBreeds[23, 9]++;
                                        mySumBreeds[23, 11]++;
                                        break;
                                    }
                                case "OTHERS":
                                    {
                                        svm.myArray[24, 9] += (double)animal.weaning_weight;
                                        svm.myArray[24, 11] += (double)animal.weaning_weight;
                                        mySumBreeds[24, 9]++;
                                        mySumBreeds[24, 11]++;
                                        break;
                                    }
                            }
                        }
                    }
                }  

                foreach (Breeding breeding in db.Breedings.Where(m => m.father_id == animal.id))
                {
                    svm.matingCount++;
                    svm.kiddingCount += breeding.Births.Count();
                    if (svm.kiddingCount == 0)
                    {
                        svm.kiddingPercentage = 0;
                    }
                    else
                    {
                        svm.kiddingPercentage = (int)((svm.kiddingCount / svm.matingCount) * 100);
                    }
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
                    if (breeding.parity == 1)
                    {
                        svm.damParity1Count++;
                    }
                    else if (breeding.parity == 2)
                    {
                        svm.damParity2Count++;
                    }
                    else if (breeding.parity == 3)
                    {
                        svm.damParity3Count++;
                    }
                    else if (breeding.parity > 3)
                    {
                        svm.damParity4Count++;
                    }
                    svm.kidsAlivePercentage = svm.kidsBornCount / svm.kidsAliveCount;
                     foreach (Birth birth in breeding.Births){
                         Animal tempAnimal = db.Animals.Find(birth.child_id);
                         if (tempAnimal.birth_weight != null)
                         {
                             if (tempAnimal.sex)
                             {
                                 svm.numberBornMale++;
                             }
                             else
                             {
                                 svm.numberBornFemale++;
                             }
                             
                         }
                         if (tempAnimal.weaning_weight != null)
                         {
                             if (tempAnimal.sex)
                             {
                                 svm.numberWeanedMale++;
                             }
                             else
                             {
                                 svm.numberWeanedFemale++;
                             }
                             
                         }
                   }

                    foreach (Birth birth in breeding.Births)
                    {
                        if (birth.Breeding.parity == 1)
                        {
                            if (db.Animals.Find(birth.child_id).birth_weight != null)
                            {
                                svm.damParity1BW += (double)db.Animals.Find(birth.child_id).birth_weight;
                                parBW1Count++;
                            }
                            if (db.Animals.Find(birth.child_id).weaning_weight != null)
                            {
                                svm.damParity1WW += (double)db.Animals.Find(birth.child_id).weaning_weight;
                                parWW1Count++;
                            }
                        }
                        else if (birth.Breeding.parity == 2)
                        {
                            if (db.Animals.Find(birth.child_id).birth_weight != null)
                            {
                                svm.damParity2BW += (double)db.Animals.Find(birth.child_id).birth_weight;
                                parBW2Count++;
                            }
                            if (db.Animals.Find(birth.child_id).weaning_weight != null)
                            {
                                svm.damParity2WW += (double)db.Animals.Find(birth.child_id).weaning_weight;
                                parWW2Count++;
                            }
                        }
                        else if (birth.Breeding.parity == 3)
                        {
                            if (db.Animals.Find(birth.child_id).birth_weight != null)
                            {
                                svm.damParity3BW += (double)db.Animals.Find(birth.child_id).birth_weight;
                                parBW3Count++;
                            }
                            if (db.Animals.Find(birth.child_id).weaning_weight != null)
                            {
                                svm.damParity3WW += (double)db.Animals.Find(birth.child_id).weaning_weight;
                                parWW3Count++;
                            }
                        }
                        else if (birth.Breeding.parity > 3)
                        {
                            if (db.Animals.Find(birth.child_id).birth_weight != null)
                            {
                                svm.damParity4BW += (double)db.Animals.Find(birth.child_id).birth_weight;
                                parBW4Count++;
                            }
                            if (db.Animals.Find(birth.child_id).weaning_weight != null)
                            {
                                svm.damParity4WW += (double)db.Animals.Find(birth.child_id).weaning_weight;
                                parWW4Count++;
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
                            if (db.Animals.Find(birth.child_id).birth_weight != null)
                            {
                                svm.singleBirthCount++;
                                svm.singleBWAvg += (double)db.Animals.Find(birth.child_id).birth_weight;
                            }
                            if (db.Animals.Find(birth.child_id).weaning_date != null)
                            {
                                svm.singleWeaningCount++;
                                svm.singleWWAvg += (double)db.Animals.Find(birth.child_id).weaning_weight;
                            }
                        }
                        if ((int)birth.Breeding.born == 2)
                        {
                            if (db.Animals.Find(birth.child_id).birth_weight != null)
                            {
                                svm.twinBirthCount++;
                                svm.twinBWAvg += (double)db.Animals.Find(birth.child_id).birth_weight;
                            }
                            if (db.Animals.Find(birth.child_id).weaning_date != null)
                            {
                                svm.twinWeaningCount++;
                                svm.twinWWAvg += (double)db.Animals.Find(birth.child_id).weaning_weight;
                            }
                        }
                        if ((int)birth.Breeding.born == 3)
                        {
                            if (db.Animals.Find(birth.child_id).birth_weight != null)
                            {
                                svm.tripletBirthCount++;
                                svm.tripletBWAvg += (double)db.Animals.Find(birth.child_id).birth_weight;
                            }
                            if (db.Animals.Find(birth.child_id).weaning_date != null)
                            {
                                svm.tripletWeaningCount++;
                                svm.tripletWWAvg += (double)db.Animals.Find(birth.child_id).weaning_weight;
                            }
                        }
                        if ((int)birth.Breeding.born > 3)
                        {
                            if (db.Animals.Find(birth.child_id).birth_weight != null)
                            {
                                svm.quadBirthCount++;
                                svm.quadBWAvg += (double)db.Animals.Find(birth.child_id).birth_weight;
                            }
                            if (db.Animals.Find(birth.child_id).weaning_date != null)
                            {
                                svm.quadWeaningCount++;
                                svm.quadWWAvg += (double)db.Animals.Find(birth.child_id).weaning_weight;
                            }
                        }
                    }
                }
            }
            foreach (Animal animal in db.Animals)
            {
                if (animal.birth_weight != null && animal.dob != null && (animal.weaning_date != null && animal.weaning_weight != null) || (animal.post_weaning_date != null && animal.post_weaning_weight != null))
                {
                    if (animal.sex)
                    {
                        if (animal.weaning_date != null && animal.weaning_weight != null)
                        {
                            if (((DateTime)animal.weaning_date - animal.dob).Days > 10)
                            {
                                svm.allMaleADGWeaning += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                svm.allADGWeaning += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                allADGWCount++;
                                allMaleADGWCount++;
                                switch (animal.breed_code)
                                {
                                    case "Purbred Boer":
                                        {
                                            svm.allArray[0, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            svm.allArray[0, 1] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            allSumBreeds[0, 0]++;
                                            allSumBreeds[0, 1]++;
                                            break;
                                        }
                                    case "Percentage-87.5 Boer":
                                        {
                                            svm.allArray[1, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            svm.allArray[1, 1] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            allSumBreeds[1, 0]++;
                                            allSumBreeds[1, 1]++;
                                            break;
                                        }
                                    case "Percentage-75 Boer":
                                        {
                                            svm.allArray[2, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            svm.allArray[2, 1] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            allSumBreeds[2, 0]++;
                                            allSumBreeds[2, 1]++;
                                            break;
                                        }
                                    case "Spanish":
                                        {
                                            svm.allArray[3, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            svm.allArray[3, 1] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            allSumBreeds[3, 0]++;
                                            allSumBreeds[3, 1]++;
                                            break;
                                        }
                                    case "Nubian":
                                        {
                                            svm.allArray[4, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            svm.allArray[4, 1] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            allSumBreeds[4, 0]++;
                                            allSumBreeds[4, 1]++;
                                            break;
                                        }
                                    case "Boer x Spanish":
                                        {
                                            svm.allArray[5, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            svm.allArray[5, 1] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            allSumBreeds[5, 0]++;
                                            allSumBreeds[5, 1]++;
                                            break;
                                        }
                                    case "Pure Kiko":
                                        {
                                            svm.allArray[6, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            svm.allArray[6, 1] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            allSumBreeds[6, 0]++;
                                            allSumBreeds[6, 1]++;
                                            break;
                                        }
                                    case "Percentage-87.5 Kiko":
                                        {
                                            svm.allArray[7, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            svm.allArray[7, 1] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            allSumBreeds[7, 0]++;
                                            allSumBreeds[7, 1]++;
                                            break;
                                        }
                                    case "Percentage-75 Kiko":
                                        {
                                            svm.allArray[8, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            svm.allArray[8, 1] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            allSumBreeds[8, 0]++;
                                            allSumBreeds[8, 1]++;
                                            break;
                                        }
                                    case "Kiko x Spanish":
                                        {
                                            svm.allArray[9, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            svm.allArray[9, 1] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            allSumBreeds[9, 0]++;
                                            allSumBreeds[9, 1]++;
                                            break;
                                        }
                                    case "Kiko x Boer":
                                        {
                                            svm.allArray[10, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            svm.allArray[10, 1] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            allSumBreeds[10, 0]++;
                                            allSumBreeds[10, 1]++;
                                            break;
                                        }
                                    case "Boer X Nubian":
                                        {
                                            svm.allArray[11, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            svm.allArray[11, 1] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            allSumBreeds[11, 0]++;
                                            allSumBreeds[11, 1]++;
                                            break;
                                        }
                                    case "Boer x Spanish x Nubian":
                                        {
                                            svm.allArray[12, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            svm.allArray[12, 1] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            allSumBreeds[12, 0]++;
                                            allSumBreeds[12, 1]++;
                                            break;
                                        }
                                    case "Savanna":
                                        {
                                            svm.allArray[13, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            svm.allArray[13, 1] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            allSumBreeds[13, 0]++;
                                            allSumBreeds[13, 1]++;
                                            break;
                                        }
                                    case "Percentag-87.5 Savanna":
                                        {
                                            svm.allArray[14, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            svm.allArray[14, 1] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            allSumBreeds[14, 0]++;
                                            allSumBreeds[14, 1]++;
                                            break;
                                        }
                                    case "Percentage-75 Savanna":
                                        {
                                            svm.allArray[15, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            svm.allArray[15, 1] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            allSumBreeds[15, 0]++;
                                            allSumBreeds[15, 1]++;
                                            break;
                                        }
                                    case "Savanna x Boer":
                                        {
                                            svm.allArray[16, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            svm.allArray[16, 1] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            allSumBreeds[16, 0]++;
                                            allSumBreeds[16, 1]++;
                                            break;
                                        }
                                    case "Savanna x Kiko":
                                        {
                                            svm.allArray[17, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            svm.allArray[17, 1] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            allSumBreeds[17, 0]++;
                                            allSumBreeds[17, 1]++;
                                            break;
                                        }
                                    case "Nubian x Spanish":
                                        {
                                            svm.allArray[18, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            svm.allArray[18, 1] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            allSumBreeds[18, 0]++;
                                            allSumBreeds[18, 1]++;
                                            break;
                                        }
                                    case "Savanna x Boer x Kiko":
                                        {
                                            svm.allArray[19, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            svm.allArray[19, 1] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            allSumBreeds[19, 0]++;
                                            allSumBreeds[19, 1]++;
                                            break;
                                        }
                                    case "Angora":
                                        {
                                            svm.allArray[20, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            svm.allArray[20, 1] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            allSumBreeds[20, 0]++;
                                            allSumBreeds[20, 1]++;
                                            break;
                                        }
                                    case "Boer x Angora":
                                        {
                                            svm.allArray[21, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            svm.allArray[21, 1] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            allSumBreeds[21, 0]++;
                                            allSumBreeds[21, 1]++;
                                            break;
                                        }
                                    case "Kiko x Angora":
                                        {
                                            svm.allArray[22, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            svm.allArray[22, 1] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            allSumBreeds[22, 0]++;
                                            allSumBreeds[22, 1]++;
                                            break;
                                        }
                                    case "UNKNOWN CROSS":
                                        {
                                            svm.allArray[23, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            svm.allArray[23, 1] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            allSumBreeds[23, 0]++;
                                            allSumBreeds[23, 1]++;
                                            break;
                                        }
                                    case "OTHERS":
                                        {
                                            svm.allArray[24, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            svm.allArray[24, 1] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            allSumBreeds[24, 0]++;
                                            allSumBreeds[24, 1]++;
                                            break;
                                        }
                                }
                            }
                        }
                        if (animal.post_weaning_date != null && animal.post_weaning_weight != null && animal.weaning_weight != null)
                        {
                            if (((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days > 10)
                            {
                                svm.allMaleADGPostWeaning += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                svm.allADGPostWeaning += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                allADGPWCount++;
                                allMaleADGPWCount++;
                                switch (animal.breed_code)
                                {
                                    case "Purbred Boer":
                                        {
                                            svm.allArray[0, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.allArray[0, 4] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            allSumBreeds[0, 3]++;
                                            allSumBreeds[0, 4]++;
                                            break;
                                        }
                                    case "Percentage-87.5 Boer":
                                        {
                                            svm.allArray[1, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.allArray[1, 4] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            allSumBreeds[1, 3]++;
                                            allSumBreeds[1, 4]++;
                                            break;
                                        }
                                    case "Percentage-75 Boer":
                                        {
                                            svm.allArray[2, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.allArray[2, 4] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            allSumBreeds[2, 3]++;
                                            allSumBreeds[2, 4]++;
                                            break;
                                        }
                                    case "Spanish":
                                        {
                                            svm.allArray[3, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.allArray[3, 4] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            allSumBreeds[3, 3]++;
                                            allSumBreeds[3, 4]++;
                                            break;
                                        }
                                    case "Nubian":
                                        {
                                            svm.allArray[4, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.allArray[4, 4] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            allSumBreeds[4, 3]++;
                                            allSumBreeds[4, 4]++;
                                            break;
                                        }
                                    case "Boer x Spanish":
                                        {
                                            svm.allArray[5, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.allArray[5, 4] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            allSumBreeds[5, 3]++;
                                            allSumBreeds[5, 4]++;
                                            break;
                                        }
                                    case "Pure Kiko":
                                        {
                                            svm.allArray[6, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.allArray[6, 4] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            allSumBreeds[6, 3]++;
                                            allSumBreeds[6, 4]++;
                                            break;
                                        }
                                    case "Percentage-87.5 Kiko":
                                        {
                                            svm.allArray[7, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.allArray[7, 4] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            allSumBreeds[7, 3]++;
                                            allSumBreeds[7, 4]++;
                                            break;
                                        }
                                    case "Percentage-75 Kiko":
                                        {
                                            svm.allArray[8, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.allArray[8, 4] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            allSumBreeds[8, 3]++;
                                            allSumBreeds[8, 4]++;
                                            break;
                                        }
                                    case "Kiko x Spanish":
                                        {
                                            svm.allArray[9, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.allArray[9, 4] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            allSumBreeds[9, 3]++;
                                            allSumBreeds[9, 4]++;
                                            break;
                                        }
                                    case "Kiko x Boer":
                                        {
                                            svm.allArray[10, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.allArray[10, 4] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            allSumBreeds[10, 3]++;
                                            allSumBreeds[10, 4]++;
                                            break;
                                        }
                                    case "Boer X Nubian":
                                        {
                                            svm.allArray[11, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.allArray[11, 4] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            allSumBreeds[11, 3]++;
                                            allSumBreeds[11, 4]++;
                                            break;
                                        }
                                    case "Boer x Spanish x Nubian":
                                        {
                                            svm.allArray[12, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.allArray[12, 4] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            allSumBreeds[12, 3]++;
                                            allSumBreeds[12, 4]++;
                                            break;
                                        }
                                    case "Savanna":
                                        {
                                            svm.allArray[13, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.allArray[13, 4] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            allSumBreeds[13, 3]++;
                                            allSumBreeds[13, 4]++;
                                            break;
                                        }
                                    case "Percentag-87.5 Savanna":
                                        {
                                            svm.allArray[14, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.allArray[14, 4] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            allSumBreeds[14, 3]++;
                                            allSumBreeds[14, 4]++;
                                            break;
                                        }
                                    case "Percentage-75 Savanna":
                                        {
                                            svm.allArray[15, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.allArray[15, 4] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            allSumBreeds[15, 3]++;
                                            allSumBreeds[15, 4]++;
                                            break;
                                        }
                                    case "Savanna x Boer":
                                        {
                                            svm.allArray[16, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.allArray[16, 4] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            allSumBreeds[16, 3]++;
                                            allSumBreeds[16, 4]++;
                                            break;
                                        }
                                    case "Savanna x Kiko":
                                        {
                                            svm.allArray[17, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.allArray[17, 4] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            allSumBreeds[17, 3]++;
                                            allSumBreeds[17, 4]++;
                                            break;
                                        }
                                    case "Nubian x Spanish":
                                        {
                                            svm.allArray[18, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.allArray[18, 4] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            allSumBreeds[18, 3]++;
                                            allSumBreeds[18, 4]++;
                                            break;
                                        }
                                    case "Savanna x Boer x Kiko":
                                        {
                                            svm.allArray[19, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.allArray[19, 4] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            allSumBreeds[19, 3]++;
                                            allSumBreeds[19, 4]++;
                                            break;
                                        }
                                    case "Angora":
                                        {
                                            svm.allArray[20, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.allArray[20, 4] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            allSumBreeds[20, 3]++;
                                            allSumBreeds[20, 4]++;
                                            break;
                                        }
                                    case "Boer x Angora":
                                        {
                                            svm.allArray[21, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.allArray[21, 4] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            allSumBreeds[21, 3]++;
                                            allSumBreeds[21, 4]++;
                                            break;
                                        }
                                    case "Kiko x Angora":
                                        {
                                            svm.allArray[22, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.allArray[22, 4] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            allSumBreeds[22, 3]++;
                                            allSumBreeds[22, 4]++;
                                            break;
                                        }
                                    case "UNKNOWN CROSS":
                                        {
                                            svm.allArray[23, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.allArray[23, 4] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            allSumBreeds[23, 3]++;
                                            allSumBreeds[23, 4]++;
                                            break;
                                        }
                                    case "OTHERS":
                                        {
                                            svm.allArray[24, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.allArray[24, 4] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            allSumBreeds[24, 3]++;
                                            allSumBreeds[24, 4]++;
                                            break;
                                        }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (animal.weaning_date != null && animal.weaning_weight != null && animal.weaning_date != null)
                        {
                            if (((DateTime)animal.weaning_date - animal.dob).Days > 10)
                            {
                                svm.allFemaleADGWeaning += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                svm.allADGWeaning += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                allADGWCount++;
                                allFemaleADGWCount++;
                                switch (animal.breed_code)
                                {
                                    case "Purbred Boer":
                                        {
                                            svm.allArray[0, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            svm.allArray[0, 2] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            allSumBreeds[0, 0]++;
                                            allSumBreeds[0, 2]++;
                                            break;
                                        }
                                    case "Percentage-87.5 Boer":
                                        {
                                            svm.allArray[1, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            svm.allArray[1, 2] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            allSumBreeds[1, 0]++;
                                            allSumBreeds[1, 2]++;
                                            break;
                                        }
                                    case "Percentage-75 Boer":
                                        {
                                            svm.allArray[2, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            svm.allArray[2, 2] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            allSumBreeds[2, 0]++;
                                            allSumBreeds[2, 2]++;
                                            break;
                                        }
                                    case "Spanish":
                                        {
                                            svm.allArray[3, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            svm.allArray[3, 2] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            allSumBreeds[3, 0]++;
                                            allSumBreeds[3, 2]++;
                                            break;
                                        }
                                    case "Nubian":
                                        {
                                            svm.allArray[4, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            svm.allArray[4, 2] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            allSumBreeds[4, 0]++;
                                            allSumBreeds[4, 2]++;
                                            break;
                                        }
                                    case "Boer x Spanish":
                                        {
                                            svm.allArray[5, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            svm.allArray[5, 2] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            allSumBreeds[5, 0]++;
                                            allSumBreeds[5, 2]++;
                                            break;
                                        }
                                    case "Pure Kiko":
                                        {
                                            svm.allArray[6, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            svm.allArray[6, 2] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            allSumBreeds[6, 0]++;
                                            allSumBreeds[6, 2]++;
                                            break;
                                        }
                                    case "Percentage-87.5 Kiko":
                                        {
                                            svm.allArray[7, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            svm.allArray[7, 2] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            allSumBreeds[7, 0]++;
                                            allSumBreeds[7, 2]++;
                                            break;
                                        }
                                    case "Percentage-75 Kiko":
                                        {
                                            svm.allArray[8, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            svm.allArray[8, 2] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            allSumBreeds[8, 0]++;
                                            allSumBreeds[8, 2]++;
                                            break;
                                        }
                                    case "Kiko x Spanish":
                                        {
                                            svm.allArray[9, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            svm.allArray[9, 2] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            allSumBreeds[9, 0]++;
                                            allSumBreeds[9, 2]++;
                                            break;
                                        }
                                    case "Kiko x Boer":
                                        {
                                            svm.allArray[10, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            svm.allArray[10, 2] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            allSumBreeds[10, 0]++;
                                            allSumBreeds[10, 2]++;
                                            break;
                                        }
                                    case "Boer X Nubian":
                                        {
                                            svm.allArray[11, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            svm.allArray[11, 2] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            allSumBreeds[11, 0]++;
                                            allSumBreeds[11, 2]++;
                                            break;
                                        }
                                    case "Boer x Spanish x Nubian":
                                        {
                                            svm.allArray[12, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            svm.allArray[12, 2] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            allSumBreeds[12, 0]++;
                                            allSumBreeds[12, 2]++;
                                            break;
                                        }
                                    case "Savanna":
                                        {
                                            svm.allArray[13, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            svm.allArray[13, 2] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            allSumBreeds[13, 0]++;
                                            allSumBreeds[13, 2]++;
                                            break;
                                        }
                                    case "Percentag-87.5 Savanna":
                                        {
                                            svm.allArray[14, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            svm.allArray[14, 2] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            allSumBreeds[14, 0]++;
                                            allSumBreeds[14, 2]++;
                                            break;
                                        }
                                    case "Percentage-75 Savanna":
                                        {
                                            svm.allArray[15, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            svm.allArray[15, 2] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            allSumBreeds[15, 0]++;
                                            allSumBreeds[15, 2]++;
                                            break;
                                        }
                                    case "Savanna x Boer":
                                        {
                                            svm.allArray[16, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            svm.allArray[16, 2] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            allSumBreeds[16, 0]++;
                                            allSumBreeds[16, 2]++;
                                            break;
                                        }
                                    case "Savanna x Kiko":
                                        {
                                            svm.allArray[17, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            svm.allArray[17, 2] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            allSumBreeds[17, 0]++;
                                            allSumBreeds[17, 2]++;
                                            break;
                                        }
                                    case "Nubian x Spanish":
                                        {
                                            svm.allArray[18, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            svm.allArray[18, 2] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            allSumBreeds[18, 0]++;
                                            allSumBreeds[18, 2]++;
                                            break;
                                        }
                                    case "Savanna x Boer x Kiko":
                                        {
                                            svm.allArray[19, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            svm.allArray[19, 2] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            allSumBreeds[19, 0]++;
                                            allSumBreeds[19, 2]++;
                                            break;
                                        }
                                    case "Angora":
                                        {
                                            svm.allArray[20, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            svm.allArray[20, 2] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            allSumBreeds[20, 0]++;
                                            allSumBreeds[20, 2]++;
                                            break;
                                        }
                                    case "Boer x Angora":
                                        {
                                            svm.allArray[21, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            svm.allArray[21, 2] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            allSumBreeds[21, 0]++;
                                            allSumBreeds[21, 2]++;
                                            break;
                                        }
                                    case "Kiko x Angora":
                                        {
                                            svm.allArray[22, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            svm.allArray[22, 2] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            allSumBreeds[22, 0]++;
                                            allSumBreeds[22, 2]++;
                                            break;
                                        }
                                    case "UNKNOWN CROSS":
                                        {
                                            svm.allArray[23, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            svm.allArray[23, 2] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            allSumBreeds[23, 0]++;
                                            allSumBreeds[23, 2]++;
                                            break;
                                        }
                                    case "OTHERS":
                                        {
                                            svm.allArray[24, 0] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            svm.allArray[24, 2] += (double)(animal.weaning_weight - animal.birth_weight) / ((DateTime)animal.weaning_date - animal.dob).Days;
                                            allSumBreeds[24, 0]++;
                                            allSumBreeds[24, 2]++;
                                            break;
                                        }
                                }
                            }

                        }
                        if (animal.post_weaning_date != null && animal.post_weaning_weight != null && animal.weaning_date != null)
                        {
                            if (((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days > 10)
                            {
                                svm.allFemaleADGPostWeaning += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                svm.allADGPostWeaning += (double)(animal.post_weaning_weight - animal.birth_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                allADGPWCount++;
                                allFemaleADGPWCount++;
                                switch (animal.breed_code)
                                {
                                    case "Purbred Boer":
                                        {
                                            svm.allArray[0, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.allArray[0, 5] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            allSumBreeds[0, 3]++;
                                            allSumBreeds[0, 5]++;
                                            break;
                                        }
                                    case "Percentage-87.5 Boer":
                                        {
                                            svm.allArray[1, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.allArray[1, 5] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            allSumBreeds[1, 3]++;
                                            allSumBreeds[1, 5]++;
                                            break;
                                        }
                                    case "Percentage-75 Boer":
                                        {
                                            svm.allArray[2, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.allArray[2, 5] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            allSumBreeds[2, 3]++;
                                            allSumBreeds[2, 5]++;
                                            break;
                                        }
                                    case "Spanish":
                                        {
                                            svm.allArray[3, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.allArray[3, 5] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            allSumBreeds[3, 3]++;
                                            allSumBreeds[3, 5]++;
                                            break;
                                        }
                                    case "Nubian":
                                        {
                                            svm.allArray[4, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.allArray[4, 5] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            allSumBreeds[4, 3]++;
                                            allSumBreeds[4, 5]++;
                                            break;
                                        }
                                    case "Boer x Spanish":
                                        {
                                            svm.allArray[5, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.allArray[5, 5] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            allSumBreeds[5, 3]++;
                                            allSumBreeds[5, 5]++;
                                            break;
                                        }
                                    case "Pure Kiko":
                                        {
                                            svm.allArray[6, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.allArray[6, 5] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            allSumBreeds[6, 3]++;
                                            allSumBreeds[6, 5]++;
                                            break;
                                        }
                                    case "Percentage-87.5 Kiko":
                                        {
                                            svm.allArray[7, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.allArray[7, 5] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            allSumBreeds[7, 3]++;
                                            allSumBreeds[7, 5]++;
                                            break;
                                        }
                                    case "Percentage-75 Kiko":
                                        {
                                            svm.allArray[8, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.allArray[8, 5] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            allSumBreeds[8, 3]++;
                                            allSumBreeds[8, 5]++;
                                            break;
                                        }
                                    case "Kiko x Spanish":
                                        {
                                            svm.allArray[9, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.allArray[9, 5] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            allSumBreeds[9, 3]++;
                                            allSumBreeds[9, 5]++;
                                            break;
                                        }
                                    case "Kiko x Boer":
                                        {
                                            svm.allArray[10, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.allArray[10, 5] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            allSumBreeds[10, 3]++;
                                            allSumBreeds[10, 5]++;
                                            break;
                                        }
                                    case "Boer X Nubian":
                                        {
                                            svm.allArray[11, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.allArray[11, 5] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            allSumBreeds[11, 3]++;
                                            allSumBreeds[11, 5]++;
                                            break;
                                        }
                                    case "Boer x Spanish x Nubian":
                                        {
                                            svm.allArray[12, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.allArray[12, 5] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            allSumBreeds[12, 3]++;
                                            allSumBreeds[12, 5]++;
                                            break;
                                        }
                                    case "Savanna":
                                        {
                                            svm.allArray[13, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.allArray[13, 5] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            allSumBreeds[13, 3]++;
                                            allSumBreeds[13, 5]++;
                                            break;
                                        }
                                    case "Percentag-87.5 Savanna":
                                        {
                                            svm.allArray[14, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.allArray[14, 5] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            allSumBreeds[14, 3]++;
                                            allSumBreeds[14, 5]++;
                                            break;
                                        }
                                    case "Percentage-75 Savanna":
                                        {
                                            svm.allArray[15, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.allArray[15, 5] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            allSumBreeds[15, 3]++;
                                            allSumBreeds[15, 5]++;
                                            break;
                                        }
                                    case "Savanna x Boer":
                                        {
                                            svm.allArray[16, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.allArray[16, 5] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            allSumBreeds[16, 3]++;
                                            allSumBreeds[16, 5]++;
                                            break;
                                        }
                                    case "Savanna x Kiko":
                                        {
                                            svm.allArray[17, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.allArray[17, 5] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            allSumBreeds[17, 3]++;
                                            allSumBreeds[17, 5]++;
                                            break;
                                        }
                                    case "Nubian x Spanish":
                                        {
                                            svm.allArray[18, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.allArray[18, 5] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            allSumBreeds[18, 3]++;
                                            allSumBreeds[18, 5]++;
                                            break;
                                        }
                                    case "Savanna x Boer x Kiko":
                                        {
                                            svm.allArray[19, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.allArray[19, 5] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            allSumBreeds[19, 3]++;
                                            allSumBreeds[19, 5]++;
                                            break;
                                        }
                                    case "Angora":
                                        {
                                            svm.allArray[20, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.allArray[20, 5] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            allSumBreeds[20, 3]++;
                                            allSumBreeds[20, 5]++;
                                            break;
                                        }
                                    case "Boer x Angora":
                                        {
                                            svm.allArray[21, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.allArray[21, 5] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            allSumBreeds[21, 3]++;
                                            allSumBreeds[21, 5]++;
                                            break;
                                        }
                                    case "Kiko x Angora":
                                        {
                                            svm.allArray[22, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.allArray[22, 5] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            allSumBreeds[22, 3]++;
                                            allSumBreeds[22, 5]++;
                                            break;
                                        }
                                    case "UNKNOWN CROSS":
                                        {
                                            svm.allArray[23, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.allArray[23, 5] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            allSumBreeds[23, 3]++;
                                            allSumBreeds[23, 5]++;
                                            break;
                                        }
                                    case "OTHERS":
                                        {
                                            svm.allArray[24, 3] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            svm.allArray[24, 5] += (double)(animal.post_weaning_weight - animal.weaning_weight) / ((DateTime)animal.post_weaning_date - ((DateTime)animal.weaning_date)).Days;
                                            allSumBreeds[24, 3]++;
                                            allSumBreeds[24, 5]++;
                                            break;
                                        }
                                }
                            }
                        }
                    }
                }
            }
            double temp;
            for (int i = 0; i < 25; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    if (mySumBreeds[i, j] == 0)
                    {
                        svm.myArray[i, j] = 0;
                    }
                    else
                    {
                        temp = svm.myArray[i, j] / mySumBreeds[i, j];
                        temp = temp * 1000;
                        temp = Math.Truncate(temp);
                        temp = temp / 1000;
                        svm.myArray[i, j] = temp;
                    }
                    if (allSumBreeds[i, j] == 0)
                    {
                        svm.allArray[i, j] = 0;
                    }
                    else
                    {
                        temp = svm.allArray[i, j] / allSumBreeds[i, j];
                        temp = temp * 1000;
                        temp = Math.Truncate(temp);
                        temp = temp / 1000;
                        svm.allArray[i, j] = temp;
                    }
                }
            }
            if (maleBWCount == 0)
            {
                maleBWCount++;
            }
            temp = svm.maleAvgBW / maleBWCount;
            temp = temp * 1000;
            temp = Math.Truncate(temp);
            temp = temp / 1000;
            svm.myArray[25, 7] = temp;

            if (femaleBWCount == 0)
            {
                femaleBWCount++;
            }
            temp = svm.femaleAvgBW / femaleBWCount;
            temp = temp * 1000;
            temp = Math.Truncate(temp);
            temp = temp / 1000;
            svm.myArray[25, 8] = temp;

            if (maleWWCount == 0)
            {
                maleWWCount++;
            }
            temp = svm.maleAvgWW / maleWWCount;
            temp = temp * 1000;
            temp = Math.Truncate(temp);
            temp = temp / 1000;
            svm.myArray[25, 10] = temp;

            if (femaleWWCount == 0)
            {
                femaleWWCount++;
            }
            temp = svm.femaleAvgWW / femaleWWCount;
            temp = temp * 1000;
            temp = Math.Truncate(temp);
            temp = temp / 1000;
            svm.myArray[25, 11] = temp;

            temp = svm.myArray[25, 6];
            temp = temp * 1000;
            temp = Math.Truncate(temp);
            temp = temp / 1000;
            svm.myArray[25, 6] = temp;
            temp = svm.myArray[25, 7];
            temp = temp * 1000;
            temp = Math.Truncate(temp);
            temp = temp / 1000;
            svm.myArray[25, 7] = temp;
            
            if (svm.myArray[25, 7] == 0 || svm.myArray[25, 8] == 0)
            {
                if (svm.myArray[25, 7] == 0)
                {
                    svm.myArray[25, 6] = svm.myArray[25, 8];
                }
                else
                {
                    svm.myArray[25, 6] = svm.myArray[25, 7];
                }
            }
            else
            {
                temp =  (svm.myArray[25, 7] + svm.myArray[25, 8]) / 2;
                temp = temp * 1000;
                temp = Math.Truncate(temp);
                temp = temp / 1000;
                svm.myArray[25, 6] = temp;
            }

            if (svm.myArray[25, 10] == 0 || svm.myArray[25, 11] == 0)
            {
                if (svm.myArray[25, 10] == 0)
                {
                    svm.myArray[25, 9] = svm.myArray[25, 11];
                }
                else
                {
                    svm.myArray[25, 9] = svm.myArray[25, 10];
                }
            }
            else
            {
                temp = (svm.myArray[25, 10] + svm.myArray[25, 11]) / 2;
                temp = temp * 1000;
                temp = Math.Truncate(temp);
                temp = temp / 1000;
                svm.myArray[25, 9] = temp;
            }

            if (svm.singleBirthCount == 0)
            {
                svm.singleBWAvg = 0;
            }
            else
            {
                temp = svm.singleBWAvg / svm.singleBirthCount;
                temp = temp * 1000;
                temp = Math.Truncate(temp);
                temp = temp / 1000;
                svm.singleBWAvg = temp;
            }

            if (svm.singleWeaningCount == 0)
            {
                svm.singleWWAvg = 0;
            }
            else
            {
                temp = svm.singleWWAvg / svm.singleWeaningCount;
                temp = temp * 1000;
                temp = Math.Truncate(temp);
                temp = temp / 1000;
                svm.singleWWAvg = temp;
            }

            if (svm.twinBirthCount == 0)
            {
                svm.twinBWAvg = 0;
            }
            else
            {
                temp = svm.twinBWAvg / svm.twinBirthCount;
                temp = temp * 1000;
                temp = Math.Truncate(temp);
                temp = temp / 1000;
                svm.twinBWAvg = temp;
            }

            if (svm.twinWeaningCount == 0)
            {
                svm.twinWWAvg = 0;
            }
            else
            {
                temp = svm.twinWWAvg / svm.twinWeaningCount;
                temp = temp * 1000;
                temp = Math.Truncate(temp);
                temp = temp / 1000;
                svm.twinWWAvg = temp;
            }

            if (svm.tripletBirthCount == 0)
            {
                svm.tripletBWAvg = 0;
            }
            else
            {
                temp = svm.tripletBWAvg / svm.tripletBirthCount;
                temp = temp * 1000;
                temp = Math.Truncate(temp);
                temp = temp / 1000;
                svm.tripletBWAvg = temp;
            }

            if (svm.tripletWeaningCount == 0)
            {
                svm.tripletWWAvg = 0;
            }
            else
            {
                temp = svm.tripletWWAvg / svm.tripletWeaningCount;
                temp = temp * 1000;
                temp = Math.Truncate(temp);
                temp = temp / 1000;
                svm.tripletWWAvg = temp;
            }

            if (svm.quadBirthCount == 0)
            {
                svm.quadBWAvg = 0;
            }
            else
            {
                temp = svm.quadBWAvg / svm.quadBirthCount;
                temp = temp * 1000;
                temp = Math.Truncate(temp);
                temp = temp / 1000;
                svm.quadBWAvg = temp;
            }

            if (svm.quadWeaningCount == 0)
            {
                svm.quadWWAvg = 0;
            }
            else
            {
                temp = svm.quadWWAvg / svm.quadWeaningCount;
                temp = temp * 1000;
                temp = Math.Truncate(temp);
                temp = temp / 1000;
                svm.quadWWAvg = temp;
            }

            if (parBW1Count == 0 || parWW1Count == 0)
            {
                if (parBW1Count == 0)
                {
                    svm.damParity1BW = 0;
                    if (svm.damParity1WW != 0)
                    {
                        temp = svm.damParity1WW / parWW1Count;
                        temp = temp * 1000;
                        temp = Math.Truncate(temp);
                        temp = temp / 1000;
                        svm.damParity1WW = temp;
                    }
                    else
                    {
                        svm.damParity1WW = 0;
                    }
                }
                else
                {
                    svm.damParity1WW = 0;
                    temp = svm.damParity1BW / parBW1Count;
                    temp = temp * 1000;
                    temp = Math.Truncate(temp);
                    temp = temp / 1000;
                    svm.damParity1BW = temp;
                }
            }
            else
            {
                temp = svm.damParity1BW / parBW1Count;
                temp = temp * 1000;
                temp = Math.Truncate(temp);
                temp = temp / 1000;
                svm.damParity1BW = temp;

                temp = svm.damParity1WW / parWW1Count;
                temp = temp * 1000;
                temp = Math.Truncate(temp);
                temp = temp / 1000;
                svm.damParity1WW = temp;
            }

            if (parBW2Count == 0 || parWW2Count == 0)
            {
                if (parBW2Count == 0)
                {
                    svm.damParity2BW = 0;
                    if (svm.damParity2WW != 0)
                    {
                        temp = svm.damParity2WW / parWW2Count;
                        temp = temp * 1000;
                        temp = Math.Truncate(temp);
                        temp = temp / 1000;
                        svm.damParity2WW = temp;
                    }
                    else
                    {
                        svm.damParity2WW = 0;
                    }
                }
                else
                {
                    svm.damParity2WW = 0;
                    temp = svm.damParity2BW / parBW2Count;
                    temp = temp * 1000;
                    temp = Math.Truncate(temp);
                    temp = temp / 1000;
                    svm.damParity2BW = temp;
                }
            }
            else
            {
                temp = svm.damParity2BW / parBW2Count;
                temp = temp * 1000;
                temp = Math.Truncate(temp);
                temp = temp / 1000;
                svm.damParity2BW = temp;

                temp = svm.damParity2WW / parBW2Count;
                temp = temp * 1000;
                temp = Math.Truncate(temp);
                temp = temp / 1000;
                svm.damParity2WW = temp;
            }

            if (parBW3Count == 0 || parWW3Count == 0)
            {
                if (parBW3Count == 0)
                {
                    svm.damParity3BW = 0;
                    if (svm.damParity3WW != 0)
                    {
                        temp = svm.damParity3WW / parWW3Count;
                        temp = temp * 1000;
                        temp = Math.Truncate(temp);
                        temp = temp / 1000;
                        svm.damParity3WW = temp;
                    }
                    else
                    {
                        svm.damParity3WW = 0;
                    }
                }
                else
                {
                    svm.damParity3WW = 0;
                    temp = svm.damParity3BW / parBW3Count;
                    temp = temp * 1000;
                    temp = Math.Truncate(temp);
                    temp = temp / 1000;
                    svm.damParity3BW = temp;
                }
            }
            else
            {
                temp = svm.damParity3BW / parBW3Count;
                temp = temp * 1000;
                temp = Math.Truncate(temp);
                temp = temp / 1000;
                svm.damParity3BW = temp;

                temp = svm.damParity3WW / parBW3Count;
                temp = temp * 1000;
                temp = Math.Truncate(temp);
                temp = temp / 1000;
                svm.damParity3WW = temp;
            }

            if (parBW4Count == 0 || parWW4Count == 0)
            {
                if (parBW4Count == 0)
                {
                    svm.damParity4BW = 0;
                    if (svm.damParity4WW != 0)
                    {
                        temp = svm.damParity4WW / parWW4Count;
                        temp = temp * 1000;
                        temp = Math.Truncate(temp);
                        temp = temp / 1000;
                        svm.damParity4WW = temp;
                    }
                    else
                    {
                        svm.damParity4WW = 0;
                    }
                }
                else
                {
                    svm.damParity4WW = 0;
                    temp = svm.damParity4BW / parBW4Count;
                    temp = temp * 1000;
                    temp = Math.Truncate(temp);
                    temp = temp / 1000;
                    svm.damParity4BW = temp;
                }
            }
            else
            {
                temp = svm.damParity4BW / parBW4Count;
                temp = temp * 1000;
                temp = Math.Truncate(temp);
                temp = temp / 1000;
                svm.damParity4BW = temp;

                temp = svm.damParity4WW / parBW4Count;
                temp = temp * 1000;
                temp = Math.Truncate(temp);
                temp = temp / 1000;
                svm.damParity4WW = temp;
            }

            if (allADGWCount == 0)
            {
                allADGWCount++;
            }
            svm.allADGWeaning = svm.allADGWeaning / allADGWCount;

            if (allADGPWCount == 0)
            {
                allADGPWCount++;
            }
            svm.allADGPostWeaning = svm.allADGPostWeaning / allADGPWCount;

            if (allMaleADGWCount == 0)
            {
                allMaleADGWCount++;
            }
            svm.allMaleADGWeaning = svm.allMaleADGWeaning / allMaleADGWCount;

            if (allMaleADGPWCount == 0)
            {
                allMaleADGPWCount++;
            }
            svm.allMaleADGPostWeaning = svm.allMaleADGPostWeaning / allMaleADGPWCount;

            if (allFemaleADGWCount == 0)
            {
                allFemaleADGWCount++;
            }
            svm.allFemaleADGWeaning = svm.allFemaleADGWeaning / allFemaleADGWCount;

            if (allFemaleADGPWCount == 0)
            {
                allFemaleADGPWCount++;
            }
            svm.allFemaleADGPostWeaning = svm.allFemaleADGPostWeaning / allFemaleADGPWCount;

            if (ADGWCount == 0)
            {
                ADGWCount++;
            }
            svm.ADGWeaning = svm.ADGWeaning / ADGWCount;

            if (ADGPWCount == 0)
            {
                ADGPWCount++;
            }
            svm.ADGPostWeaning = svm.ADGPostWeaning / ADGPWCount;

            if (MaleADGWCount == 0)
            {
                MaleADGWCount++;
            }
            svm.maleADGWeaning = svm.maleADGWeaning / MaleADGWCount;

            if (MaleADGPWCount == 0)
            {
                MaleADGPWCount++;
            }
            svm.maleADGPostWeaning = svm.maleADGPostWeaning / MaleADGPWCount;

            if (FemaleADGWCount == 0)
            {
                FemaleADGWCount++;
            }
            svm.femaleADGWeaning = svm.femaleADGWeaning / FemaleADGWCount;
            
            if (FemaleADGPWCount == 0)
            {
                FemaleADGPWCount++;
            }
            svm.femaleADGPostWeaning = svm.femaleADGPostWeaning / FemaleADGPWCount;

            temp = svm.ADGWeaning;
            temp = temp * 1000;
            temp = Math.Truncate(temp);
            temp = temp / 1000;
            svm.myArray[25, 0] = temp;

            temp = svm.maleADGWeaning;
            temp = temp * 1000;
            temp = Math.Truncate(temp);
            temp = temp / 1000;
            svm.myArray[25, 1] = temp;

            temp = svm.femaleADGWeaning;
            temp = temp * 1000;
            temp = Math.Truncate(temp);
            temp = temp / 1000;
            svm.myArray[25, 2] = temp;

            temp = svm.ADGPostWeaning;
            temp = temp * 1000;
            temp = Math.Truncate(temp);
            temp = temp / 1000;
            svm.myArray[25, 3] = temp;

            temp = svm.maleADGPostWeaning;
            temp = temp * 1000;
            temp = Math.Truncate(temp);
            temp = temp / 1000;
            svm.myArray[25, 4] = temp;

            temp = svm.femaleADGPostWeaning;
            temp = temp * 1000;
            temp = Math.Truncate(temp);
            temp = temp / 1000;
            svm.myArray[25, 5] = temp;

            temp = svm.allADGWeaning;
            temp = temp * 1000;
            temp = Math.Truncate(temp);
            temp = temp / 1000;
            svm.allArray[25, 0] = temp;

            temp = svm.allMaleADGWeaning;
            temp = temp * 1000;
            temp = Math.Truncate(temp);
            temp = temp / 1000;
            svm.allArray[25, 1] = temp;

            temp = svm.allFemaleADGWeaning;
            temp = temp * 1000;
            temp = Math.Truncate(temp);
            temp = temp / 1000;
            svm.allArray[25, 2] = temp;

            temp = svm.allADGPostWeaning;
            temp = temp * 1000;
            temp = Math.Truncate(temp);
            temp = temp / 1000;
            svm.allArray[25, 3] = temp;

            temp = svm.allMaleADGPostWeaning;
            temp = temp * 1000;
            temp = Math.Truncate(temp);
            temp = temp / 1000;
            svm.allArray[25, 4] = temp;

            temp = svm.allFemaleADGPostWeaning;
            temp = temp * 1000;
            temp = Math.Truncate(temp);
            temp = temp / 1000;
            svm.allArray[25, 5] = temp;
            List<SelectListItem> graphList = new List<SelectListItem>()
            {
                new SelectListItem() { Text = "General", Value = "General"},
                new SelectListItem() { Text = "Weaning", Value = "Weaning"},
                new SelectListItem() { Text = "Birth", Value = "Birth"}
            };
            @ViewBag.GraphList = graphList;
            List<SelectListItem> breedList = new List<SelectListItem>() {
                new SelectListItem() { Text = "ALL BREEDS", Value = "25"},
                new SelectListItem() { Text = "Purbred Boer", Value = "0"},
                new SelectListItem() { Text = "Percentage-87.5 Boer", Value = "1"},
                new SelectListItem() { Text = "Percentage-75 Boer", Value = "2"},
                new SelectListItem() { Text = "Spanish", Value = "3"},
                new SelectListItem() { Text = "Nubian", Value = "4"},
                new SelectListItem() { Text = "Boer x Spanish", Value = "5"},
                new SelectListItem() { Text = "Pure Kiko", Value = "6"},
                new SelectListItem() { Text = "Percentage-87.5 Kiko", Value = "7"},
                new SelectListItem() { Text = "Percentage-75 Kiko", Value = "8"},
                new SelectListItem() { Text = "Kiko x Spanish", Value = "9"},
                new SelectListItem() { Text = "Kiko x Boer", Value = "10"},
                new SelectListItem() { Text = "Boer X Nubian", Value = "11"},
                new SelectListItem() { Text = "Boer x Spanish x Nubian", Value = "12"},
                new SelectListItem() { Text = "Savanna", Value = "13"},
                new SelectListItem() { Text = "Percentag-87.5 Savanna", Value = "14"},
                new SelectListItem() { Text = "Percentage-75 Savanna", Value = "15"},
                new SelectListItem() { Text = "Savanna x Boer", Value = "16"},
                new SelectListItem() { Text = "Savanna x Kiko", Value = "17"},
                new SelectListItem() { Text = "Nubian x Spanish", Value = "18"},
                new SelectListItem() { Text = "Savanna x Boer x Kiko", Value = "19"},
                new SelectListItem() { Text = "Angora", Value = "20"},
                new SelectListItem() { Text = "Boer x Angora", Value = "21"},
                new SelectListItem() { Text = "Kiko x Angora", Value = "22"},
                new SelectListItem() { Text = "UNKNOWN CROSS", Value = "23"},
                new SelectListItem() { Text = "OTHERS", Value = "24"},
            };
            @ViewBag.breedList = breedList;
            return View(svm);
        }

        [Authorize]
        public ActionResult Export()
        {
            ExcelPackage package = new ExcelPackage();
            int userID = (int)Membership.GetUser().ProviderUserKey;
            //Create Animals
            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Animals");
            worksheet.Cells[1, 1].Value = "Species";
            worksheet.Cells[1, 2].Value = "Name";
            worksheet.Cells[1, 3].Value = "Tag";
            worksheet.Cells[1, 4].Value = "Date of Birth";
            worksheet.Cells[1, 5].Value = "Sex";
            worksheet.Cells[1, 6].Value = "Maturity";
            worksheet.Cells[1, 7].Value = "Breed Code";
            worksheet.Cells[1, 8].Value = "Status";
            worksheet.Cells[1, 9].Value = "Registration Number";
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
            worksheet.Cells[1, 22].Value = "FAMACHA Score";
            worksheet.Cells[1, 23].Value = "Fecal Egg Count";
            worksheet.Cells[1, 24].Value = "Hair Coat Score";
            worksheet.Cells[1, 25].Value = "Body Condition Score";
            worksheet.Cells[1, 26].Value = "Comments";

            // populate a row for each animal
            List<Animal> animals = db.Animals.Where(animal => animal.owner == userID).ToList();
            if (User.IsInRole("admin"))
            {
                animals = db.Animals.ToList();
            }
            int row = 2;
            foreach (Animal animal in animals)
            {
                worksheet.Cells[row, 1].Value = animal.species;
                worksheet.Cells[row, 2].Value = animal.name;
                worksheet.Cells[row, 3].Value = animal.tag;
                worksheet.Cells[row, 4].Value = animal.dob.ToShortDateString();
                if (animal.sex) {
                    worksheet.Cells[row, 5].Value = "Male";
                }
                else {
                    worksheet.Cells[row, 5].Value = "Female";
                }
                if (animal.isChild)
                {
                    worksheet.Cells[row, 6].Value = "Offspring";
                }
                else
                {
                    worksheet.Cells[row, 6].Value = "Adult";
                }
                worksheet.Cells[row, 7].Value = animal.breed_code;
                worksheet.Cells[row, 8].Value = animal.status_code;
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
                worksheet.Cells[row, 22].Value = animal.FAMACHA_score;
                worksheet.Cells[row, 23].Value = animal.fecal_egg_count;
                worksheet.Cells[row, 24].Value = animal.hair_coat_score;
                worksheet.Cells[row, 25].Value = animal.body_condition_score;
                worksheet.Cells[row, 26].Value = animal.remarks;
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
            if (User.IsInRole("admin"))
            {
                treatments = db.Treatments.ToList();
            }
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
            if (User.IsInRole("admin"))
            {
                transactions = db.Transactions.ToList();
            }
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
            worksheet = package.Workbook.Worksheets.Add("Business Contacts");
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
            if (User.IsInRole("admin"))
            {
                associates = db.Associates.ToList();
            }
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
            worksheet.Cells[1, 1].Value = "Dam's Tag";
            worksheet.Cells[1, 2].Value = "Sire's Tag";
            worksheet.Cells[1, 3].Value = "Breeding Date";
            worksheet.Cells[1, 4].Value = "Pregnant";
            worksheet.Cells[1, 5].Value = "Expected Birthing Date";
            worksheet.Cells[1, 6].Value = "Actual Birthing Date";
            worksheet.Cells[1, 7].Value = "Remarks";

            List<Breeding> breedings = db.Breedings.Where(breeding => breeding.Animal.owner == userID).ToList();
            if (User.IsInRole("admin"))
            {
                breedings = db.Breedings.ToList();
            }
            row = 2;
            foreach (Breeding breeding in breedings)
            {
                worksheet.Cells[row, 1].Value = db.Animals.Find(breeding.mother_id).tag;
                worksheet.Cells[row, 2].Value = db.Animals.Find(breeding.father_id).tag;
                worksheet.Cells[row, 3].Value = breeding.date == null ? "" : ((DateTime)breeding.date).ToShortDateString(); ;
                if (breeding.pregnancy_check)
                {
                    worksheet.Cells[row, 4].Value = "Yes";
                }
                else
                {
                    worksheet.Cells[row, 4].Value = "No";
                }
                worksheet.Cells[row, 5].Value = breeding.expected_kidding_date == null ? "" : ((DateTime)breeding.expected_kidding_date).ToShortDateString();
                worksheet.Cells[row, 6].Value = breeding.actual_birthing_date == null ? "" : ((DateTime)breeding.actual_birthing_date).ToShortDateString();
                worksheet.Cells[row, 7].Value = breeding.remarks;
                row++;
            }
            worksheet.Cells.AutoFitColumns(0);

            //create Births
            worksheet = package.Workbook.Worksheets.Add("Birth_Records");
            worksheet.Cells[1, 1].Value = "Offspring's Tag";
            worksheet.Cells[1, 2].Value = "Dam's Tag";
            worksheet.Cells[1, 3].Value = "Sire's Tag";
            worksheet.Cells[1, 4].Value = "Date of Birth";
            worksheet.Cells[1, 5].Value = "Score";
            worksheet.Cells[1, 6].Value = "Dam's Parity";
            worksheet.Cells[1, 7].Value = "Alive";
            worksheet.Cells[1, 8].Value = "Born";
            worksheet.Cells[1, 9].Value = "Notes";

            List<Birth> births = db.Births.Where(birth => birth.Animal.owner == userID).ToList();
            if (User.IsInRole("admin"))
            {
                births = db.Births.ToList();
            }
            row = 2;
            foreach (Birth birth in births)
            {
                worksheet.Cells[row, 1].Value = db.Animals.Find(birth.child_id).tag;
                worksheet.Cells[row, 2].Value = db.Animals.Find(db.Breedings.Find(birth.breed_id).mother_id).tag;
                worksheet.Cells[row, 3].Value = db.Animals.Find(db.Breedings.Find(birth.breed_id).father_id).tag;
                worksheet.Cells[row, 4].Value = db.Animals.Find(birth.child_id).dob;
                worksheet.Cells[row, 5].Value = birth.score;
                worksheet.Cells[row, 6].Value = db.Breedings.Find(birth.breed_id).parity;
                worksheet.Cells[row, 7].Value = db.Breedings.Find(birth.breed_id).alive;
                worksheet.Cells[row, 8].Value = db.Breedings.Find(birth.breed_id).born;
                worksheet.Cells[row, 9].Value = birth.notes;
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