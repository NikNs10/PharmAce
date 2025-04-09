using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PharmAce.Services.Interface;

namespace PharmAce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
         private readonly IOrderService _orderService;

        public OrderController(IOrderService service){
            _orderService=service;
        }

        [Authorize]
        [HttpGet("view-orders")]
        public async Task<IActionResult> ViewOrder(){
            var order=await _orderService.ViewOrder();
            return Ok(order);
        }
    }
}