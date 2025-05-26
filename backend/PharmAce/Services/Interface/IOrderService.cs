using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PharmAce.Models.DTO;

namespace PharmAce.Services.Interface
{
    public interface IOrderService
    {
        // Task<IEnumerable<OrderDto>> ViewOrder();
        Task<IEnumerable<OrderDto>> GetAllOrdersAsync();
        Task<OrderDto> GetOrderByIdAsync(Guid orderId);
        Task<bool> CreateOrderAsync(CreateOrderDto createOrderDto);
        Task<bool> UpdateOrderAsync(Guid orderId, UpdateOrderDto updateOrderDto);
        Task<bool> DeleteOrderAsync(Guid orderId);

        Task<int> GetOrderCountAsync();
    }
}