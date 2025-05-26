using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmAce.Models
{
    // / <summary>
    // / Represents a pharmaceutical drug in the inventory
    // / </summary>
    public class Drug
    {
        public Drug()
        {
            OrderItems = new HashSet<OrderItem>();
            Inventory = new HashSet<Inventory>();
        }

        [Key]
        public Guid DrugId { get; set; }

        [Required]
        public Guid SupplierId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int Stock { get; set; }

        [Required]
        public DateTime DrugExpiry{get;set;}

        [Required]
        public decimal Price { get; set; }

        [Required]
        public Guid CategoryId { get; set; }

        // Navigation properties
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual ICollection<Inventory> Inventory { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public virtual Category? Category { get; set; }
        // public ICollection<OrderItem> OrderItems { get; set; }


        // public virtual Inventory  Inventory  { get; set; }
        // public virtual Category Category { get; set; }


        // [ForeignKey("CategoryId")]
        // public Category Category { get; set; }

    
        // [Required(ErrorMessage = "Manufacturer is required")]
        // [StringLength(100, ErrorMessage = "Manufacturer cannot exceed 100 characters")]
        // public string Manufacturer { get; set; } = string.Empty;


        // [Required(ErrorMessage = "Expiration date is required")]
        // public DateTime ExpirationDate { get; set; }

        // [Required(ErrorMessage = "Batch number is required")]
        // [StringLength(50, ErrorMessage = "Batch number cannot exceed 50 characters")]
        // public string BatchNumber { get; set; } = string.Empty;

        // Navigation properties
        // public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
