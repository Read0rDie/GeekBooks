using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GeekBooks.Models;
using GeekBooks.DataAccessLayer;
using System.Net;

namespace GeekBooks.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
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

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserAccount account)
        {
            if (ModelState.IsValid)
            {
                bool UniqueEmail = true;
                bool UniqueUserName = true;

                using (MyDbContext db = new MyDbContext())
                {              
                    foreach (UserAccount u in db.Users)
                    {
                        if (account.Email.ToString().Equals(u.Email.ToString()))
                        {
                            UniqueEmail = false;
                        }
                        if (account.NickName.ToString().Equals(u.NickName.ToString()))
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
                        ModelState.AddModelError("NickName", "Username already exists.");
                    }
                    else if (UniqueUserName && !UniqueEmail)
                    {
                        ModelState.AddModelError("Email", "Email Address already exists.");                        
                    }
                    else
                    {
                        ModelState.AddModelError("Email", "Email Address already exists.");
                        ModelState.AddModelError("NickName", "Username already exists.");
                    }
                }               
            }            
            return View();

        }

        public ActionResult Update(int ? id)
        {
            using(MyDbContext db = new MyDbContext())
            {
                if(id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                UserAccount account = db.Users.Find(id);
                if (account == null)
                {
                    return new HttpNotFoundResult();
                }
                
            }
            return View("Register");
        }

        [HttpPost]
        public ActionResult Update(UserAccount account)
        {
            return View();
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
        public ActionResult Login(UserAccount user)
        {
            using (MyDbContext db = new MyDbContext())
            {
                var usr = db.Users.SingleOrDefault(u => u.PassWord == user.PassWord && u.NickName == user.NickName);
                if (usr != null)
                {
                    Session["UserId"] = usr.UID.ToString();
                    Session["UserName"] = usr.NickName.ToString();
                    return View("Index");
                }
                else
                {
                    ModelState.AddModelError("PassWord", "Username or Password is incorrect.");
                    ModelState.AddModelError("NickName", "Username or Password is incorrect.");
                }
            }
            return View();
        }

        public ActionResult LoggedIn()
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

        
    }
}