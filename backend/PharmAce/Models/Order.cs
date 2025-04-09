using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PharmAce.Models
{
    public enum OrderStatus { Pending, Verified}
    public class Order
    {
        
        [Key]
        public Guid OrderId{get;set;}

        [Required]
        public Guid DoctorId{get;set;}

        [Required]
        public OrderStatus Status{get;set;}
        
        [Required]
        public long OrderDate{get;set;}

        [Required]
        public decimal TotalAmount{get;set;}

        
        public Guid? TransactionId{get;set;}

        [Required]
        public Guid OrderItemId{get;set;}

        
    }
}