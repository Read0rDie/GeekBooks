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

        [Display(Name = "Genre")]
        [StringLength(255)]
        public string Genre { get; set; }
        //Should constrain to specific strings

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
    }
}