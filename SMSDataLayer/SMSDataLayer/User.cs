using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SMSDataLayer
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Name cannot be empty")]
        [RegularExpression("^([a-zA-Z.-]*)$", ErrorMessage = "Enter only alphabets")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Phone cannot be empty")]
        [RegularExpression(@"^(?:\+?88)?01[15-9]\d{8}$", ErrorMessage = "Invalid Phone")]
        [StringLength(450)]
        [Index(IsUnique = true)]
        public string UserPhone { get; set; }

        [Required(ErrorMessage = "Password cannot be empty")]
        [MinLength(6, ErrorMessage = "Length should be above 6")]
        public string UserPassword { get; set; }
    }
}