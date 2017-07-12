using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using GeekBooks.Models;
using System.Data.Entity;


namespace GeekBooks.Controllers
{
    [Authorize]
    public class BookRatingController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BookRating
        public ActionResult Index()
        {
            return View();
        }

        // GET: BookRating/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BookRating/Create
        public ActionResult Create(int id)
        {
            BookRating bookrating = new BookRating();
            bookrating.BookID = id;
            bookrating.UID = User.Identity.GetUserId();
            return View(bookrating);
        }

        // POST: BookRating/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "BookRatingID,UID,BookID,Comment,Rating")] BookRating bookrating)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.BookRatings.Add(bookrating);
                    db.SaveChanges();
                }

                return RedirectToAction("ProductDetails", "Store", new { id = bookrating.BookID });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
                return View(bookrating);
            }
        }

        // GET: BookRating/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookRating bookrating = db.BookRatings.Find(id);
            if (bookrating == null)
            {
                return HttpNotFound();
            }
            return View(bookrating);
        }

        // POST: BookRating/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "BookRatingID,UID,BookID,Comment,Rating")] BookRating bookrating)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string userid = User.Identity.GetUserId();


                    db.Entry(bookrating).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("ProductDetails", "Store", new { id = bookrating.BookID });
                }
                return View(bookrating);
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
                return View(bookrating);
            }
        }

        // GET: BookRating/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookRating bookrating = db.BookRatings.Find(id);
            if (bookrating == null)
            {
                return HttpNotFound();
            }
            return View(bookrating);
        }

        // POST: BookRating/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, [Bind(Include = "BookRatingID,UID,BookID,Comment,Rating")] BookRating bookrating)
        {
            try
            {
                BookRating brating = db.BookRatings.Find(id);
                db.BookRatings.Remove(brating);
                db.SaveChanges();
                return RedirectToAction("ProductDetails", "Store", new { id = brating.BookID });
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
                return View(bookrating);
            }
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
