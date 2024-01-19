namespace CashFlowTracker.Application.Interfaces
{
    public interface IConsolidationService
    {
        Task<decimal> ConsolidateTransactionsAsync(DateTime date, Guid accountId);
    }
}

