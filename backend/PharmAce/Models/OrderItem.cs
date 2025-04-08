using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
    }
}