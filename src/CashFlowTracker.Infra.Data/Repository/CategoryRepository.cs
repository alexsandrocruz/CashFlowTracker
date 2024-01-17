using CashFlowTracker.Application.Interfaces.Repository;
using CashFlowTracker.Domain.Entities;

namespace CashFlowTracker.Infra.Data.Repository
{
    public class CategoryRepository : EntityFrameworkBaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(SqlServerContext context) : base(context)
        {
        }
    }
}
