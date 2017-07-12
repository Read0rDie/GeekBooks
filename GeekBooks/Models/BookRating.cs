using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekBooks.Models
{
    public class BookRating
    {       
        
        public string UID { get; set; }

        [ForeignKey("Book")]
        public int BookID { get; set; }

        [Key]
        public int BookRatingID { get; set; }

        [Required]
        [StringLength(256)]
        [Display(Name = "Comment")]
        public string Comment { get; set; }

        [Required]
        [Display(Name = "Rating")]
        public int Rating { get; set; }

        [Display(Name = "Date Modified")]        
        [DataType(DataType.Date)]
        public DateTime PublishDate { get; set; }

        public virtual Book Book { get; set; }

        [ForeignKey("UID")]
        public virtual ApplicationUser User { get; set; }
    }

    public class BookRatingViewModel
    {
        public string UID { get; set; }

        public int BookID { get; set; }

        public int BookRatingID { get; set; }

        [Display(Name = "Comment")]
        public string Comment { get; set; }

        [Display(Name = "Rating")]
        public int Rating { get; set; }

        public Book Book { get; set; }

        public ApplicationUser User { get; set; }
    }
}