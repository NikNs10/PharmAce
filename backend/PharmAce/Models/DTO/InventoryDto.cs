using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PharmAce.Models.DTO
{
    public class InventoryDto
    {
        public Guid InventoryId { get; set; }
        public Guid DrugId { get; set; }
        public string DrugName { get; set; }
        public DateTime DrugExpiry { get; set; }
        public long DrugQuantity { get; set; }
        public Guid SupplierId { get; set; }
    }

    public class CreateInventoryDto
    {
        public Guid DrugId { get; set; }
        public DateTime DrugExpiry { get; set; }
        public long DrugQuantity { get; set; }
        public Guid SupplierId { get; set; }
    }

    public class UpdateInventoryDto
    {
        public Guid DrugId { get; set; }
        public DateTime DrugExpiry { get; set; }
        public long DrugQuantity { get; set; }
        public Guid SupplierId { get; set; }
    }
}