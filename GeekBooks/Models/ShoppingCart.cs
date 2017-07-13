using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekBooks.Models
{
    public class ShoppingCart
    {
        [Key]
        public int SCI { get; set; }

        public string UID { get; set; }

        [ForeignKey("Book")]
        public int BookID { get; set; }

        [Display(Name = "Quantity")]
        [Required(ErrorMessage = "Quantity is Required")]
        public int Quantity { get; set; }

        [Display(Name = "Save For Later")]
        public Boolean SaveForLater { get; set; }

        public virtual Book Book { get; set; }

        [ForeignKey("UID")]
        public virtual ApplicationUser User { get; set; }
    }

    public class ShoppingCartConfrmCViewModel
    {
        public int BookID { get; set; }
    }
}