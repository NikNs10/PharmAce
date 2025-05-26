using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PharmAce.Data;
using PharmAce.Models;
using PharmAce.Models.DTO;
using PharmAce.Services.Interface;

namespace PharmAce.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        
        public CategoryService(ApplicationDbContext context,IConfiguration configuration){
            _context=context;
            _configuration=configuration;
        }

        public async Task<bool> AddCategory(CategoryDto categoryDto){
            var result = await _context.Category.FirstOrDefaultAsync(c => c.CategoryName == categoryDto.CategoryName);
            if(result!=null){
                return false;
            }
        
            var category= new Category{
                CategoryId=Guid.NewGuid(),
                CategoryName=categoryDto.CategoryName
            };
            await _context.Category.AddAsync(category);
            await _context.SaveChangesAsync();
            
            return true;
        }

        public async Task<IEnumerable<CategoryDto>> GetCategory(){
           var result= await _context.Category.ToListAsync();
           var category=result.Select(d=> new CategoryDto{
            CategoryId=d.CategoryId,
            CategoryName=d.CategoryName
           });
           return category;
        }

        public async Task<bool> EditCategory(CategoryDto categoryDto,Guid id){
            var result=await _context.Category.FindAsync(id);
            if(result==null){
                return false;
            }
            result.CategoryName=categoryDto.CategoryName;
            _context.Category.Update(result);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}