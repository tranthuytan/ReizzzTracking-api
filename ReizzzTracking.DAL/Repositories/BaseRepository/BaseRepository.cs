using Microsoft.EntityFrameworkCore;
using ReizzzTracking.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ReizzzTracking.DAL.Repositories.BaseRepository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected readonly ReizzzTrackingV1Context _context;
        protected readonly DbSet<TEntity> _dbSet;
        public BaseRepository(ReizzzTrackingV1Context context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public async Task<TEntity?> Find(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<TEntity?> FirstOrDefault(Expression<Func<TEntity, bool>>? expression = null, Func<IQueryable<TEntity>, IQueryable<TEntity>>? includeFunc = null)
        {
            IQueryable<TEntity> query = _dbSet;
            if (expression != null)
            {
                query = query.Where(expression);
            }
            if (includeFunc != null)
            {
                query = includeFunc(query);
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>>? expression = null, Func<IQueryable<TEntity>, IQueryable<TEntity>>? includeFunc = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if (expression != null)
            {
                query = query.Where(expression);
            }

            if (includeFunc != null)
            {
                query = includeFunc(query);
            }

            return await query.ToListAsync();
        }
        public void Remove(TEntity entity)
        {
            _dbSet.Remove(entity);
        }
        public void RemoveAll(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
        }
        public void Update(TEntity entity)
        {
            var tracker = _context.Attach(entity);
            tracker.State = EntityState.Modified;
            _context.SaveChanges();

        }
        public async Task<(int, IEnumerable<TEntity>)> Pagination(int page = 1,
                                                                   int pageSize = 10,
                                                                   Expression<Func<TEntity, bool>>? expression = null,
                                                                   Func<IQueryable<TEntity>, IQueryable<TEntity>>? includeFunc = null,
                                                                   PaginationFilter<TEntity>? filterList = null,
                                                                   string? orderByProperty = null,
                                                                   bool descending = false
                                                                   )
        {
            IQueryable<TEntity> query = _dbSet;

            if (expression != null)
            {
                query = query.Where(expression);
            }
            foreach (var condition in filterList?.Conditions ?? Enumerable.Empty<Expression<Func<TEntity, bool>>>())
            {
                query = query.Where(condition);
            }
            if (includeFunc != null)
            {
                query = includeFunc(query);
            }
            if (!string.IsNullOrEmpty(orderByProperty))
            {
                query = ApplyOrderBy(query, orderByProperty, descending);
            }

            var total = await query.CountAsync();

            query = query.Skip((page - 1) * pageSize)
                .Take(pageSize);

            var data = await query.ToListAsync();

            return (total, data);
        }
        private IQueryable<TEntity> ApplyOrderBy(IQueryable<TEntity> query, string orderByProperty, bool descending = false)
        {
            var entityType = typeof(TEntity);
            var property = entityType.GetProperty(orderByProperty);
            if (property == null)
            {
                throw new ArgumentException($"{entityType} doesn't have property {orderByProperty}");
            }

            var parameter = Expression.Parameter(typeof(TEntity), "x");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExpression = Expression.Lambda(propertyAccess, parameter);
            if (descending)
            {
                query = Queryable.OrderByDescending(query, (dynamic)orderByExpression);
            }
            else
            {
                query = Queryable.OrderBy(query, (dynamic)orderByExpression);
            }
            return query;
        }
    }
}
