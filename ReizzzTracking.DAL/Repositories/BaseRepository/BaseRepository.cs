using Microsoft.EntityFrameworkCore;
using ReizzzTracking.DAL.Entities;
using System.Linq.Expressions;

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

        public async Task<TEntity?> Find(long id)
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

        public async Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>>? expression = null,
                                                        Func<IQueryable<TEntity>, IQueryable<TEntity>>? includeFunc = null,
                                                        PaginationFilter<TEntity>? filterList = null,
                                                        string[]? orderByProperty = null,
                                                        bool[]? descending = null,
                                                        int? take = null)
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
            if (take != null)
            {
                query = query.Take(take ?? default(int));
            }

            if (orderByProperty != null && orderByProperty.Any())
            {
                var orderedQuery = ApplyOrderBy(query, orderByProperty[0], descending[0]);
                for (int i = 1; i < orderByProperty.Length; i++)
                {
                    orderedQuery = ThenApplyOrderBy(orderedQuery, orderByProperty[i], descending[i]);
                }
                query = orderedQuery;
            }

            return await query.ToListAsync();
        }
        public void Remove(TEntity entity)
        {
            _dbSet.Remove(entity);
        }
        public void Update(TEntity entity, params Expression<Func<TEntity, object>>[] propertiesToIgnore)
        {
            var tracker = _context.Attach(entity);
            tracker.State = EntityState.Modified;
            foreach (var property in propertiesToIgnore)
            {
                _context.Entry(entity).Property(property).IsModified = false;
            }
        }
        public async Task<(int, IEnumerable<TEntity>)> Pagination(
                                                                   int page = 1,
                                                                   int pageSize = 10,
                                                                   Expression<Func<TEntity, bool>>? expression = null,
                                                                   Func<IQueryable<TEntity>, IQueryable<TEntity>>? includeFunc = null,
                                                                   PaginationFilter<TEntity>? filterList = null,
                                                                   string[]? orderByProperty = null,
                                                                   bool[]? descending = null
                                                                   )
        {
            if (descending != null && orderByProperty != null && (descending.Length != orderByProperty.Length))
            {
                throw new ArgumentException("desceding's Length and orderByPropery's Length must be the same");
            }
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
            if (orderByProperty != null && orderByProperty.Any())
            {
                var orderedQuery = ApplyOrderBy(query, orderByProperty[0], descending[0]);
                for (int i = 1; i < orderByProperty.Length; i++)
                {
                    orderedQuery = ThenApplyOrderBy(orderedQuery, orderByProperty[i], descending[i]);
                }
                query = orderedQuery;
            }

            var total = await query.CountAsync();

            query = query.Skip((page - 1) * pageSize)
                .Take(pageSize);

            var data = await query.ToListAsync();

            return (total, data);
        }
        private IOrderedQueryable<TEntity> ApplyOrderBy(IQueryable<TEntity> query, string orderByProperty, bool descending = false)
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
                return Queryable.OrderByDescending(query, (dynamic)orderByExpression);
            }
            return Queryable.OrderBy(query, (dynamic)orderByExpression);
        }
        private IOrderedQueryable<TEntity> ThenApplyOrderBy(IOrderedQueryable<TEntity> query, string orderByProperty, bool descending = false)
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
                return query = Queryable.ThenByDescending(query, (dynamic)orderByExpression);
            }
            return query = Queryable.ThenBy(query, (dynamic)orderByExpression);
        }
    }
}
