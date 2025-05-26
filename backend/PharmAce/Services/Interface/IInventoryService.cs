using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PharmAce.Models.DTO;

namespace PharmAce.Services.Interface
{
    public interface IInventoryService
    {
        Task<IEnumerable<InventoryDto>> GetAllInventoryAsync();
        Task<InventoryDto> GetInventoryByIdAsync(Guid id);
        Task<IEnumerable<InventoryDto>> GetInventoryByDrugIdAsync(Guid drugId);
        Task<IEnumerable<InventoryDto>> GetInventoryBySupplierIdAsync(Guid supplierId);
        Task<InventoryDto> CreateInventoryAsync(DrugInventoryDto inventoryDto);
        Task<InventoryDto> UpdateInventoryAsync(DrugInventoryDto inventoryDto);
        Task<bool> DeleteInventoryAsync(Guid id);
    }
}