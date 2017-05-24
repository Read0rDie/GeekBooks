using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GeekBooks.Models
{
    public class Profile
    {
        [Key]
        public UserAccount User { get; set; }
        public List<Address> BillingAddresses { get; set; }
        public List<Address> ShippingAddresses { get; set; }
        public List<CreditCard> PaymentInfo { get; set; }
        public List<Purchase> PurchaseHistory { get; set; }
    }
}