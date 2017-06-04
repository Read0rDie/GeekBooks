using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GeekBooks.Models;
using GeekBooks.DataAccessLayer;
using System.Net;
using System.Data.Entity;
using AutoMapper;

namespace GeekBooks.Controllers
{
    public class AccountController : Controller
    {
        private MyDbContext db = new MyDbContext();
        // GET: Account
        public ActionResult Index()
        {
            return View("Login");
        }

        public ActionResult Register()
        {            
            return View();
        }

        [HttpPost]
        public ActionResult Register([Bind(Include = "UID,FirstName,Lastname,Username,Email,PassWord,ConfirmPassword")] Account account)
        {
            if (ModelState.IsValid)
            {
                bool UniqueEmail = true;
                bool UniqueUserName = true;

                       
                foreach (Account u in db.Users)
                {
                    if (account.Email.ToString().Equals(u.Email.ToString()))
                    {
                        UniqueEmail = false;
                    }
                    if (account.Username.ToString().Equals(u.Username.ToString()))
                    {
                        UniqueUserName = false;
                    }
                }
                if (UniqueUserName && UniqueEmail)
                {
                    db.Users.Add(account);
                    db.SaveChanges();
                    ModelState.Clear();                        
                    TempData["message"] = account.FirstName + " " + account.LastName + " successfully registered";
                    return RedirectToAction("Login");
                }
                else if (!UniqueUserName && UniqueEmail)
                {
                    ModelState.AddModelError("Username", "Username already exists.");
                }
                else if (UniqueUserName && !UniqueEmail)
                {
                    ModelState.AddModelError("Email", "Email Address already exists.");                        
                }
                else
                {
                    ModelState.AddModelError("Email", "Email Address already exists.");
                    ModelState.AddModelError("Username", "Username already exists.");
                }
                           
            }            
            return View();

        }

        public ActionResult Update(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Users.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update( Account account)
        {            
            var errors = ModelState
                .Where(x => x.Value.Errors.Count > 0)
                .Select(x => new { x.Key, x.Value.Errors })
                .ToArray();

            if (ModelState.IsValid)
            {
                bool UniqueEmail = true;
                bool UniqueUserName = true;


                foreach (Account u in db.Users)
                {
                    if (account.Email.ToString().Equals(u.Email.ToString()) && !account.UID.Equals(u.UID))
                    {
                        UniqueEmail = false;
                    }
                    if (account.Username.ToString().Equals(u.Username.ToString()) && !account.UID.Equals(u.UID))
                    {
                        UniqueUserName = false;
                    }
                }
                if (UniqueUserName && UniqueEmail)
                {
                    var updatedAccount = db.Users.Find(account.UID);
                    updatedAccount.FirstName = account.FirstName;
                    updatedAccount.LastName = account.LastName;
                    updatedAccount.Username = account.Username;
                    updatedAccount.Email = account.Email;
                    db.Entry(updatedAccount).State = EntityState.Modified;
                    db.SaveChanges();
                    ModelState.Clear();
                    TempData["message"] = account.FirstName + " " + account.LastName + " successfully updated";
                    return View("Profile");
                }
                else if (!UniqueUserName && UniqueEmail)
                {
                    ModelState.AddModelError("Username", "Username already exists.");
                }
                else if (UniqueUserName && !UniqueEmail)
                {
                    ModelState.AddModelError("Email", "Email Address already exists.");
                }
                else
                {
                    ModelState.AddModelError("Email", "Email Address already exists.");
                    ModelState.AddModelError("Username", "Username already exists.");
                }

            }
            return View(account);
        }

        public ActionResult Login()
        {
            if (TempData["message"] != null)
            {
                ViewBag.Message = TempData["message"].ToString();
            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(Account user)
        {
            
            var usr = db.Users.SingleOrDefault(u => u.PassWord == user.PassWord && u.Username == user.Username);

            if (usr != null)
            {
                Session["UserId"] = usr.UID.ToString();
                Session["UserName"] = usr.Username.ToString();
                return View("Profile");
            }
            else
            {
                ModelState.AddModelError("PassWord", "Username or Password is incorrect.");
                ModelState.AddModelError("NickName", "Username or Password is incorrect.");
            }
            
            return View();
        }

        public ActionResult Profile()
        {
            if (Session["UserId"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Register");
            }
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }

        public Account search(string username)
        {
            return db.Users.Where(acc => acc.Username.Equals(username)).FirstOrDefault();
        }

        // GET: PersonalDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account toDelete = db.Users.Find(id);
            if (toDelete == null)
            {
                return HttpNotFound();
            }
            return View(toDelete);
        }

        // POST: PersonalDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Account toDelete = db.Users.Find(id);
            db.Users.Remove(toDelete);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}