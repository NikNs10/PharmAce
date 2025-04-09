using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PharmAce.Models.DTO;

namespace PharmAce.Services.Interface
{
    public interface IInventoryService
    {
        Task<IEnumerable<InventoryDto>> ViewInventory();
        Task<bool> AddInInventory(InventoryDto inventoryDto);
        Task<bool> DeleteInInventory(InventoryDto inventoryDto);
    }
}