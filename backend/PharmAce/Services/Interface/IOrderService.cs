using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PharmAce.Models.DTO;

namespace PharmAce.Services.Interface
{
    public interface IOrderService
    {
          Task<IEnumerable<OrderDto>> ViewOrder();
    }
}