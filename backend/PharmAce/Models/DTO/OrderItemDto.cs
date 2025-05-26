using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PharmAce.Models.DTO
{
    public class OrderItemDto
    {
        [Required]
        public Guid DrugId { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}