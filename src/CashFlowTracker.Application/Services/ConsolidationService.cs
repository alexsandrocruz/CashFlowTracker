using CashFlowTracker.Application.Interfaces;
using CashFlowTracker.Application.Interfaces.Repository;

namespace CashFlowTracker.Application.Services
{
    public class ConsolidationService : IConsolidationService
    {
        private readonly ITransactionRepository _transactionRepository;

        public ConsolidationService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<decimal> ConsolidateTransactionsAsync(DateTime date, Guid accountId)
        {
            var transactions = await _transactionRepository.FindAllAsync(
                t => t.AccountId == accountId && t.TransactionDate.Date == date.Date);

            var total = transactions.Sum(t => t.Amount); // Assumindo que Amount já considera débitos e créditos

            return total;
        }
    }
}
