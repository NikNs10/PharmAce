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

        public DbSet<User> Users { get; set; }
        public DbSet<Drug> Drugs { get; set; }
        //public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<SalesReport> SalesReports { get; set; }
        
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<TransactionDetail> TransactionDetails { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<Inventory> Inventories {get; set;}
        //public DbSet<Transaction> Transactions { get; set; }


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