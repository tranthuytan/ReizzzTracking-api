using ReizzzTracking.BL.ViewModels.Common;
using ReizzzTracking.BL.ViewModels.ResultViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReizzzTracking.BL.ViewModels.RoutineViewModel
{
    public class RoutineGetResultViewModel : ResultViewModel
    {
        public PaginationGetViewModel<RoutineGetViewModel> PaginatedResult { get; set; } = new();
    }
}
