using GeekBooks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using AutoMapper;
using System.Data.Entity;

namespace GeekBooks.Controllers
{
    [Authorize]
    public class StoreController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        #region To Use with Book Tab
        //public ActionResult Index(string sortOrder, string sortType)
        //{
        //    ViewBag.BookNameSortParm = String.IsNullOrEmpty(sortOrder) ? "bookname" : sortOrder;
        //    ViewBag.PriceSortParm = String.IsNullOrEmpty(sortOrder) ? "price" : sortOrder;
        //    ViewBag.RatingSortParm = String.IsNullOrEmpty(sortOrder) ? "rating" : sortOrder;
        //    ViewBag.GenreSortParm = String.IsNullOrEmpty(sortOrder) ? "genre" : sortOrder;

        //    ViewBag.SortType = String.IsNullOrEmpty(sortType) ? "" : sortType;

        //    List<Book> t_bookList = db.Books.ToList();
        //    List<BookViewModel> bookList = new List<BookViewModel>();
        //    Mapper.Initialize(cfg => { cfg.CreateMap<Book, BookViewModel>().ReverseMap(); });
        //    BookViewModel model;


        //    foreach (var item in t_bookList)
        //    {
        //        model = Mapper.Map<BookViewModel>(item);

        //        foreach (var item2 in item.BookRatings)
        //        {
        //            model.AvgRating += item2.Rating;
        //        }

        //        if (item.BookRatings.Count > 0)
        //        {
        //            model.AvgRating = model.AvgRating / item.BookRatings.Count;
        //        }

        //        bookList.Add(model);
        //    }

        //    switch (sortOrder)
        //    {
        //        case "bookname":
        //            {
        //                if (sortType == "asc")
        //                {
        //                    bookList = bookList.OrderByDescending(s => s.BookName).ToList();
        //                    ViewBag.SortType = "desc";
        //                }
        //                else
        //                {
        //                    bookList = bookList.OrderBy(s => s.BookName).ToList();
        //                    ViewBag.SortType = "asc";
        //                }
        //                break;
        //            }
        //        case "genre":
        //            {
        //                if (sortType == "asc")
        //                {
        //                    bookList = bookList.OrderByDescending(s => s.Genre).ToList();
        //                    ViewBag.SortType = "desc";
        //                }
        //                else
        //                {
        //                    bookList = bookList.OrderBy(s => s.Genre).ToList();
        //                    ViewBag.SortType = "asc";
        //                }

        //                break;
        //            }
        //        case "price":
        //            {
        //                if (sortType == "asc")
        //                {
        //                    bookList = bookList.OrderByDescending(s => s.Price).ToList();
        //                    ViewBag.SortType = "desc";
        //                }
        //                else
        //                {
        //                    bookList = bookList.OrderBy(s => s.Price).ToList();
        //                    ViewBag.SortType = "asc";
        //                }
        //                break;
        //            }
        //        case "rating":
        //            {
        //                if (sortType == "asc")
        //                {
        //                    bookList = bookList.OrderByDescending(s => s.AvgRating).ToList();
        //                    ViewBag.SortType = "desc";
        //                }
        //                else
        //                {
        //                    bookList = bookList.OrderBy(s => s.AvgRating).ToList();
        //                    ViewBag.SortType = "asc";
        //                }
        //                break;
        //            }
        //        default:
        //            bookList = bookList.OrderBy(s => s.BookName).ToList();
        //            ViewBag.SortType = "asc";
        //            break;
        //    }

        //    return View(bookList);
        //}

        #endregion

        // GET: Product
        public ActionResult Index()
        {
            List<Book> t_bookList = db.Books.ToList();
            List<BookViewModel> bookList = new List<BookViewModel>();
            Mapper.Initialize(cfg => { cfg.CreateMap<Book, BookViewModel>().ReverseMap(); });
            BookViewModel model;

            foreach (var item in t_bookList)
            {
                model = Mapper.Map<BookViewModel>(item);
                model.AvgRating = 0;
                foreach (var item2 in item.BookRatings)
                {
                    model.AvgRating += item2.Rating;
                }

                if (item.BookRatings.Count > 0)
                {
                    model.AvgRating = model.AvgRating / item.BookRatings.Count;
                }

                bookList.Add(model);
            }

            return View(bookList);
        }

        public ActionResult Search(string query, int? type)
        {
            List<Book> t_bookList = new List<Book>();
            List<BookViewModel> bookList = new List<BookViewModel>();
            Mapper.Initialize(cfg => { cfg.CreateMap<Book, BookViewModel>().ReverseMap(); });
            BookViewModel model;
            
            switch (type)
            {
                case 0:
                    t_bookList = db.Books.Where(p => p.Genre == query).ToList();
                    ViewBag.Message = query;
                    break;
                case 1:
                    t_bookList = db.Books.Where(p => p.Author.AuthorName == query).ToList();
                    ViewBag.Message = "Books by " + query;
                    break;
                case 2:
                    t_bookList = db.Books.Where(p => p.BookName == query).ToList();
                    ViewBag.Message = query;
                    break;
                default:
                    t_bookList = db.Books.Where(p => p.Author.AuthorName.Contains(query) || p.Genre.Contains(query) || p.BookName.Contains(query)).ToList();
                    ViewBag.Message = "Search results for: " + query;
                    break;
            }

            
            foreach (var item in t_bookList)
            {
                model = Mapper.Map<BookViewModel>(item);
                model.AvgRating = 0;
                foreach (var item2 in item.BookRatings)
                {
                    model.AvgRating += item2.Rating;
                }

                if (item.BookRatings.Count > 0)
                {
                    model.AvgRating = model.AvgRating / item.BookRatings.Count;
                }

                bookList.Add(model);
            }

            return View(bookList.ToList());
            
        }

        public ActionResult ProductDetails(int? id)
        {
            Book book = db.Books.FirstOrDefault(x => x.BookID == id);

            double averg = 0;

            foreach (var item in book.BookRatings)
            {
                averg += item.Rating;               
            }

            if (book.BookRatings.Count > 0)
            {
                averg = averg / book.BookRatings.Count;
            }


            if (book == null)
            {
                book = new Book();
            }

            ViewBag.AvgRating = averg;

            return View(book);
        }

        public ActionResult AddBookToCart(int bookID)
        {
            try
            {
                string userid = User.Identity.GetUserId();

                ShoppingCart cartItem = db.ShoppingCarts.FirstOrDefault(x => x.BookID == bookID && x.UID == userid);

                if (cartItem != null)
                {
                    cartItem.Quantity += 1;
                    db.Entry(cartItem).State = EntityState.Modified;
                }
                else
                {
                    ShoppingCart item = new ShoppingCart()
                    {
                        BookID = bookID,
                        UID = userid,
                        Quantity = 1,
                        SaveForLater = false
                    };

                    db.ShoppingCarts.Add(item);
                }

                
                db.SaveChanges();

                Book book = db.Books.FirstOrDefault(x => x.BookID == bookID);

                return RedirectToAction("ShoppingCartConfirmation", "ShoppingCart", book);
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
                return View();
            }
        }
    }
}