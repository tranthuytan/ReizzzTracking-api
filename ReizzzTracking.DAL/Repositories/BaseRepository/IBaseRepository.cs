using System.Linq.Expressions;

namespace ReizzzTracking.DAL.Repositories.BaseRepository
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        public void Add(TEntity entity);
        public void Remove(TEntity entity);
        public void Update(TEntity entity, params Expression<Func<TEntity, object>>[] propertyToIgnore);
        public Task<TEntity?> Find(long id);
        public Task<TEntity?> FirstOrDefault(Expression<Func<TEntity, bool>>? expression = null, Func<IQueryable<TEntity>, IQueryable<TEntity>>? includeFunc = null);
        public Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>>? expression = null,
                                                        Func<IQueryable<TEntity>, IQueryable<TEntity>>? includeFunc = null,
                                                        PaginationFilter<TEntity>? filterList = null,
                                                        string[]? orderByProperty = null,
                                                        bool[]? descending = null,
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
