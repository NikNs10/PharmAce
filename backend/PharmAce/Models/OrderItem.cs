using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmAce.Models
{
    public class OrderItem
    {
        [Key]
        public Guid OrderItemId{get;set;}

        [Required]
        public Guid OrderId{get;set;}

        [Required]
        public Guid DrugId{get;set;}

        [Required]
        public long Quantity{get;set;}

        [ForeignKey(nameof(DrugId))]
        public virtual Drug Drugs { get; set; }

        [ForeignKey(nameof(OrderId))]
        public virtual Order Orders { get; set; }
        // public virtual Drug Drugs { get; set; }
        // public virtual Order Orders { get; set; }
    }
}