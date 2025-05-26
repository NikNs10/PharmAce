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

        
        [HttpGet]
        [Authorize(Roles = "Admin,Supplier")]
        public async Task<ActionResult<IEnumerable<InventoryDto>>> GetAllInventory()
        {
            var inventories = await _inventoryService.GetAllInventoryAsync();
            return Ok(inventories);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Supplier")]
        public async Task<ActionResult<InventoryDto>> GetInventoryById(Guid id)
        {
            var inventory = await _inventoryService.GetInventoryByIdAsync(id);
            if (inventory == null)
                return NotFound($"Inventory with ID {id} not found");

            return Ok(inventory);
        }

        [HttpGet("drug/{drugId}")]
        [Authorize(Roles = "Admin,Supplier")]
        public async Task<ActionResult<IEnumerable<InventoryDto>>> GetInventoryByDrugId(Guid drugId)
        {
            var inventories = await _inventoryService.GetInventoryByDrugIdAsync(drugId);
            return Ok(inventories);
        }

        [HttpGet("supplier/{supplierId}")]
        [Authorize(Roles = "Admin,Supplier")]
        public async Task<ActionResult<IEnumerable<InventoryDto>>> GetInventoryBySupplierId(Guid supplierId)
        {
            var inventories = await _inventoryService.GetInventoryBySupplierIdAsync(supplierId);
            return Ok(inventories);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Supplier")]
        public async Task<ActionResult<InventoryDto>> CreateInventory([FromBody] DrugInventoryDto inventoryDto)
        {
            try
            {
                var newInventory = await _inventoryService.CreateInventoryAsync(inventoryDto);
                return Ok(newInventory);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Authorize(Roles = "Admin,Supplier")]
        public async Task<ActionResult<InventoryDto>> UpdateInventory(DrugInventoryDto inventoryDto)
        {
            try
            {
                var updatedInventory = await _inventoryService.UpdateInventoryAsync(inventoryDto);
                if (updatedInventory == null)
                    return NotFound($"Inventory with ID {inventoryDto.DrugId} not found");

                return Ok(updatedInventory);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Supplier")]
        public async Task<ActionResult> DeleteInventory(Guid id)
        {
            var result = await _inventoryService.DeleteInventoryAsync(id);
            if (!result)
                return NotFound($"Inventory with ID {id} not found");

            return NoContent();
        }
    }
}