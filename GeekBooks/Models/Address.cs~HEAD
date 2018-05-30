using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekBooks.Models
{
    public class Address
    {
        //[ForeignKey("UserAccount")]        
        public string UID { get; set; }

        [Key]
        public int AID { get; set; }

        [Display(Name = "Address 1")]
        [StringLength(255)]
        [Required(ErrorMessage = "Street Address is Required")]
        public string Address1 { get; set; }

        [Display(Name = "Address 2")]
        [StringLength(255)]
        public string Address2 { get; set; }

        [Display(Name = "City")]
        [StringLength(255)]
        [Required(ErrorMessage = "City is Required")]
        public string City { get; set; }

        [Display(Name = "State/Province")]
        [StringLength(255)]
        [Required(ErrorMessage = "State/Province is Required")]
        public string State_Province { get; set; }

        [Display(Name = "Country")]
        [StringLength(255)]
        [Required(ErrorMessage = "Country is Required")]
        public string Country { get; set; }

        [Display(Name = "Postal")]
        [StringLength(20)]
        [Required(ErrorMessage = "Postal code is Required")]
        public string Postal { get; set; }

        [Display(Name = "Is shipping?")]
        public Boolean IsShipping { get; set; }

        //public virtual ApplicationUser UserAccount { get; set; }
    }

    public class AddressViewModel
    {
        [Display(Name = "Address 1")]
        public string Address1 { get; set; }

        [Display(Name = "Address 2")]
        public string Address2 { get; set; }
        
        [Display(Name = "City")]
        public string City { get; set; }
        
        [Display(Name = "State/Province")]
        public string State_Province { get; set; }

        [Display(Name = "Country")]
        public int Country { get; set; }

        [Display(Name = "Postal")]
        public string Postal { get; set; }
    }

}