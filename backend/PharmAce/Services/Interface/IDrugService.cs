using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pharmace.API.Models.Dtos;
using PharmAce.Models.DTO;

namespace PharmAce.Services.Interface
{
    public interface IDrugService
    {
        // //Task<IEnumerable<DrugDto>> ViewDrugs();
        // Task<IEnumerable<DrugReadDto>> ViewDrugs();
        // Task AddDrugs(DrugDto drugDto);
        // Task<bool> EditDrugs(DrugDto drugDto);
        // //Task<bool> DeleteDrugs(string Name);

        //Task<bool> EditDrug(DrugDto editDrugDto,Guid id);

        Task<bool> EditDrug(DrugInventoryDto viewDrugDto);
        Task<IEnumerable<ViewDrugDto>> ViewDrugs();
        //Task AddDrugs(DrugDto drugDto,Guid id);
        Task<DrugInventoryDto> AddDrugs(DrugInventoryDto drugDto);
        Task<PagedResult<DrugReadDto>> GetFilteredDrugsAsync(
            string searchTerm,
            int page = 1,
            int pageSize = 10,
            string sortBy = "Name",
            bool ascending = true,
            Guid? categoryId = null
        );
        Task<bool> DeleteDrugs(Guid id);
        Task<bool> UpdateDrugAsync(Guid id, DrugReadDto drugDto);
    }
}