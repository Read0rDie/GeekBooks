using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekBooks.Models
{
    public class CreditCard
    {
        [Key]
        public int CID { get; set; }

        
        [ForeignKey("UserAccount")]
        public string UID { get; set; }

        [Display(Name = "Name on Card")]
        [StringLength(20)]
        [Required(ErrorMessage = "Name on card is Required")]
        public string CardName { get; set; }

        [Display(Name = "Card Number")]
        [StringLength(16, MinimumLength = 16)]
        [Required(ErrorMessage = "Card Number is Required")]
        public string CardNumber { get; set; }

        [Display(Name = "Expiration Month")]
        [Required(ErrorMessage = "Expiration Month is Required")]
        public int ExpirationMonth { get; set; }

        [Display(Name = "Expiration Year")]
        [Required(ErrorMessage = "Expiration year is Required")]
        public int ExpirationYear { get; set; }

        [Display(Name = "Security Code of Card")]
        [Required(ErrorMessage = "Security Code is Required")]
        public int SecurityCode { get; set; }

        public virtual ApplicationUser UserAccount { get; set; }
    }
}