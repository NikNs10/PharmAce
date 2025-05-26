using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmAce.Models.DTO
{
    public class DrugInventoryDto
    {
        public Guid InventoryId { get; set; }
        public Guid DrugId { get; set; }
        public string Name { get; set; }
        public DateTime DrugExpiry { get; set; }
        public string Description{get;set;}
        public int Stock{get;set;}
        public decimal Price{get;set;}
        public Guid SupplierId { get; set; }
        public Guid CategoryId{get;set;}
    }
}