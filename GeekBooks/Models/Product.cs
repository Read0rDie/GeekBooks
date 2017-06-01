using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekBooks.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }

        [StringLength(255)]
        [Index]
        public string ProductName { get; set; }


        public decimal Price { get; set; }


        public int Stock { get; set; }
    }
}