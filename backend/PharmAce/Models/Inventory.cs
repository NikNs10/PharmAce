using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PharmAce.Models
{
    public class Inventory
    {
        [Key]
        public Guid DrugId {get; set;}

        [Required]
        public string Name {get; set;}

        [Required]
        public long DrugQuantity {get; set;}
    }
}