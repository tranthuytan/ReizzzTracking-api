using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReizzzTracking.BL.ViewModels.Common
{
    /// <summary>
    /// Handle all HttpGet method responses
    /// </summary>
    public class PaginationGetViewModel<T> where T : class
    {
        public bool IsPaginated { get; set; } 
        public int TotalRecord { get; set; }
        public int? PageSize { get; set; }
        public int? CurrentPage { get; set; }
        public int? TotalPage => TotalRecord / PageSize + 1;
        public ICollection<T> Data { get; set; } = new List<T>();

    }
}
