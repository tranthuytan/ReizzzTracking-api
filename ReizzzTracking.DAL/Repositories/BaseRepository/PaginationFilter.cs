using System.Linq.Expressions;

namespace ReizzzTracking.DAL.Repositories.BaseRepository
{
    public class PaginationFilter<TEntity> where TEntity : class
    {
        public List<Expression<Func<TEntity, bool>>> Conditions { get; set; } = new List<Expression<Func<TEntity, bool>>>();
    }
}
