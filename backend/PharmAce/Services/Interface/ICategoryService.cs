using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PharmAce.Models.DTO;

namespace PharmAce.Services.Interface
{
    public interface ICategoryService
    {
        Task<bool> AddCategory(CategoryDto categoryDto);
        Task<IEnumerable<CategoryDto>> GetCategory();
        Task<bool> EditCategory(CategoryDto categoryDto,Guid id);
    }
}