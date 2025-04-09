using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmAce.Models
{
    /// <summary>
    /// Represents a pharmaceutical drug in the inventory
    /// </summary>
    public class Drug
    {
        [Key]
        public Guid DrugId { get; set; }

        [Required]
        public Guid SupplierId{get;set;}

        [Required(ErrorMessage = "Drug name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string Name { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string Description { get; set; } = string.Empty;


        [Range(0, int.MaxValue, ErrorMessage = "Stock cannot be negative")]
        public int Stock { get; set; }

        //[Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        
        [Required(ErrorMessage = "Category is required")]
        public Guid CategoryId { get; set; }

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
        // public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        //public Category? Category { get; set; }
    }
}
