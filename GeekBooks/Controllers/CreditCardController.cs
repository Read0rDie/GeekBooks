using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GeekBooks.Models;
using Microsoft.AspNet.Identity;

namespace GeekBooks.Controllers
{
    [Authorize]
    public class CreditCardController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        
        // GET: CreditCard/Create
        public ActionResult Create()
        {           
            ViewBag.UID = new SelectList(db.Users, "UID", "FirstName");
            return View();
        }

        // POST: CreditCard/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UID,CardName,CardNumber,ExpirationMonth,ExpirationYear,SecurityCode,IsPreferred")] CreditCard creditCard)
        {
            if (ModelState.IsValid)
            {
                string userid = User.Identity.GetUserId();

                if (creditCard.IsPreferred)
                {
                    List<CreditCard> creditcards = (from a in db.CreditCards
                                                  where a.UID == userid
                                                  select a).ToList();
                    foreach (var item in creditcards)
                    {
                        item.IsPreferred = false;
                    }
                }

                creditCard.UID = userid;
                db.CreditCards.Add(creditCard);
                db.SaveChanges();
                return RedirectToAction("UserProfile", "Account");
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
        public ActionResult Edit([Bind(Include = "UID,CID,CardName,CardNumber,ExpirationMonth,ExpirationYear,SecurityCode,IsPreferred")] CreditCard creditCard)
        {
            if (ModelState.IsValid)
            {
                string userid = User.Identity.GetUserId();

                if (creditCard.IsPreferred)
                {
                    List<CreditCard> creditcards = (from a in db.CreditCards
                                                    where a.UID == userid && a.CID != creditCard.CID
                                                    select a).ToList();
                    foreach (var item in creditcards)
                    {
                        item.IsPreferred = false;
                    }
                }

                db.Entry(creditCard).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("UserProfile", "Account");
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
            return RedirectToAction("UserProfile", "Account");
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
