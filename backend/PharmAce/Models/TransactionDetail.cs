using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PharmAce.Models
{
     public enum TransactionStatus{
        Pending,
        Completed
    }

    public class TransactionDetail
    {
        [Key]
        public Guid TransactionId{get;set;}

        [Required]
        public TransactionStatus Status{get;set;}
        
        [Required]
        public DateTime Date{get;set;}

        [Required]
        public string PaymentMethod{get;set;}

        [Required]
        public decimal amount{get;set;}
    }
}