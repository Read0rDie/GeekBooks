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

        [ForeignKey("Author")]
        public int AuthID { get; set; }

        [StringLength(255)]
        public string ProductName { get; set; }

        [DataType(DataType.ImageUrl)]
        public string CoverUrl { get; set; }

        [StringLength(255)]
        public string Genre { get; set; }
        //Should constrain to specific strings

        [StringLength(2000)]
        public string Synopsis { get; set; }

        public string Publisher { get; set; }


        public decimal Price { get; set; }


        public int Stock { get; set; }

        public virtual Author Author { get; set;}
    }
}