using CashFlowTracker.Application.Interfaces.Repository;
using CashFlowTracker.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CashFlowTracker.Infra.Data.Repository
{

    public abstract class EntityFrameworkBaseRepository<TEntity> : IEntityFrameworkBaseRepository<TEntity> where TEntity : class
    {
        protected readonly SqlServerContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public EntityFrameworkBaseRepository(SqlServerContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public virtual void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        public virtual void Update(TEntity entity, Expression<Func<TEntity, object>> fieldsToUpdate)
        {
            var entry = _context.Entry(entity);

            if (entry.State == EntityState.Detached)
                _dbSet.Attach(entity);

            var propertyNames = GetPropertyNames(fieldsToUpdate);

            foreach (var propertyName in propertyNames)
                entry.Property(propertyName).IsModified = true;

            _context.SaveChanges();
        }

        public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public virtual IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> expression)
        {
            return _dbSet.Where(expression);
        }

        public virtual async Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await _dbSet.Where(expression).ToListAsync();
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return _dbSet.ToList();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual Pagination<TEntity> FindAllPaginate(Expression<Func<TEntity, bool>> expression, int offset, int limit = 100)
        {
            var query = _dbSet.Where(expression).AsQueryable();

            return Paginate(query, offset, limit);
        }

        public virtual Pagination<TEntity> Paginate(IQueryable<TEntity> query, int offset, int limit = 100)
        {
            return new Pagination<TEntity>
            {
                Offset = offset,
                Limit = limit,
                Total = query.Count(),
                Items = query.Skip(offset).Take(limit).ToList()
            };
        }

        public virtual async Task<TEntity> GetByIdAsync(long id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual void Remove(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public virtual async Task RemoveAsync(long id)
        {
            var entity = await GetByIdAsync(id);
            Remove(entity);
        }

        public virtual void RemoveRange(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        //public virtual IQueryable<TEntity> FromSqlRaw(string sql, object args)
        //{
        //    return _dbSet.FromSqlRaw(sql, args);
        //}

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public IQueryable<TEntity> AsQueryable()
        {
            return _dbSet.AsQueryable();
        }

        private static string[] GetPropertyNames(Expression<Func<TEntity, object>> fieldsToUpdate)
        {
            if (fieldsToUpdate.Body is NewExpression newExpression)
                return newExpression.Members.Select(m => m.Name).ToArray();

            if (fieldsToUpdate.Body is MemberExpression memberExpression)
                return new[] { memberExpression.Member.Name };

            if (fieldsToUpdate.Body is UnaryExpression unaryExpression && unaryExpression.Operand is MemberExpression operand)
                return new[] { operand.Member.Name };

            throw new ArgumentException("The provided expression is not valid for updating fields.");
        }
    }
}
