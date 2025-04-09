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
    public class InventoryService : IInventoryService
    {
         private readonly ApplicationDbContext _context;

        public InventoryService(ApplicationDbContext context){
            _context=context;
        }

        public async Task<IEnumerable<InventoryDto>> ViewInventory(){
            var inventory=await _context.Inventories.ToListAsync();
            var inventorydto=inventory.Select(p=>new InventoryDto{
                DrugName=p.Name,
                DrugQuantity=p.DrugQuantity
            });

            return inventorydto;
        }

        public async Task<bool> AddInInventory(InventoryDto inventoryDto){
           var result=await  _context.Inventories.FirstOrDefaultAsync(p=>p.Name==inventoryDto.DrugName);
           if(result==null){
                var drug=new Inventory{
                    Name=inventoryDto.DrugName,
                    DrugQuantity=inventoryDto.DrugQuantity
                };
                await _context.Inventories.AddAsync(drug);
           }
           else{
                result.DrugQuantity+=inventoryDto.DrugQuantity;
           }
            
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteInInventory(InventoryDto inventoryDto){
            var drug=await _context.Inventories.FirstOrDefaultAsync(p=>p.Name==inventoryDto.DrugName);
            if(drug==null){
                return false;
            }
            else{
                drug.DrugQuantity-=inventoryDto.DrugQuantity;
            }
            await _context.SaveChangesAsync();
            return true;
        }

    }
}