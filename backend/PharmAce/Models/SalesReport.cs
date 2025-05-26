using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmAce.Models
{
    public class SalesReport
    {   
        
        [Key]
        public Guid OrderId{get;set;}

        [Required]
        public long OrderDetails{get;set;}

        [Required]
        public long TotalSales{get;set;}

         [ForeignKey(nameof(OrderId))]
        public virtual Order Order { get; set; }

        // public virtual Order Orders { get; set; }
        
        // [Key]
        // public Guid ReportId { get; set; }
        // public DateTime Date { get; set; }
        // public double TotalSales { get; set; }
    }
}