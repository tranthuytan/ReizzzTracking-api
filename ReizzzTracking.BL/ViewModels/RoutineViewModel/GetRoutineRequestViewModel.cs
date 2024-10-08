using ReizzzTracking.BL.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReizzzTracking.BL.ViewModels.RoutineViewModel
{
    /// <summary>
    /// Handle all the HttpGet method requests
    /// </summary>
    public class GetRoutineRequestViewModel : GetRequestViewModel
    {
        public long RoutineCollectionId { get; set; }
    }
}
