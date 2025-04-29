using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Identity.Client;
using Pharmace.API.Models.Dtos;
using PharmAce.Models.DTO;
using PharmAce.Services.Interface;

namespace PharmAce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DrugController : ControllerBase
    {
        private readonly IDrugService _drug;

        public DrugController(IDrugService drugService){
            _drug = drugService;
        }

        //All Roles
        [Authorize]
        [HttpGet("View_drugs")]
        public async Task<IActionResult> ViewDrugs(){
            var drug = await _drug.ViewDrugs();
            return Ok(drug);
        }

        [Authorize(Roles = "Admin , Supplier")]
        [HttpPost("Add_drugs")]
        public async Task<IActionResult> AddDrugs([FromBody] DrugDto drug){
            await _drug.AddDrugs(drug);
            return Ok(new {message = "Added Successfully"});
        }  

        [Authorize(Roles="Admin,Supplier")]
        [HttpPut("Edit_drug")]
        public async Task<IActionResult> EditDrugs([FromBody] DrugDto drug){
            var res = await _drug.EditDrugs(drug);
            if(res){
                return Ok(new {message = "Updated Successfully"});
            }
            return BadRequest(new {message = "Failed to Update"});
        }

        [Authorize(Roles="Admin,Supplier")]
        [HttpDelete]
        [Route("Delete_drug/{Name}")]
        public async Task<IActionResult> DeleteDrugs(string Name){
            var res = await _drug.DeleteDrugs(Name);
            if(res){
                return Ok(new {message = "Deleted Successfully"});
            }
            return BadRequest(new {message = "No  Drugs Found"});
        }

        [Authorize(Roles="Admin,Supplier")]
        [HttpGet("filter")]
        public async Task<ActionResult<PagedResult<DrugDto>>> GetFilteredDrugs(
            [FromQuery] string searchTerm = null,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string sortBy = "Name",
            [FromQuery] bool ascending = true,
            [FromQuery] Guid? categoryId = null)
        {
            var drugs = await _drug.GetFilteredDrugsAsync(searchTerm, page, pageSize, sortBy, ascending, categoryId);
            return Ok(drugs);
        }

        [Authorize(Roles="Admin,Supplier")]
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteDrug(Guid id)
        {
            var deleted = await _drug.DeleteDrugAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}