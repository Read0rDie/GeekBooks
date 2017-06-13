using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GeekBooks.Controllers
{
    public class StoreController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProductDetails()
        {
            return View();
        }
    }
}