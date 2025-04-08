using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PharmAce.Models
{
    public class SalesReport
    {   
        
        [Key]
        public long OrderId{get;set;}

        [Required]
        public long OrderDetails{get;set;}

        [Required]
        public long TotalSales{get;set;}
        
        // [Key]
        // public Guid ReportId { get; set; }
        // public DateTime Date { get; set; }
        // public double TotalSales { get; set; }
    }
}