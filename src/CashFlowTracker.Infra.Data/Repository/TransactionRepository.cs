using CashFlowTracker.Application.Interfaces.Repository;
using CashFlowTracker.Domain.Entities;

namespace CashFlowTracker.Infra.Data.Repository
{
    public class TransactionRepository : EntityFrameworkBaseRepository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(SqlServerContext context) : base(context)
        {
        }
    }
}
