using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmAce.Models.DTO
{
    public class DrugDto
    {
        //public Guid DrugId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        
        public int Stock { get; set; }
        public decimal Price { get; set; }
        
    }
}