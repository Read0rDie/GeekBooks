using GeekBooks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace GeekBooks.Controllers
{
    public class StoreController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: Product
        public ActionResult Index()
        {
            List<Book> bookList = db.Books.ToList();
            return View(bookList);
        }

        public ActionResult ProductDetails(int? bookid)
        { 
            Book book = db.Books.FirstOrDefault(x => x.BookID == bookid);

            if (book != null)
            {
                book = new Book();
            }

            return View(book);
        }
    }
}