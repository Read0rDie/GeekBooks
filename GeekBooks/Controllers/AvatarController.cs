using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using GeekBooks.Models;

namespace GeekBooks.Controllers
{
    [Authorize]
    public class AvatarController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        
        // GET: Avatars/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Avatars/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Avatar avatar)
        {
            avatar.UID = User.Identity.GetUserId();
            db.Avatars.Add(avatar);
            db.SaveChanges();
            return RedirectToAction("UserProfile", "Account");
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
            return View(avatar);
        }

        // POST: Avatars/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Avatar avatar)
        {
            avatar.UID = User.Identity.GetUserId();
            db.Entry(avatar).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("UserProfile", "Account");
        }        
    }
}
