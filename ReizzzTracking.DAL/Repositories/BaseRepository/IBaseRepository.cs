using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ReizzzTracking.DAL.Repositories.BaseRepository
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        public void Add(TEntity entity);
        public void Remove(TEntity entity);
        public void Update(TEntity entity);
        public void RemoveAll(IEnumerable<TEntity> entities);
        public Task<TEntity?> Find(int id);
        public Task<TEntity?> FirstOrDefault(Expression<Func<TEntity, bool>>? expression = null, Func<IQueryable<TEntity>, IQueryable<TEntity>>? includeFunc = null);
        public Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>>? expression = null, 
                                                    Func<IQueryable<TEntity>, IQueryable<TEntity>>? includeFunc = null,
                                                    int? take = null);
        public Task<(int, IEnumerable<TEntity>)> Pagination(int page = 1,
                                                            int pageSize = 10,
                                                            Expression<Func<TEntity, bool>>? expression = null,
                                                            Func<IQueryable<TEntity>, IQueryable<TEntity>>? includeFunc = null,
                                                            PaginationFilter<TEntity>? filterList = null,
                                                            string[]? orderByProperty = null,
                                                            bool[]? descending = null
                                                            );

    }
}
