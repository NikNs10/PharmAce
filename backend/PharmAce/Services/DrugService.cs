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

        private readonly IInventoryService _inv;
        public DrugService(ApplicationDbContext context , IConfiguration config , IInventoryService inv){
            _context = context;
            _config = config;
            _inv = inv;
        }
        // DrugReadDto :- Read DTO for Drug && Delete 
        // DrugDto :- Create DTO for Drug && Edit Drug
        // public async Task<IEnumerable<DrugReadDto>> ViewDrugs(){
        //     var res = await _context.Drugs.ToListAsync();
        //     var drug =res.Select(d => new DrugReadDto{
        //         DrugId = d.DrugId,
        //         Name = d.Name,
        //         Description = d.Description,
        //         Stock = d.Stock,                
        //         Price = d.Price
        //     });

        //     return drug;
        // }
        public async Task<IEnumerable<ViewDrugDto>> ViewDrugs(){
            var result=await _context.Drugs.ToListAsync();
            var drugdto=result.Select(d=> new ViewDrugDto
                {
                    DrugId=d.DrugId,
                    Name=d.Name,
                    Description=d.Description,
                    Stock=d.Stock,
                    Price=d.Price,
                    DrugExpiry=d.DrugExpiry,
                    CategoryId=d.CategoryId,
                    SupplierName=_context.Users.Where(t=>t.Id==d.SupplierId).Select(s=>s.Name).FirstOrDefault()?? "Unknown"
                }
            );

            return drugdto;
        }



        // public async Task AddDrugs(DrugDto drugDto){
        //     var drug = new Drug{
        //         DrugId = Guid.NewGuid(),
        //         Name = drugDto.Name,
        //         Description = drugDto.Description,
        //         Stock = drugDto.Stock,
        //         Price = drugDto.Price,
        //         CategoryId=Guid.NewGuid(),
        //         SupplierId=Guid.NewGuid()
        //     };
        //     await _context.Drugs.AddAsync(drug);
        //     await _context.SaveChangesAsync();
        // }

           public async Task<DrugInventoryDto> AddDrugs(DrugInventoryDto drugDto){
            var drug=new Drug{
                DrugId=Guid.NewGuid(),
                Name=drugDto.Name,
                Description=drugDto.Description,
                Stock=drugDto.Stock,
                Price=drugDto.Price,
                DrugExpiry=drugDto.DrugExpiry,
                CategoryId=drugDto.CategoryId,
                SupplierId=drugDto.SupplierId
            };
            await _context.Drugs.AddAsync(drug);
            await _context.SaveChangesAsync();
            
            return new DrugInventoryDto{
                DrugId=drug.DrugId,
                Name=drug.Name,
                Description=drug.Description,
                Stock=drug.Stock,
                Price=drug.Price,
                DrugExpiry=drug.DrugExpiry,
                CategoryId=drug.CategoryId,
                SupplierId=drug.SupplierId
            };
        }

        // public async Task<bool> EditDrugs(DrugDto drugDto){
        //     var res = await _context.Drugs.FirstOrDefaultAsync(d => d.Name == drugDto.Name);
        //     if (res != null){
        //         res.Name = drugDto.Name;
        //         res.Description = drugDto.Description;
        //         res.Stock = drugDto.Stock;
        //         res.Price = drugDto.Price;
        //         await _context.SaveChangesAsync();
        //         return true;
        //     }
        //     return false;
        // }

        public async Task<bool> EditDrug(DrugInventoryDto viewDrugDto){
           var drug=await  _context.Drugs.FindAsync(viewDrugDto.DrugId);
           if(drug==null){
                throw new Exception("No Drug Found");
           }
           drug.Name=viewDrugDto.Name;
           drug.Description=viewDrugDto.Description;
           drug.Price=viewDrugDto.Price;
           drug.DrugExpiry=viewDrugDto.DrugExpiry;
           drug.Stock=viewDrugDto.Stock;
           _context.Drugs.Update(drug);
           await _inv.UpdateInventoryAsync(viewDrugDto);
           await _context.SaveChangesAsync();
           return true;
        }

        // public async Task<bool> DeleteDrugs(string Name){
        //     var res = await _context.Drugs.FirstOrDefaultAsync();
        //     if(res != null){
        //         _context.Drugs.Remove(res);
        //         _context.SaveChangesAsync();
        //         return true;
        //     }
        //     return false;
        // }

        public async Task<PagedResult<DrugReadDto>> GetFilteredDrugsAsync(
    string searchTerm,
    int page = 1,
    int pageSize = 10,
    string sortBy = "Name",
    bool ascending = true,
    Guid? categoryId = null)
{
    // 1. Eager‑load the Category navigation:
    var query = _context.Drugs
                        .Include(d => d.Category)
                        .AsQueryable();

    // 2. Apply text search:
    if (!string.IsNullOrWhiteSpace(searchTerm))
    {
        query = query.Where(d =>
            EF.Functions.Like(d.Name,        $"%{searchTerm}%") ||
            EF.Functions.Like(d.Description, $"%{searchTerm}%"));
    }

    // 3. Filter by CategoryId:
    if (categoryId.HasValue)
    {
        query = query.Where(d => d.CategoryId == categoryId.Value);
    }

    // 4. Count total items before paging:
    var totalCount = await query.CountAsync();

    // 5. Apply sorting:
    query = sortBy switch
    {
        "Price" => ascending
                   ? query.OrderBy(d => d.Price)
                   : query.OrderByDescending(d => d.Price),
        _       => ascending
                   ? query.OrderBy(d => d.Name)
                   : query.OrderByDescending(d => d.Name),
    };

    // 6. Apply paging and materialize:
    var pageItems = await query
        .Skip((page - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync();

    // 7. Map to DTOs, pulling in loaded CategoryName:
    var items = pageItems.Select(MapToDto);

    return new PagedResult<DrugReadDto>
    {
        Items      = items,
        TotalCount = totalCount
    };
}

        // public async Task<PagedResult<DrugReadDto>> GetFilteredDrugsAsync(
        //     string searchTerm,
        //     int page = 1,
        //     int pageSize = 10,
        //     string sortBy = "Name",
        //     bool ascending = true,
        //     Guid? categoryId = null)
        // {
        //     var query = _context.Drugs.Include(d => d.Category).AsQueryable();

        //     if (!string.IsNullOrWhiteSpace(searchTerm))
        //     {
        //         query = query.Where(d =>
        //             EF.Functions.Like(d.Name, $"%{searchTerm}%") ||
        //             (d.Description != null && EF.Functions.Like(d.Description, $"%{searchTerm}%")));
        //     }

        //     if (categoryId.HasValue)
        //     {
        //         query = query.Where(d => d.CategoryId == categoryId.Value);
        //     }

        //     var totalCount = await query.CountAsync();

        //     query = sortBy switch
        //     {
        //         "Price" => ascending ? query.OrderBy(d => d.Price) : query.OrderByDescending(d => d.Price),
        //         _ => ascending ? query.OrderBy(d => d.Name) : query.OrderByDescending(d => d.Name),
        //     };

        //     var items = await query
        //         .Skip((page - 1) * pageSize)
        //         .Take(pageSize)
        //         .ToListAsync();

        //     return new PagedResult<DrugReadDto>
        //     {
        //         Items = items.Select(MapToDto),
        //         TotalCount = totalCount
        //     };
        // }

        //  public async Task<bool> DeleteDrugAsync(Guid id)
        // {
        //     var drug = await _context.Drugs.FindAsync(id);
        //     if (drug == null) return false;

        //     _context.Drugs.Remove(drug);
        //     await _context.SaveChangesAsync();
        //     return true;
        // }

        public async Task<bool> DeleteDrugs(Guid id){
            var drug=await _context.Drugs.FindAsync(id);
            if(drug==null){
                return false;
            }
            _context.Drugs.Remove(drug);
            // await _inventoryService.DeleteInventoryAsync(id);
            await _context.SaveChangesAsync();
            return true;
        }
         private DrugReadDto MapToDto(Drug drug)
        {
            return new DrugReadDto
            {
                DrugId = drug.DrugId,
                Name = drug.Name,
                Description = drug.Description,
                Price = drug.Price,
                Stock = drug.Stock,
                DrugExpiry = drug.DrugExpiry,
                CategoryId = drug.CategoryId,
                CategoryName = drug.Category?.CategoryName ?? "Unknown"
            };
        }

          public async Task<bool> UpdateDrugAsync(Guid id, DrugReadDto drugDto)
        {
            var existingDrug = await _context.Drugs.FindAsync(id);
            if (existingDrug == null) return false;

            

            existingDrug.Name = drugDto.Name;
            existingDrug.Description = drugDto.Description;
            existingDrug.Price = drugDto.Price;
            existingDrug.Stock = drugDto.Stock;

            await _context.SaveChangesAsync();
            return true;
        }

    }
}