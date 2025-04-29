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
        Task<IEnumerable<DrugDto>> ViewDrugs();
        Task AddDrugs(DrugDto drugDto);
        Task<bool> EditDrugs(DrugDto drugDto);
        Task<bool> DeleteDrugs(string Name);

        Task<PagedResult<DrugDto>> GetFilteredDrugsAsync(
            string searchTerm,
            int page = 1,
            int pageSize = 10,
            string sortBy = "Name",
            bool ascending = true,
            Guid? categoryId = null
        );
        Task<bool> DeleteDrugAsync(Guid id);
    }
}