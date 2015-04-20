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
    public class TransactionController : Controller
    {
        private goatDBEntities db = new goatDBEntities();

        //
        // GET: /Transaction/
        public ActionResult Index()
        {
            int userID = (int)Membership.GetUser().ProviderUserKey;
            List<Transaction> myTransactions = db.Transactions.Where(m => m.userid == userID).ToList();
            if (User.IsInRole("admin"))
            {
                myTransactions = db.Transactions.ToList();
            }
            return View(myTransactions);
        }

        //
        // GET: /Transaction/Compare
        public ActionResult Compare()
        {
            int userID = (int)Membership.GetUser().ProviderUserKey;
            var incomes = db.Transactions.Where(m => m.type == true && m.userid == userID).ToList();
            double incomeTotal = 0;
            double a = 0;
            double b = 0;
            double c = 0;
            double d = 0;
            double e = 0;
            double f = 0;
            double g = 0;
            double h = 0;
            double i = 0;
            double j = 0;
            double k = 0;
            double l = 0;
            double x = 0;
            double n = 0;
            if (User.IsInRole("admin"))
            {
                incomes = db.Transactions.Where(m => m.type == true).ToList();
            }
            foreach (Transaction trans in incomes)
            {
                incomeTotal += (double)trans.total_payment;
                if (trans.item_type == "Sale of Meat Kids")
                {
                    a += (double)trans.total_payment;
                }
                if (trans.item_type == "Sale of Culls")
                {
                    b += (double)trans.total_payment;
                }
                if (trans.item_type == "Sale for Breeding")
                {
                    c += (double)trans.total_payment;
                }
                if (trans.item_type == "Sale for Pet/Show")
                {
                    d += (double)trans.total_payment;
                }
                if (trans.item_type == "Farm Income")
                {
                    e += (double)trans.total_payment;
                }
                if (trans.item_type == "Other")
                {
                    f += (double)trans.total_payment;
                }
            }
            var expenses = db.Transactions.Where(m => m.type == false && m.userid == userID).ToList();
            if (User.IsInRole("admin"))
            {
                expenses = db.Transactions.Where(m => m.type == false).ToList();
            }
            double expenseTotal = 0;
            foreach (Transaction trans in expenses)
            {
                expenseTotal += (double)trans.total_payment;
                if (trans.item_type == "Feed/Hay")
                {
                    g += (double)trans.total_payment;
                }
                if (trans.item_type == "Fertilizer/Seed")
                {
                    h += (double)trans.total_payment;
                }
                if (trans.item_type == "Equipment")
                {
                    i += (double)trans.total_payment;
                }
                if (trans.item_type == "Vet Medication")
                {
                    j += (double)trans.total_payment;
                }
                if (trans.item_type == "Farm Supplies")
                {
                    k += (double)trans.total_payment;
                }
                if (trans.item_type == "Animal Purchase")
                {
                    l += (double)trans.total_payment;
                }
                if (trans.item_type == "Utilities")
                {
                    x += (double)trans.total_payment;
                }
                if (trans.item_type == "Others")
                {
                    n += (double)trans.total_payment;
                }
            }
            GraphViewModel gvm = new GraphViewModel()
            {
                income = incomeTotal,
                expense = expenseTotal,
                sfmk = a,
                sfc = b,
                sfb = c,
                sfp = d,
                fi = e,
                other = f,
                fh = g,
                fs = h,
                eq = i,
                vm = j,
                fsp = k,
                ap = l,
                ut = x,
                other2 = n
            };
            return View(gvm);
        }

        //
        // GET: /Transaction/Details/5
        public ActionResult Details(Int32 id)
        {
            Transaction transaction = db.Transactions.Find(id);
            int userID = (int)Membership.GetUser().ProviderUserKey;
            if (transaction == null)
            {
                return HttpNotFound();
            }
            else if ((!User.IsInRole("admin")) && transaction.UserProfile.UserId != userID)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        //
        // GET: /Transaction/Create
        public ActionResult CreateIncome()
        {
            List<SelectListItem> incomeList = new List<SelectListItem>() {
                new SelectListItem() { Text= "Sale of Meat Kids", Value = "Sale of Meat Kids"},
                new SelectListItem() { Text= "Sale of Culls", Value = "Sale of Culls"},
                new SelectListItem() { Text = "Sale for Breeding", Value = "Sale for Breeding"},
                new SelectListItem() { Text = "Sale for Pet/Show", Value = "Sale for Pet/Show"},
                new SelectListItem() { Text = "Farm Income", Value = "Farm Income"},
                new SelectListItem() { Text = "Other", Value = "Other"}
            };
            @ViewBag.incomeList = incomeList;
            return View();
        }

        public ActionResult CreateExpense()
        {
            List<SelectListItem> expenseList = new List<SelectListItem>() {
                new SelectListItem() { Text= "Feed/Hay", Value = "Feed/Hay"},
                new SelectListItem() { Text= "Fertilizer/Seed", Value = "Fertilizer/Seed"},
                new SelectListItem() { Text = "Equipment", Value = "Equipment"},
                new SelectListItem() { Text = "Vet Medication", Value = "Vet Medication"},
                new SelectListItem() { Text = "Farm Supplies", Value = "Farm Supplies"},
                new SelectListItem() { Text = "Animal Purchase", Value = "Animal Purchase"},
                new SelectListItem() { Text = "Utilities", Value = "Utilities"},
                new SelectListItem() { Text = "Others", Value = "Others"}
            };
            @ViewBag.expenseList = expenseList;
            return View();
        }

        //
        // POST: /Transaction/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateIncome(Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                transaction.type = true;
                transaction.userid = (int)Membership.GetUser().ProviderUserKey;
                db.Transactions.Add(transaction);
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
            List<SelectListItem> incomeList = new List<SelectListItem>() {
                new SelectListItem() { Text= "Sale of Meat Kids", Value = "Sale of Meat Kids"},
                new SelectListItem() { Text= "Sale of Culls", Value = "Sale of Culls"},
                new SelectListItem() { Text = "Sale for Breeding", Value = "Sale for Breeding"},
                new SelectListItem() { Text = "Sale for Pet/Show", Value = "Sale for Pet/Show"},
                new SelectListItem() { Text = "Farm Income", Value = "Farm Income"},
                new SelectListItem() { Text = "Other", Value = "Other"}
            };
            @ViewBag.incomeList = incomeList;
            return View(transaction);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateExpense(Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                transaction.type = false;
                transaction.userid = (int)Membership.GetUser().ProviderUserKey;
                db.Transactions.Add(transaction);
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
            List<SelectListItem> expenseList = new List<SelectListItem>() {
                new SelectListItem() { Text= "Feed/Hay", Value = "Feed/Hay"},
                new SelectListItem() { Text= "Fertilizer/Seed", Value = "Fertilizer/Seed"},
                new SelectListItem() { Text = "Equipment", Value = "Equipment"},
                new SelectListItem() { Text = "Vet Medication", Value = "Vet Medication"},
                new SelectListItem() { Text = "Farm Supplies", Value = "Farm Supplies"},
                new SelectListItem() { Text = "Animal Purchase", Value = "Animal Purchase"},
                new SelectListItem() { Text = "Utilities", Value = "Utilities"},
                new SelectListItem() { Text = "Others", Value = "Others"}
            };
            @ViewBag.expenseList = expenseList;
            return View(transaction);
        }

        //
        // GET: /Transaction/Edit/5
        public ActionResult EditIncome(Int32 id)
        {
            Transaction transaction = db.Transactions.Find(id);
            int userID = (int)Membership.GetUser().ProviderUserKey;
            if (transaction == null)
            {
                return HttpNotFound();
            }
            else if ((!User.IsInRole("admin")) && transaction.UserProfile.UserId != userID)
            {
                return HttpNotFound();
            }
            List<SelectListItem> incomeList = new List<SelectListItem>() {
                new SelectListItem() { Text= "Sale of Meat Kids", Value = "Sale of Meat Kids"},
                new SelectListItem() { Text= "Sale of Culls", Value = "Sale of Culls"},
                new SelectListItem() { Text = "Sale for Breeding", Value = "Sale for Breeding"},
                new SelectListItem() { Text = "Sale for Pet/Show", Value = "Sale for Pet/Show"},
                new SelectListItem() { Text = "Farm Income", Value = "Farm Income"},
                new SelectListItem() { Text = "Other", Value = "Other"}
            };
            @ViewBag.incomeList = incomeList;
            return View(transaction);
        }

        public ActionResult EditExpense(Int32 id)
        {
            Transaction transaction = db.Transactions.Find(id);
            int userID = (int)Membership.GetUser().ProviderUserKey;
            if (transaction == null)
            {
                return HttpNotFound();
            }
            else if ((!User.IsInRole("admin")) && transaction.UserProfile.UserId != userID)
            {
                return HttpNotFound();
            }
            List<SelectListItem> expenseList = new List<SelectListItem>() {
                new SelectListItem() { Text= "Feed/Hay", Value = "Feed/Hay"},
                new SelectListItem() { Text= "Fertilizer/Seed", Value = "Fertilizer/Seed"},
                new SelectListItem() { Text = "Equipment", Value = "Equipment"},
                new SelectListItem() { Text = "Vet Medication", Value = "Vet Medication"},
                new SelectListItem() { Text = "Farm Supplies", Value = "Farm Supplies"},
                new SelectListItem() { Text = "Animal Purchase", Value = "Animal Purchase"},
                new SelectListItem() { Text = "Utilities", Value = "Utilities"},
                new SelectListItem() { Text = "Others", Value = "Others"}
            };
            @ViewBag.expenseList = expenseList;
            return View(transaction);
        }

        //
        // POST: /Transaction/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditIncome(Transaction transaction)
        {
            if (ModelState.IsValid && transaction.userid != 0)
            {
                db.Entry(transaction).State = EntityState.Modified;
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
            List<SelectListItem> incomeList = new List<SelectListItem>() {
                new SelectListItem() { Text= "Sale of Meat Kids", Value = "Sale of Meat Kids"},
                new SelectListItem() { Text= "Sale of Culls", Value = "Sale of Culls"},
                new SelectListItem() { Text = "Sale for Breeding", Value = "Sale for Breeding"},
                new SelectListItem() { Text = "Sale for Pet/Show", Value = "Sale for Pet/Show"},
                new SelectListItem() { Text = "Farm Income", Value = "Farm Income"},
                new SelectListItem() { Text = "Other", Value = "Other"}
            };
            @ViewBag.incomeList = incomeList;
            return View(transaction);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditExpense(Transaction transaction)
        {
            if (ModelState.IsValid && transaction.userid != 0)
            {
                db.Entry(transaction).State = EntityState.Modified;
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
            List<SelectListItem> expenseList = new List<SelectListItem>() {
                new SelectListItem() { Text= "Feed/Hay", Value = "Feed/Hay"},
                new SelectListItem() { Text= "Fertilizer/Seed", Value = "Fertilizer/Seed"},
                new SelectListItem() { Text = "Equipment", Value = "Equipment"},
                new SelectListItem() { Text = "Vet Medication", Value = "Vet Medication"},
                new SelectListItem() { Text = "Farm Supplies", Value = "Farm Supplies"},
                new SelectListItem() { Text = "Animal Purchase", Value = "Animal Purchase"},
                new SelectListItem() { Text = "Utilities", Value = "Utilities"},
                new SelectListItem() { Text = "Others", Value = "Others"}
            };
            @ViewBag.expenseList = expenseList;
            return View(transaction);
        }

        //
        // GET: /Transaction/Delete/5
        public ActionResult Delete(Int32 id)
        {
            Transaction transaction = db.Transactions.Find(id);
            int userID = (int)Membership.GetUser().ProviderUserKey;
            if (transaction == null)
            {
                return HttpNotFound();
            }
            else if ((!User.IsInRole("admin")) && transaction.UserProfile.UserId != userID)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        //
        // POST: /Transaction/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Int32 id)
        {
            Transaction transaction = db.Transactions.Find(id);
            db.Transactions.Remove(transaction);
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
