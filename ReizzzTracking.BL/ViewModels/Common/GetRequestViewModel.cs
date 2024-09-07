using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReizzzTracking.BL.ViewModels.Common
{
    /// <summary>
    /// Handle all the HttpGet method requests
    /// </summary>
    public class GetRequestViewModel
    {
        //Send request user's Id 
        public long? RequestedById { get; set; }
        public bool IsPaginated { get; set; }
        public int PageSize { get; set; } = 20;
        public int CurrentPage { get; set; } = 1;
    }
}
