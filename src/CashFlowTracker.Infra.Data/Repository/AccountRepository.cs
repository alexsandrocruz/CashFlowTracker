using CashFlowTracker.Application.Interfaces.Repository;
using CashFlowTracker.Domain.Entities;

namespace CashFlowTracker.Infra.Data.Repository
{
    public class AccountRepository : EntityFrameworkBaseRepository<Account>, IAccountRepository
    {
        public AccountRepository(SqlServerContext context) : base(context)
        {
        }
    }
}
