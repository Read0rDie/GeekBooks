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
        [StringLength(258)]
        [Required]
        public string UID { get; set; }

        [Key]
        public int AVATARID { get; set; }

        [Required]
        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }

        public virtual ApplicationUser UserAccount { get; set; }
    }
}