using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekBooks.Models
{
    public class Book
    {
        [Key]
        public int BookID { get; set; }

        [ForeignKey("Author")]
        public int AuthorID { get; set; }

        [Display(Name ="Book Name")]
        [StringLength(255)]
        public string BookName { get; set; }

        [Display(Name = "Cover Image")]
        [DataType(DataType.ImageUrl)]
        public string CoverUrl { get; set; }
                  
        [Display(Name = "Synopsis")]
        [StringLength(2001)]
        public string Synopsis { get; set; }

        [Display(Name = "Publisher")]
        public string Publisher { get; set; }

        [Display(Name = "Price")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Display(Name = "Number in stock")]
        public int Stock { get; set; }

        [Display(Name = "ASIN")]
        public string ASIN { get; set; }

        [Display(Name = "Publication Date")]
        public DateTime PDate { get; set; }

        public virtual BookAuthor Author { get; set;}

        public virtual ICollection<BookRating> BookRatings { get; set; }
        
        public virtual List<Genre> Genre { get; set; }
    }

    public class Genre
    {
        public int GenreID { get; set; }

        [StringLength(256)]
        public string GenreTitle { get; set; }
    }

    public class BookViewModel
    {        
        public int BookID { get; set; }
       
        [Display(Name = "Book Name")]
        public string BookName { get; set; }

        [Display(Name = "Cover Image")]
        public string CoverUrl { get; set; }
        
        [Display(Name = "Synopsis")]
        public string Synopsis { get; set; }

        [Display(Name = "Price")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Display(Name = "Number in stock")]
        public int Stock { get; set; }

        [Display(Name = "Publication Date")]
        public DateTime PDate { get; set; }

        [Display(Name = "Rating")]
        public double AvgRating { get; set; }

        public virtual BookAuthor Author { get; set; }

        public virtual List<Genre> Genre { get; set; }        
    }

    public class Query
    {
        public string query { set; get; }
        public int? type { set; get; }
    }    

    public class FilterViewModel
    {
        public IEnumerable<BookViewModel> FilterItems { get; set; }

        public IEnumerable<BookViewModel> Items { get; set; }

        public List<Query> Queries { set; get; }

        public String SortName { set; get; }

        public Pager Pager { set; get; }

        public int ItemsPerPage { set; get; }
    }

    public class SearchViewModel
    {
        public IEnumerable<BookViewModel> Items { get; set; }

        public Query Query { set; get; }

        public String SortName { set; get; }

        public Pager Pager { set; get; }

        public int ItemsPerPage { set; get; }
    }


    public class Pager
    {
        public Pager(int totalItems, int? page, int pageSize)
        {
            // calculate total, start and end pages
            var totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)pageSize);
            var currentPage = page != null ? (int)page : 1;
            var startPage = currentPage - 5;
            var endPage = currentPage + 4;
            if (startPage <= 0)
            {
                endPage -= (startPage - 1);
                startPage = 1;
            }
            if (endPage > totalPages)
            {
                endPage = totalPages;
                if (endPage > 10)
                {
                    startPage = endPage - 9;
                }
            }

            TotalItems = totalItems;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPages = totalPages;
            StartPage = startPage;
            EndPage = endPage;
        }

        public int TotalItems { get; private set; }
        public int CurrentPage { get; private set; }
        public int PageSize { get; private set; }
        public int TotalPages { get; private set; }
        public int StartPage { get; private set; }
        public int EndPage { get; private set; }
    }
}