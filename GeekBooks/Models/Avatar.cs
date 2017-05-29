using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekBooks.Models
{
    public class Avatar
    {
        [ForeignKey("UserAccount")]
        public int UID { get; set; }

        [Key]
        public int AVATARID { get; set; }

        [Required]
        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }

        public virtual Account UserAccount { get; set; }
    }
}