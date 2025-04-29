using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PharmAce.Data;
using PharmAce.Models.DTO;
using PharmAce.Services.Interface;
using PharmAce.Models;
using Pharmace.API.Models.Dtos;

namespace PharmAce.Services
{
    public class DrugService : IDrugService
    {
        private readonly ApplicationDbContext _context;
        private IConfiguration _config;
        public DrugService(ApplicationDbContext context , IConfiguration config){
            _context = context;
            _config = config;
        }
        public async Task<IEnumerable<DrugDto>> ViewDrugs(){
            var res = await _context.Drugs.ToListAsync();
            var drug =res.Select(d => new DrugDto{
                Name = d.Name,
                Description = d.Description,
                Stock = d.Stock,                
                Price = d.Price
            });

            return drug;
        }

        public async Task AddDrugs(DrugDto drugDto){
            var drug = new Drug{
                DrugId = Guid.NewGuid(),
                Name = drugDto.Name,
                Description = drugDto.Description,
                Stock = drugDto.Stock,
                Price = drugDto.Price,
                CategoryId=Guid.NewGuid(),
                SupplierId=Guid.NewGuid()
            };
            await _context.Drugs.AddAsync(drug);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> EditDrugs(DrugDto drugDto){
            var res = await _context.Drugs.FirstOrDefaultAsync(d => d.Name == drugDto.Name);
            if (res != null){
                res.Name = drugDto.Name;
                res.Description = drugDto.Description;
                res.Stock = drugDto.Stock;
                res.Price = drugDto.Price;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteDrugs(string Name){
            var res = await _context.Drugs.FirstOrDefaultAsync();
            if(res != null){
                _context.Drugs.Remove(res);
                _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<PagedResult<DrugDto>> GetFilteredDrugsAsync(
            string searchTerm,
            int page = 1,
            int pageSize = 10,
            string sortBy = "Name",
            bool ascending = true,
            Guid? categoryId = null)
        {
            var query = _context.Drugs.Include(d => d.CategoryId).AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(d =>
                    EF.Functions.Like(d.Name, $"%{searchTerm}%") ||
                    (d.Description != null && EF.Functions.Like(d.Description, $"%{searchTerm}%")));
            }

            if (categoryId.HasValue)
            {
                query = query.Where(d => d.CategoryId == categoryId.Value);
            }

            var totalCount = await query.CountAsync();

            query = sortBy switch
            {
                "Price" => ascending ? query.OrderBy(d => d.Price) : query.OrderByDescending(d => d.Price),
                _ => ascending ? query.OrderBy(d => d.Name) : query.OrderByDescending(d => d.Name),
            };

            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<DrugDto>
            {
                Items = items.Select(MapToDto),
                TotalCount = totalCount
            };
        }

         public async Task<bool> DeleteDrugAsync(Guid id)
        {
            var drug = await _context.Drugs.FindAsync(id);
            if (drug == null) return false;

            _context.Drugs.Remove(drug);
            await _context.SaveChangesAsync();
            return true;
        }

         private DrugDto MapToDto(Drug drug)
        {
            return new DrugDto
            {
                
                Name = drug.Name,
                Description = drug.Description,
                Price = drug.Price,
                Stock = drug.Stock
            };
        }

    }
}