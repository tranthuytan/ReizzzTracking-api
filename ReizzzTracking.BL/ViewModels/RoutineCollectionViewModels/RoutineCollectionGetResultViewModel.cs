using ReizzzTracking.BL.ViewModels.Common;
using ReizzzTracking.BL.ViewModels.RoutineCollectionViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReizzzTracking.BL.ViewModels.ResultViewModels.RoutineCollectionViewModel
{
    public class RoutineCollectionGetResultViewModel : ResultViewModel
    {
        public PaginationGetViewModel<RoutineCollectionGetViewModel> PaginatedResult { get; set; }
    }
}
