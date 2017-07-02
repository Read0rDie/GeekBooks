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
        [StringLength(256)]
        public string BookName { get; set; }

        [Display(Name = "Cover Image")]
        [DataType(DataType.ImageUrl)]
        public string CoverUrl { get; set; }

        [Display(Name = "Genre")]
        [StringLength(255)]
        public string Genre { get; set; }
        //Should constrain to specific strings

        //[Display(Name = "Genre")]
        //[StringLength(255)]
        //public ICollection<string> Genre { get; set; }

        //[Display(Name = "Publication Date")]
        //[Required(ErrorMessage = "Please enter a publication date")]
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString ="{0:MM/dd/yyyy}")]
        //public DateTime PublishDate { get; set; }

        //[Display(Name = "Page Count")]
        //public int Pages { get; set; }

        [Display(Name = "Synopsis")]
        [StringLength(2000)]
        public string Synopsis { get; set; }

        [Display(Name = "Publisher")]
        public string Publisher { get; set; }

        [Display(Name = "Price")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Display(Name = "Number in stock")]
        public int Stock { get; set; }

        public virtual BookAuthor Author { get; set;}

        public virtual ICollection<BookRating> BookRatings { get; set; }
        
    }

    public class BookViewModel
    {        
        public int BookID { get; set; }
       
        //public int AuthorID { get; set; }

        [Display(Name = "Book Name")]
        public string BookName { get; set; }

        [Display(Name = "Cover Image")]
        public string CoverUrl { get; set; }

        [Display(Name = "Genre")]
        public string Genre { get; set; }
        //Should constrain to specific strings

        [Display(Name = "Synopsis")]
        public string Synopsis { get; set; }

        //[Display(Name = "Publisher")]
        //public string Publisher { get; set; }

        [Display(Name = "Price")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Display(Name = "Number in stock")]
        public int Stock { get; set; }

        [Display(Name = "Rating")]
        public double AvgRating { get; set; }

        public virtual BookAuthor Author { get; set; }

        //public virtual List<BookRating> BookRatings { get; set; }
    }
}