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
    public class AvatarController : Controller
    {
        private MyDbContext db = new MyDbContext();
        
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
                return RedirectToAction("Profile", "Account");
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
                return RedirectToAction("Profile", "Account");
            }
            ViewBag.UID = new SelectList(db.Users, "UID", "FirstName", avatar.UID);
            return View(avatar);
        }
        
    }
}
