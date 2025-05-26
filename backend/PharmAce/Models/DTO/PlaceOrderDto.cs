using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PharmAce.Models.DTO
{
    public class PlaceOrderDto
    {
         [Required]
        public decimal TotalAmount { get; set; }

        [Required]
        public List<OrderItemDto> OrderItems { get; set;}
    }
}