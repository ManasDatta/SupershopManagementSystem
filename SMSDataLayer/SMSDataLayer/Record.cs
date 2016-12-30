using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SMSDataLayer
{
    public class Record
    {
        [Key]
        public int InvoiceId { get; set; }

        [Required(ErrorMessage = "Date cannot be empty")]
        public string InvoiceDate { get; set; }

        [Required(ErrorMessage = "Total Price cannot be empty")]
        public double InvoiceTotal { get; set; }

        public int SalesmanId { get; set; }
        [ForeignKey("SalesmanId")]
        public Salesman Salesman { get; set; }
        public Product Products { get; set; }
    }
}