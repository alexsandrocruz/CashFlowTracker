using CashFlowTracker.Domain.Models;
using System.Linq.Expressions;

namespace CashFlowTracker.Application.Interfaces.Repository
{
    public interface IEntityFrameworkBaseRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetByIdAsync(long id);
        IEnumerable<TEntity> GetAll();
        Task<IEnumerable<TEntity>> GetAllAsync();
        IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> expression);
        Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> expression);
        Pagination<TEntity> FindAllPaginate(Expression<Func<TEntity, bool>> expression, int offset, int limit = 100);
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Update(TEntity entity, Expression<Func<TEntity, object>> fieldsToUpdate);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
        void Remove(TEntity entity);
        Task RemoveAsync(long id);
        void RemoveRange(IEnumerable<TEntity> entities);
        Task SaveChangesAsync();
        IQueryable<TEntity> AsQueryable();
    }
}
