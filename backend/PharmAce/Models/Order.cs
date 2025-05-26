using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PharmAce.Models
{
     public enum OrderStatus
    {
        Pending,
        Verified
    }
    
    public class Order
    {
        public Order()
        {
            OrderItems = new HashSet<OrderItem>();
        }

        [Key]
        public Guid OrderId { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public OrderStatus Status { get; set; }
        
        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public decimal TotalAmount { get; set; }

        public Guid? TransactionId { get; set; }

        // This property creates a circular reference and should be removed
        // It's being ignored in the DbContext configuration
        public Guid OrderItemId { get; set; }

        // Navigation properties
        [ForeignKey(nameof(TransactionId))]
        public virtual TransactionDetail TransactionDetails { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }

        public virtual SalesReport SalesReport { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser User { get; set; }
    }
}