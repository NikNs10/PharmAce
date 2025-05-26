using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PharmAce.Models.DTO;
using PharmAce.Services.Interface;

namespace PharmAce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionDetailsController : ControllerBase
    {
        private readonly ITransactionDetailsService _transactionService;

        public TransactionDetailsController(ITransactionDetailsService transactionService)
        {
            _transactionService = transactionService;
        }

        // GET: api/TransactionDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransactionDetailsDto>>> GetAllTransactionDetails()
        {
            var transactions = await _transactionService.GetAllTransactionDetailsAsync();
            return Ok(transactions);
        }

        // GET: api/TransactionDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionDetailsDto>> GetTransactionDetails(Guid id)
        {
            var transaction = await _transactionService.GetTransactionDetailsByIdAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }
            return Ok(transaction);
        }

        // POST: api/TransactionDetails
        [HttpPost]
        public async Task<ActionResult<TransactionDetailsDto>> CreateTransactionDetails(CreateTransactionDetailsDto createTransactionDetailsDto)
        {
            var transaction = await _transactionService.CreateTransactionDetailsAsync(createTransactionDetailsDto);
            return CreatedAtAction(nameof(GetTransactionDetails), new { id = transaction.TransactionId }, transaction);
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransactionDetails(Guid id)
        {
            var result = await _transactionService.DeleteTransactionDetailsAsync(id);
            if (result==false)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}