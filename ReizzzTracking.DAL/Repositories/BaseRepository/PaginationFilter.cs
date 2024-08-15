using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ReizzzTracking.DAL.Common.BaseRepository
{
    public class PaginationFilter<TEntity> where TEntity : class
    {
        public List<Expression<Func<TEntity, bool>>> Conditions { get; set; } = new List<Expression<Func<TEntity, bool>>>();
    }
}
