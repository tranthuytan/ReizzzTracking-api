using ReizzzTracking.BL.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReizzzTracking.BL.ViewModels.TodoScheduleViewModels
{
    /// <summary>
    /// Handle all the HttpGet method requests
    /// </summary>
    public class GetTodoScheduleRequestViewModel : GetRequestViewModel
    {
        public long UserId { get; set; }
    }
}
