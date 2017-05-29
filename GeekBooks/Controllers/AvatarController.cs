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
using System.IO;


namespace GeekBooks.Controllers
{
    public class AvatarController : Controller
    {
        private MyDbContext db = new MyDbContext();

        public ActionResult ShowImage()
        {
            return View();
        }

        public ActionResult ChangeAvatar(int? id)
        {
            return View();
        }
        
        // GET: Avatars
        public ActionResult Index()
        {
            var avatars = db.Avatars.Include(a => a.UserAccount);
            return View(avatars.ToList());
        }

        // GET: Avatars/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Avatar avatar = db.Avatars.Find(id);
            if (avatar == null)
            {
                return HttpNotFound();
            }
            return View(avatar);
        }

        // GET: Avatars/Create
        public ActionResult Create()
        {
            ViewBag.UID = new SelectList(db.Users, "UID", "FirstName");
            return View();
        }

        // POST: Avatars/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AVATARID,UID,ImageUrl")] Avatar avatar)
        {
            if (ModelState.IsValid)
            {
                db.Avatars.Add(avatar);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UID = new SelectList(db.Users, "UID", "FirstName", avatar.UID);
            return View(avatar);
        }

        // GET: Avatars/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Avatar avatar = db.Avatars.Find(id);
            if (avatar == null)
            {
                return HttpNotFound();
            }
            ViewBag.UID = new SelectList(db.Users, "UID", "FirstName", avatar.UID);
            return View(avatar);
        }

        // POST: Avatars/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AVATARID,UID,ImageUrl")] Avatar avatar)
        {
            if (ModelState.IsValid)
            {
                db.Entry(avatar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UID = new SelectList(db.Users, "UID", "FirstName", avatar.UID);
            return View(avatar);
        }

        // GET: Avatars/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Avatar avatar = db.Avatars.Find(id);
            if (avatar == null)
            {
                return HttpNotFound();
            }
            return View(avatar);
        }

        // POST: Avatars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Avatar avatar = db.Avatars.Find(id);
            db.Avatars.Remove(avatar);
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
