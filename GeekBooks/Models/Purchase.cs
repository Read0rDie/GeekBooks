using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekBooks.Models
{
    public class Purchase
    {
        [Key]
        public int PurchaseID { get; set; }

        [ForeignKey("UserAccount")]
        public int UID { get; set; }

        [ForeignKey("Product")]
        public int PID { get; set; }
        
        public Address BillingAddress { get; set; }        
        
        public Address ShipingAddress { get; set; }

        public virtual UserAccount UserAccount { get; set; }
        public virtual Product Product { get; set; }

    }
}