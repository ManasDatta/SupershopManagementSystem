using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SMSDataLayer
{
    public class Salesman
    {
        [Key]
        public int SalesmanId { get; set; }

        [Required(ErrorMessage = "Name cannot be empty")]
        [RegularExpression("^([a-zA-Z.-]*)$", ErrorMessage = "Enter only alphabets")]
        public string SalesmanName { get; set; }

        [Required(ErrorMessage = "Email cannot be empty")]
        [RegularExpression(@"^[a-zA-Z]+(([\'\,\.\- ][a-zA-Z ])?[a-zA-Z]*)*\s+<(\w[-._\w]*\w@\w[-._\w]*\w\.\w{2,3})>$|^(\w[-._\w]*\w@\w[-._\w]*\w\.\w{2,3})$", ErrorMessage = "Invalid Email")]
        [StringLength(450)]
        [Index(IsUnique = true)]
        public string SalesmanEmail { get; set; }

        [Required(ErrorMessage = "Phone cannot be empty")]
        [RegularExpression(@"^(?:\+?88)?01[15-9]\d{8}$", ErrorMessage = "Invalid Phone")]
        [StringLength(450)]
        [Index(IsUnique = true)]
        public string SalesmanPhone { get; set; }

        [Required(ErrorMessage = "Address cannot be empty")]
        public string SalesmanAddress { get; set; }

        [Required(ErrorMessage = "DOB cannot be empty")]
        public string SalesmanDOB { get; set; }

        [Required(ErrorMessage = "Joining Date cannot be empty")]
        public string SalesmanJoiningDate { get; set; }

        [Required(ErrorMessage = "Salary cannot be empty")]
        public double SalesmanSalary { get; set; }

        [Required(ErrorMessage = "Password cannot be empty")]
        [MinLength(6, ErrorMessage = "Length should be above 6")]
        public string SalesmanPassword { get; set; }

        public List<Record> Records { get; set; }
    }
}