using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PharmAce.Data;
using PharmAce.Models.DTO;
using PharmAce.Services.Interface;
using PharmAce.Models;

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

    }
}