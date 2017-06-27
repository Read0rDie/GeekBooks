using GeekBooks.DataAccessLayer;
using GeekBooks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace GeekBooks.Controllers
{
    public class StoreController : Controller
    {
        private MyDbContext db = new MyDbContext();
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProductDetails(int id)
        {            
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.AuthID = new SelectList(db.Authors, "AuthID", "AuthorName", product.AuthID);
            return View(product);
        }
    }
}