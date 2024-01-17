using CashFlowTracker.Application.Interfaces.Repository;
using CashFlowTracker.Domain.Entities;

namespace CashFlowTracker.Infra.Data.Repository
{
    public class ConsolidationLogRepository : EntityFrameworkBaseRepository<ConsolidationLog>, IConsolidationLogRepository
    {
        public ConsolidationLogRepository(SqlServerContext context) : base(context)
        {
        }
    }
}
