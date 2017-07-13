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

        public ActionResult Carousel(List<Genre> query, string title)
        {
            List<Book> t_bookList = new List<Book>();
            List<BookViewModel> bookList = new List<BookViewModel>();
            Mapper.Initialize(cfg => { cfg.CreateMap<Book, BookViewModel>().ReverseMap(); });
            BookViewModel model;

            var books = db.Books.ToList();
            foreach (var book in books)
            {
                foreach(var genre in book.Genre)
                {
                    bool found = false;
                    foreach(var bGenre in query)
                    {
                        if (genre.GenreTitle == bGenre.GenreTitle && book.BookName != title)
                        {
                            t_bookList.Add(book);
                            found = true;
                            break;
                        }
                    }
                    if (found == true)
                    {
                        break;
                    }
                }
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
            return PartialView(bookList.ToList());
        }

        public ActionResult MiniSearch(string query, string title)
        {
            List<Book> t_bookList = new List<Book>();
            List<BookViewModel> bookList = new List<BookViewModel>();
            Mapper.Initialize(cfg => { cfg.CreateMap<Book, BookViewModel>().ReverseMap(); });
            BookViewModel model;

            t_bookList = db.Books.Where(p => p.Author.AuthorName.Contains(query) && p.BookName != title).ToList();

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
            return PartialView(bookList.ToList());
        }

        public ActionResult Search(string query, int? type)
        {
            List<Book> t_bookList = new List<Book>();
            List<BookViewModel> bookList = new List<BookViewModel>();
            Mapper.Initialize(cfg => { cfg.CreateMap<Book, BookViewModel>().ReverseMap(); });
            BookViewModel model;

            var books = db.Books.ToList();
            switch (type)
            {
                case 0:                    
                    foreach(var item in books)
                    {
                        var genres = item.Genre.ToList();
                        foreach(var genre in genres)
                        {
                            if(genre.GenreTitle == query)
                            {
                                t_bookList.Add(item);
                            }
                        }
                    }
                    //t_bookList = db.Books.Where(p => p.Genre.Contains(query)).ToList();
                    ViewBag.Message = query;
                    break;
                case 1:
                    t_bookList = db.Books.Where(p => p.Author.AuthorName.Contains(query)).ToList();
                    ViewBag.Message = "Books by " + query;
                    break;
                case 2:
                    t_bookList = db.Books.Where(p => p.BookName.Contains(query)).ToList();
                    ViewBag.Message = query;
                    break;
                default:
                    if (query.Length > 0)
                    {
                        foreach (var item in books)
                        {
                            var genres = item.Genre.ToList();
                            foreach (var genre in genres)
                            {
                                if (genre.GenreTitle.ToLower().Contains(query.ToLower()) || item.Author.AuthorName.ToLower().Contains(query.ToLower()) || item.BookName.ToLower().Contains(query.ToLower()))
                                {
                                    t_bookList.Add(item);
                                    break;
                                }
                            }
                        }                       
                    }
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

        public ActionResult AuthorSpotlight()
        {
            var bookList = db.Books.ToList();
            int size = bookList.Count;
            Random r = new Random();
            int random = r.Next(0, size);
            Book book = bookList.ElementAt(random);            
            return View(book);
        }        
        
        public ActionResult Filter(List<Query> queries)
        {
            List<Book> books = new List<Book>();
            List<FilterViewModel> bookList = new List<FilterViewModel>();
            FilterViewModel fvm = new FilterViewModel();
            if (queries.Count() == 0)
            {
                books = db.Books.ToList();                
            }
            else
            {
                foreach (var query in queries)
                {
                    switch (query.type)
                    {
                        case 0:
                            List<Book> temp = new List<Book>();
                            temp = books.Where(p => p.Author.AuthorName.Contains(query.query)).ToList();
                            books = temp;
                            break;
                        case 1:
                            List<Book> temp2 = new List<Book>();
                            foreach (var item in books)
                            {
                                var genres = item.Genre.ToList();
                                foreach (var genre in genres)
                                {
                                    if (genre.GenreTitle == query.query)
                                    {
                                        temp2.Add(item);
                                    }
                                }
                            }
                            books = temp2;
                            break;
                        default:
                            break;
                    }
                }
            }            

            foreach(var book in books)
            {
                fvm = Mapper.Map<FilterViewModel>(book);
                fvm.AvgRating = 0;
                foreach (var rating in book.BookRatings)
                {
                    fvm.AvgRating += rating.Rating;
                }

                if (book.BookRatings.Count > 0)
                {
                    fvm.AvgRating = fvm.AvgRating / book.BookRatings.Count;
                }
                bookList.Add(fvm);
            }

            return View(bookList);
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

                ShoppingCartConfrmCViewModel cartConfrm = new ShoppingCartConfrmCViewModel()
                {
                    BookID = bookID
                };

                return RedirectToAction("ShoppingCartConfirmation", "ShoppingCart", cartConfrm);
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
                return View();
            }
        }
    }
}