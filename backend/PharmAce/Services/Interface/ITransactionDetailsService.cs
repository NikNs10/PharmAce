using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PharmAce.Models.DTO;

namespace PharmAce.Services.Interface
{
    public interface ITransactionDetailsService
    {
        Task<IEnumerable<TransactionDetailsDto>> GetAllTransactionDetailsAsync();
        Task<TransactionDetailsDto> GetTransactionDetailsByIdAsync(Guid transactionId);
        Task<TransactionDetailsDto> CreateTransactionDetailsAsync(CreateTransactionDetailsDto createTransactionDetailsDto);
        // Task<TransactionDetailsDto> UpdateTransactionDetailsAsync(Guid transactionId, UpdateTransactionDetailsDto updateTransactionDetailsDto);
        Task<bool> DeleteTransactionDetailsAsync(Guid transactionId);
    }
}