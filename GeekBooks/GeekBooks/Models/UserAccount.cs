using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekBooks.Models
{
    public class UserAccount
    {
        [Key]
        public int UID { get; set; }

        [Required(ErrorMessage = "First Name is Required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is Required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Nick Name is Required")]
        [Index(IsUnique = true)]
        public string NickName { get; set; }

        [Required(ErrorMessage = "Email is Required")]
        [StringLength(50)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [DataType(DataType.Password)]
        public string PassWord { get; set; }

        [System.ComponentModel.DataAnnotations.Compare("PassWord", ErrorMessage = "Please confirm your password.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

    }
}