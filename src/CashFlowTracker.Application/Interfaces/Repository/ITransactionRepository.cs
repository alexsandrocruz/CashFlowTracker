using CashFlowTracker.Domain.Entities;

namespace CashFlowTracker.Application.Interfaces.Repository
{
    public interface ITransactionRepository : IEntityFrameworkBaseRepository<Transaction>
    {
    }
}
