using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SMSDataLayer
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Name cannot be empty")]
        [RegularExpression("^([a-zA-Z.-]*)$", ErrorMessage = "Enter only alphabets")]
        public string CategoryName { get; set; }

        public List<Product> Products { get; set; }
    }
}