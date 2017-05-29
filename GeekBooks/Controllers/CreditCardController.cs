using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GeekBooks.DataAccessLayer;
using GeekBooks.Models;

namespace GeekBooks.Controllers
{
    public class CreditCardController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: CreditCard
        public ActionResult Index()
        {
            var creditCards = db.CreditCards.Include(c => c.UserAccount);
            return View(creditCards.ToList());
        }

        // GET: CreditCard/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CreditCard creditCard = db.CreditCards.Find(id);
            if (creditCard == null)
            {
                return HttpNotFound();
            }
            return View(creditCard);
        }

        // GET: CreditCard/Create
        public ActionResult Create()
        {
            Address address = new Address();
            string id = (string) Session["UserId"];
            address.UID = Int32.Parse(id);
            ViewBag.UID = new SelectList(db.Users, "UID", "FirstName");
            return View();
        }

        // POST: CreditCard/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UID,CardName,CardNumber")] CreditCard creditCard)
        {
            if (ModelState.IsValid)
            {
                db.CreditCards.Add(creditCard);
                db.SaveChanges();
                return RedirectToAction("Profile", "Account");
            }

            ViewBag.UID = new SelectList(db.Users, "UID", "FirstName", creditCard.UID);
            return View(creditCard);
        }

        // GET: CreditCard/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CreditCard creditCard = db.CreditCards.Find(id);
            if (creditCard == null)
            {
                return HttpNotFound();
            }
            ViewBag.UID = new SelectList(db.Users, "UID", "FirstName", creditCard.UID);
            return View(creditCard);
        }

        // POST: CreditCard/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UID,CardName,CardNumber")] CreditCard creditCard)
        {
            if (ModelState.IsValid)
            {
                db.Entry(creditCard).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Profile", "Account");
            }
            ViewBag.UID = new SelectList(db.Users, "UID", "FirstName", creditCard.UID);
            return View(creditCard);
        }

        // GET: CreditCard/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CreditCard creditCard = db.CreditCards.Find(id);
            if (creditCard == null)
            {
                return HttpNotFound();
            }
            return View(creditCard);
        }

        // POST: CreditCard/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CreditCard creditCard = db.CreditCards.Find(id);
            db.CreditCards.Remove(creditCard);
            db.SaveChanges();
            return RedirectToAction("Profile", "Account");
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
