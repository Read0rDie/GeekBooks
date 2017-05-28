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
        [ForeignKey("UserAccount")]
        public int UID { get; set; }

        [StringLength(20)]
        [Required(ErrorMessage = "Name on card is Required")]
        public string CardName { get; set; }

        [StringLength(16, MinimumLength = 16)]
        [Required(ErrorMessage = "Card Number is Required")]
        public string CardNumber { get; set; }

        public virtual Account UserAccount { get; set; }
    }
}