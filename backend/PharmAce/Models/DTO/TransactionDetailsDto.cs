using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmAce.Models.DTO
{
    public class TransactionDetailsDto
    {
         public Guid TransactionId { get; set; }
        public TransactionStatus Status { get; set; }
        public DateTime Date { get; set; }
        public string PaymentMethod { get; set; }
        public decimal amount { get; set; }
    }

    public class CreateTransactionDetailsDto
    {
        public TransactionStatus Status { get; set; }
        public DateTime Date { get; set; }
        public string PaymentMethod { get; set; }
        public decimal amount { get; set; }
    }
    
}