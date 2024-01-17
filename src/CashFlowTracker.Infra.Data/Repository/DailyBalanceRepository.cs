using CashFlowTracker.Application.Interfaces.Repository;
using CashFlowTracker.Domain.Entities;

namespace CashFlowTracker.Infra.Data.Repository
{
    public class DailyBalanceRepository : EntityFrameworkBaseRepository<DailyBalance>, IDailyBalanceRepository
    {
        public DailyBalanceRepository(SqlServerContext context) : base(context)
        {
        }
    }
}
