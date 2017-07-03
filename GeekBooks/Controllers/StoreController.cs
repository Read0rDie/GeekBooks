using GeekBooks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using AutoMapper;

namespace GeekBooks.Controllers
{
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

            foreach(var bGenre in query)
            {
                var books = db.Books.ToList();
                foreach (var item in books)
                {
                    var genres = item.Genre.ToList();
                    foreach (var genre in genres)
                    {
                        if (genre.GenreTitle == bGenre.GenreTitle && item.BookName != title)
                        {
                            t_bookList.Add(item);
                        }
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
    }
}