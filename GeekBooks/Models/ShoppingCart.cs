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
        [StringLength(255)]
        [Required(ErrorMessage = "Quantity is Required")]
        public string Quantity { get; set; }

        [Display(Name = "Save For Later")]
        public Boolean SaveForLater { get; set; }

        public virtual Book Book { get; set; }

        [ForeignKey("UID")]
        public virtual ApplicationUser User { get; set; }

        //[Display(Name = "Address 1")]
        //[StringLength(255)]
        //[Required(ErrorMessage = "Street Address is Required")]
        //public string Address1 { get; set; }

        //[Display(Name = "Address 2")]
        //[StringLength(255)]
        //public string Address2 { get; set; }

        //[Display(Name = "City")]
        //[StringLength(255)]
        //[Required(ErrorMessage = "City is Required")]
        //public string City { get; set; }

        //[Display(Name = "State/Province")]
        //[StringLength(255)]
        //[Required(ErrorMessage = "State/Province is Required")]
        //public string State_Province { get; set; }

        //[Display(Name = "Country")]
        //[StringLength(255)]
        //[Required(ErrorMessage = "Country is Required")]
        //public string Country { get; set; }

        //[Display(Name = "Postal")]
        //[StringLength(20)]
        //[Required(ErrorMessage = "Postal code is Required")]
        //public string Postal { get; set; }

        //[Display(Name = "Is shipping?")]
        //public Boolean IsShipping { get; set; }

        //public virtual ApplicationUser UserAccount { get; set; }
    }
}