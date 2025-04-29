using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReizzzTracking.BL.ViewModels.RoutineCollectionViewModels
{
    public class RoutineCollectionUpdateViewModel
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public bool? IsPublic { get; set; }
    }
}
