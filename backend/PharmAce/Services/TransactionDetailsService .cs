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
    public class TransactionDetailsService : ITransactionDetailsService
        
    {
        private readonly ApplicationDbContext _context;

        private readonly IConfiguration _configuration;
        public TransactionDetailsService(ApplicationDbContext context , IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
      

        public async Task<IEnumerable<TransactionDetailsDto>> GetAllTransactionDetailsAsync()
        {
            var transactions = await _context.TransactionDetails.ToListAsync();
            return transactions.Select(t => new TransactionDetailsDto
            {
                TransactionId = t.TransactionId,
                Status = t.Status,
                Date = t.Date,
                PaymentMethod = t.PaymentMethod,
                amount = t.amount
            });
        }

        public async Task<TransactionDetailsDto> GetTransactionDetailsByIdAsync(Guid transactionId)
        {
            var transaction = await _context.TransactionDetails.FindAsync(transactionId);
            if (transaction == null)
                return null;

            return new TransactionDetailsDto
            {
                TransactionId = transaction.TransactionId,
                Status = transaction.Status,
                Date = transaction.Date,
                PaymentMethod = transaction.PaymentMethod,
                amount = transaction.amount
            };
        }

        public async Task<TransactionDetailsDto> CreateTransactionDetailsAsync(CreateTransactionDetailsDto createTransactionDetailsDto)
        {
            var transaction = new TransactionDetail
            {
                TransactionId = Guid.NewGuid(),
                Status = createTransactionDetailsDto.Status,
                Date = createTransactionDetailsDto.Date,
                PaymentMethod = createTransactionDetailsDto.PaymentMethod,
                amount = createTransactionDetailsDto.amount
            };

            _context.TransactionDetails.Add(transaction);
            await _context.SaveChangesAsync();


            return new TransactionDetailsDto
            {
                TransactionId = transaction.TransactionId,
                Status = transaction.Status,
                Date = transaction.Date,
                PaymentMethod = transaction.PaymentMethod,
                amount = transaction.amount
            };
        }

       

        public async Task<bool> DeleteTransactionDetailsAsync(Guid transactionId)
        {
            var transaction = await _context.TransactionDetails.FindAsync(transactionId);
            if (transaction == null)
                return false;

            _context.TransactionDetails.Remove(transaction);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
