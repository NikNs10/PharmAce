using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PharmAce.Models
{
    public class Category
    {
          public Category()
        {
            Drugs = new HashSet<Drug>();
        }

        [Key]
        public Guid CategoryId { get; set; }
        //public int CategoryId { get; set; }

        [Required]
        public string CategoryName { get; set; }

    // //Navigation.
        public virtual ICollection<Drug> Drugs { get; set; }

    }
    
}

