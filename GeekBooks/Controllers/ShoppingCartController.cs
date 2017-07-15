using GeekBooks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Data.Entity;

namespace GeekBooks.Controllers
{
    [Authorize]
    public class ShoppingCartController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        
        // GET: ShoppingCart
        public ActionResult Index()
        {
            string usr = User.Identity.GetUserId();
            List<ShoppingCart> cartList = new List<ShoppingCart>();
            cartList = (from a in db.ShoppingCarts
                        where a.UID == usr
                        select a).ToList();
            return View(cartList);
        }
        
        public ActionResult Update([Bind(Include = "SCI,UID,BookID,Quantity,SaveForLater")] ShoppingCart cartItem)
        {
            try
            {
                int bookQty = (from a in db.Books
                               where a.BookID == cartItem.BookID
                               select a.Stock).FirstOrDefault();
                if (cartItem.Quantity > bookQty)
                {
                    ModelState.AddModelError("Quantity", "Insufficient books in stock");
                    return View();
                }

                db.Entry(cartItem).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
                return View();
            }
        }

        public ActionResult DeleteItemFromCart([Bind(Include = "SCI,UID,BookID,Quantity,SaveForLater")] ShoppingCart cartItem)
        {
            try
            {
                ShoppingCart item = db.ShoppingCarts.FirstOrDefault(x => x.BookID == cartItem.BookID && x.UID == cartItem.UID);
                db.ShoppingCarts.Remove(item);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult ShoppingCartConfirmation(ShoppingCartConfrmCViewModel cartConfrm)
        {
            Book book = db.Books.FirstOrDefault(x => x.BookID == cartConfrm.BookID);
            return View(book);
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
