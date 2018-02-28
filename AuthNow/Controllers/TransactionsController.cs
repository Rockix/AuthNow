using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AuthNow.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Ajax.Utilities;
using Microsoft.Owin.Logging;

namespace AuthNow.Controllers
{
    public class TransactionsController : Controller
    {
        
        private ApplicationDbContext db = new ApplicationDbContext();

        protected UserManager<ApplicationUser> UserManager { get; set; }

        public TransactionsController()
        {            
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.db));
        }

        // GET: Transactions
        public ActionResult Index()
        {
            var transactions = db.Transactions.Include(t => t.Campaign).Include(t => t.Donor);
            return View(transactions.ToList());
        }

        // GET: Transactions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // GET: Transactions/Create
        public ActionResult Create(int? id)
        {
            if (id != null)
            {
                Campaign c = db.Campaigns.Find(id);
                ViewBag.Camp = c.Title;
            }else
            {
                ViewBag.Camp = "gnu here";
            }
            
            ViewBag.CampaignId = new SelectList(db.Campaigns, "CampaignId", "Title");
            ViewBag.DonorId = new SelectList(db.Donors, "DonorId", "FirstName");
            return View();
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TransactionId,Amount,CampaignId,PaymentOption")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                var user = UserManager.FindById(User.Identity.GetUserId());
                Donor donor = new Donor();
                if (user != null)
                {
                    donor.FirstName = user.UserName;
                    donor.LastName = user.Email;
                    //var CheckingDonor = db.Donors.Where(d=>);
                    //if (CheckingDonor.length() < 0)
                    //{
                    //    donor.NumberOfDonations += 1;
                    //}
                    //else
                    //{
                        db.Donors.Add(donor);
                   // }

                    
                    transaction.Donor = donor;
                }
                Campaign c = db.Campaigns.Find(transaction.CampaignId);

                c.CurrentAmount = c.CurrentAmount + transaction.Amount;

                transaction.DateCreated = DateTime.Now;
                db.Transactions.Add(transaction);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CampaignId = new SelectList(db.Campaigns, "CampaignId", "Title", transaction.CampaignId);
            ViewBag.DonorId = new SelectList(db.Donors, "DonorId", "FirstName", transaction.DonorId);
            return View(transaction);
        }

        // GET: Transactions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            ViewBag.CampaignId = new SelectList(db.Campaigns, "CampaignId", "Title", transaction.CampaignId);
            ViewBag.DonorId = new SelectList(db.Donors, "DonorId", "FirstName", transaction.DonorId);
            return View(transaction);
        }

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TransactionId,Amount,CampaignId,DonorId,PaymentOption,DateCreated")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transaction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CampaignId = new SelectList(db.Campaigns, "CampaignId", "Title", transaction.CampaignId);
            ViewBag.DonorId = new SelectList(db.Donors, "DonorId", "FirstName", transaction.DonorId);
            return View(transaction);
        }

        // GET: Transactions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Transaction transaction = db.Transactions.Find(id);
            db.Transactions.Remove(transaction);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
