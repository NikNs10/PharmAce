using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PharmAce.Models;

namespace PharmAce.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        //public DbSet<CustomUser> CustomUsers { get; set; }
        public DbSet<Drug> Drugs { get; set; }
        //public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<SalesReport> SalesReport { get; set; }
        
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<TransactionDetail> TransactionDetails { get; set; }
        public DbSet<Category> Category { get; set; }

        public DbSet<Inventory> Inventory {get; set;}
        //public DbSet<Transaction> Transactions { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Drugs to Category relationship
            modelBuilder.Entity<Drug>()
                .HasOne(d => d.Category)
                .WithMany(c => c.Drugs)
                .HasForeignKey(d => d.CategoryId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            // Configure Inventory to Drugs relationship
            modelBuilder.Entity<Inventory>()
                .HasOne(i => i.Drugs)
                .WithMany(d => d.Inventory)
                .HasForeignKey(i => i.DrugId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            // Configure OrderItems relationships
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Drugs)
                .WithMany(d => d.OrderItems)
                .HasForeignKey(oi => oi.DrugId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Orders)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            // Configure Orders relationships
            modelBuilder.Entity<Order>()
                .HasOne(o => o.TransactionDetails)
                .WithMany(td => td.Orders)
                .HasForeignKey(o => o.TransactionId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            // Fix the circular reference between Orders and OrderItems
            // Remove the OrderItemId from Orders as it creates a circular reference
            modelBuilder.Entity<Order>()
                .Ignore(o => o.OrderItemId);

            // Configure SalesReport to Orders relationship (one-to-one)
            modelBuilder.Entity<SalesReport>()
                .HasOne(sr => sr.Order)
                .WithOne(o => o.SalesReport)
                .HasForeignKey<SalesReport>(sr => sr.OrderId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Inventory>()
                .HasOne(o => o.User)
                .WithMany(u => u.Inventories)
                .HasForeignKey(o => o.SupplierId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            // Ensure table names are singular
            modelBuilder.Entity<Category>().ToTable("Category");
            modelBuilder.Entity<Inventory>().ToTable("Inventory");
            modelBuilder.Entity<SalesReport>().ToTable("SalesReport");
            
            // Avoid conflicts with ASP.NET Identity's Users table
        }
    //      protected override void OnModelCreating(ModelBuilder modelBuilder)
    //     {
    //         modelBuilder.Entity<OrderItem>()
    //             .HasOne(oi => oi.Order)
    //             .WithMany(o => o.OrderItems)
    //             .HasForeignKey(oi => oi.OrderId)
    //             .OnDelete(DeleteBehavior.Restrict);

    //         modelBuilder.Entity<OrderItem>()
    //             .HasOne(oi => oi.Drug)
    //             .WithMany(d => d.OrderItems)
    //             .HasForeignKey(oi => oi.DrugId);

    //         foreach (var entityType in modelBuilder.Model.GetEntityTypes())
    // {
    //     foreach (var property in entityType.GetProperties())
    //     {
    //         if (property.ClrType == typeof(decimal) || property.ClrType == typeof(decimal?))
    //         {
    //             property.SetPrecision(18);
    //             property.SetScale(2);
    //         }
    //     }
    // }

    //       modelBuilder.Entity<TransactionDetail>()
    //     .HasOne(td => td.Drug)
    //     .WithMany()
    //     .HasForeignKey(td => td.DrugId)
    //     .OnDelete(DeleteBehavior.Restrict);

    //     modelBuilder.Entity<OrderItem>()
    //         .HasOne(oi => oi.Drug)
    //         .WithMany()
    //         .HasForeignKey(oi => oi.DrugId)
    //         .OnDelete(DeleteBehavior.Restrict);
    //     }
    }
}