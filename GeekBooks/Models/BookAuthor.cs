using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekBooks.Models
{
    public class BookAuthor
    {
        [Key]
        public int AuthID { get; set; }

        [Display(Name ="Author Name")]
        [StringLength(255)]
        [Required(ErrorMessage = "Author Name is Reqiured")]
        public string AuthorName { get; set; }

        [StringLength(2000)]
        public string Blurb { get; set; }
    }
}