using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmAce.Models.DTO
{
    public class OrderDto
    {
        public string OrderId{get;set;}

        public string DoctorName{get;set;}

        public string Status{get;set;}
        
        public long OrderDate{get;set;}

        public decimal TotalAmount{get;set;}

        public string TransactionId{get;set;}

        public string OrderItem{get;set;}
    }
}