using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PharmAce.Models.DTO;
using PharmAce.Services.Interface;

namespace PharmAce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : ControllerBase
    {
       
        private readonly IInventoryService _inventoryService;

        public InventoryController(IInventoryService service){
            _inventoryService=service;
        }

        [Authorize(Roles ="Admin,Supplier")]
        [HttpGet]
        public async Task<IActionResult> ViewInventory(){
            var result =await _inventoryService.ViewInventory();
            if(result==null){
                return BadRequest("Inventory is Empty");
            }
            return Ok(result);
        }


        [HttpPut]
        public async Task<IActionResult> AddInInventory(InventoryDto inventoryDto){
            var result=await _inventoryService.AddInInventory(inventoryDto);
            return Ok(new {message="Added Succesfully"});
        } 

        [HttpDelete]
        public async Task<IActionResult> DeleteInInventory(InventoryDto inventoryDto){
            var result=await _inventoryService.DeleteInInventory(inventoryDto);
            if(result==false){
                return BadRequest(new {message="Drug not found"});
            }
            return Ok(new {message="Deleted Succesfully"});
        } 
    }
}