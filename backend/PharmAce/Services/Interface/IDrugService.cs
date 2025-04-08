using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PharmAce.Models.DTO;

namespace PharmAce.Services.Interface
{
    public interface IDrugService
    {
        Task<IEnumerable<DrugDto>> ViewDrugs();
        Task AddDrugs(DrugDto drugDto);
        Task<bool> EditDrugs(DrugDto drugDto);
        Task<bool> DeleteDrugs(string Name);
    }
}