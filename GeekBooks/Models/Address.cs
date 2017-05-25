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
        [ForeignKey("UserAccount")]
        public int UID { get; set; }

        [Key]
        public int AID { get; set; }

        [StringLength(255)]
        [Required(ErrorMessage = "Street Address is Required")]
        public string StreetAddress { get; set; }

        [StringLength(255)]
        public string AddressTwo { get; set; }

        [StringLength(255)]
        [Required(ErrorMessage = "City is Required")]
        public string City { get; set; }

        [StringLength(255)]
        [Required(ErrorMessage = "State/ Province is Required")]
        public string State_Province { get; set; }

        [StringLength(255)]
        [Required(ErrorMessage = "Country is Required")]
        public string Country { get; set; }

        [StringLength(20)]
        [Required(ErrorMessage = "Postal code is Required")]
        public string Postal { get; set; }

        public virtual UserAccount UserAccount { get; set; }
    }
}