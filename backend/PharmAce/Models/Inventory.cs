using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PharmAce.Models
{
    public class Inventory
    {
        [Key]
        public Guid InventoryId{get;set;}

        public Guid DrugId {get; set;}

        [Required]
        public string Name {get; set;}

        [Required]
        public DateTime ExpiryDate{get;set;}

        [Required]
        public long DrugQuantity {get; set;}

        [Required]
        public Guid SupplierId{get;set;}

        
        
    }
}