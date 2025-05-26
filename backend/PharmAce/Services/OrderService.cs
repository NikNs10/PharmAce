using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PharmAce.Data;
using PharmAce.Models;
using PharmAce.Models.DTO;
using PharmAce.Services.Interface;

namespace PharmAce.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;
         private readonly ITransactionDetailsService _trans;

        private readonly IConfiguration _config;

        public OrderService(ApplicationDbContext context , IConfiguration config , ITransactionDetailsService trans){
            _context = context;
            _config = config;
            _trans = trans;
        }

       
        // public async Task<IEnumerable<OrderDto>> ViewOrder(){
        //     var order=await _context.Orders.ToListAsync();
        //     var orderdto=order.Select(p=>new OrderDto{
        //         DoctorName=_context.Users.Where(t=>t.UserId==p.DoctorId).Select(s=>s.Name).ToString(),
        //         OrderId=p.OrderId.ToString(),
        //         Status=p.Status.ToString(),
        //         OrderDate=p.OrderDate,
        //         TotalAmount=p.TotalAmount,
        //         TransactionId=p.TransactionId.ToString(),
        //         OrderItem=_context.OrderItems.Where(s=>p.OrderItemId==s.OrderItemId).ToList().ToString()
        //         }
            
        //     );

        //     return orderdto;
        
        // }

                public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
        {
            var orders = await _context.Orders.ToListAsync();
            return orders.Select(o => new OrderDto
            {
                OrderId = o.OrderId,
                UserId = o.UserId,
                Status = o.Status,
                OrderDate = o.OrderDate,
                TotalAmount = o.TotalAmount,
                TransactionId = o.TransactionId,
    
            });
        }

        public async Task<OrderDto> GetOrderByIdAsync(Guid orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null)
                return null;

            return new OrderDto
            {
                OrderId = order.OrderId,
                UserId = order.UserId,
                Status = order.Status,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                TransactionId = order.TransactionId,
            };
        }

        public async Task<bool> CreateOrderAsync([FromBody]CreateOrderDto createOrderDto)
        {
            // Create transaction details first
            var createTransactionDto = new CreateTransactionDetailsDto
            {
                Status = TransactionStatus.Pending,
                Date = DateTime.UtcNow,
                PaymentMethod = createOrderDto.PaymentMethod,
                amount = createOrderDto.TransactionAmount
            };

            var transactionDto = await _trans.CreateTransactionDetailsAsync(createTransactionDto);

            // Create order with transaction ID
            var order = new Order
            {
                OrderId = Guid.NewGuid(),
                UserId = createOrderDto.UserId,
                Status = createOrderDto.Status,
                OrderDate = createOrderDto.OrderDate,
                TotalAmount = createOrderDto.TotalAmount,
                TransactionId = transactionDto.TransactionId,
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return true;

            // return new OrderDto
            // {
            //     OrderId = order.OrderId,
            //     UserId = order.UserId,
            //     Status = order.Status,
            //     OrderDate = order.OrderDate,
            //     TotalAmount = order.TotalAmount,
            //     TransactionId = order.TransactionId
            // };
        }

        public async Task<bool> UpdateOrderAsync(Guid orderId, UpdateOrderDto updateOrderDto)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null)
                return false;

            order.Status = updateOrderDto.Status;
            order.TotalAmount = updateOrderDto.TotalAmount;

            _context.Orders.Update(order);
            await _context.SaveChangesAsync();

            return true;

            // return new OrderDto
            // {
            //     OrderId = order.OrderId,
            //     UserId = order.UserId,
            //     Status = order.Status,
            //     OrderDate = order.OrderDate,
            //     TotalAmount = order.TotalAmount,
            //     TransactionId = order.TransactionId,
            // };
        }

        public async Task<bool> DeleteOrderAsync(Guid orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null)
                return false;

            // Delete transaction details if exists
            if (order.TransactionId.HasValue)
            {
                await _trans.DeleteTransactionDetailsAsync(order.TransactionId.Value);
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<int> GetOrderCountAsync()
        {
            return await _context.Orders.CountAsync();
        }
    }
}
