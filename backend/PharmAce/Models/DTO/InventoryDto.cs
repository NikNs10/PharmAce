using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PharmAce.Models.DTO
{
    public class InventoryDto
    {
        [Required]
        public string DrugName{get;set;}

        [Required]
        public long DrugQuantity{get;set;}
    }
}