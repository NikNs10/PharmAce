using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PharmAce.Data;
using PharmAce.Models.DTO;
using PharmAce.Services.Interface;

namespace PharmAce.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;

        private readonly IConfiguration _config;

        public OrderService(ApplicationDbContext context , IConfiguration config){
            _context = context;
            _config = config;
        }

       
        public async Task<IEnumerable<OrderDto>> ViewOrder(){
            var order=await _context.Orders.ToListAsync();
            var orderdto=order.Select(p=>new OrderDto{
                DoctorName=_context.Users.Where(t=>t.UserId==p.DoctorId).Select(s=>s.Name).ToString(),
                OrderId=p.OrderId.ToString(),
                Status=p.Status.ToString(),
                OrderDate=p.OrderDate,
                TotalAmount=p.TotalAmount,
                TransactionId=p.TransactionId.ToString(),
                OrderItem=_context.OrderItems.Where(s=>p.OrderItemId==s.OrderItemId).ToList().ToString()
                }
            
            );

            return orderdto;
        
        }
    }
}