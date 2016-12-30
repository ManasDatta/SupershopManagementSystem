using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SMSDataLayer
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Name cannot be empty")]
        [RegularExpression("^([a-zA-Z.-]*)$", ErrorMessage = "Enter only alphabets")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Price cannot be empty")]
        public double ProductPrice { get; set; }

        [Required(ErrorMessage = "Quantity cannot be empty")]
        public int ProductQuantity { get; set; }

        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        internal object ToList()
        {
            throw new NotImplementedException();
        }
    }
}