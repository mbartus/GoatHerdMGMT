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
            
            return View(myTransactions);
        }

        //
        // GET: /Transaction/Compare
        public ActionResult Compare()
        {
            return View(db.Transactions.ToList());
        }

        //
        // GET: /Transaction/Details/5
        public ActionResult Details(Int32 id)
        {
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        //
        // GET: /Transaction/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Transaction/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                transaction.userid = (int)Membership.GetUser().ProviderUserKey;
                db.Transactions.Add(transaction);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(transaction);
        }

        //
        // GET: /Transaction/Edit/5
        public ActionResult Edit(Int32 id)
        {
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        //
        // POST: /Transaction/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transaction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(transaction);
        }

        //
        // GET: /Transaction/Delete/5
        public ActionResult Delete(Int32 id)
        {
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
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
