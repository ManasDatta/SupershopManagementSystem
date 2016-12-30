using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SMSDataLayer
{
    public class Admin
    {
        [Key]
        public int AdminId { get; set; }

        [Required(ErrorMessage = "Name cannot be empty")]
        [RegularExpression("^([a-zA-Z.-]*)$", ErrorMessage = "Enter only alphabets")]
        public string AdminName { get; set; }

        [Required(ErrorMessage = "Email cannot be empty")]
        [RegularExpression(@"^[a-zA-Z]+(([\'\,\.\- ][a-zA-Z ])?[a-zA-Z]*)*\s+<(\w[-._\w]*\w@\w[-._\w]*\w\.\w{2,3})>$|^(\w[-._\w]*\w@\w[-._\w]*\w\.\w{2,3})$", ErrorMessage = "Invalid Email")]
        [StringLength(450)]
        [Index(IsUnique = true)]
        public string AdminEmail { get; set; }

        [Required(ErrorMessage = "Phone cannot be empty")]
        [RegularExpression(@"^(?:\+?88)?01[15-9]\d{8}$", ErrorMessage = "Invalid Phone")]
        [StringLength(450)]
        [Index(IsUnique = true)]
        public string AdminPhone { get; set; }

        [Required(ErrorMessage = "Address cannot be empty")]
        public string AdminAddress { get; set; }

        [Required(ErrorMessage = "DOB cannot be empty")]
        public string AdminDOB { get; set; }
        

        [Required(ErrorMessage = "Password cannot be empty")]
        [MinLength(6, ErrorMessage = "Length should be above 6")]
        public string AdminPassword { get; set; }
    }
}