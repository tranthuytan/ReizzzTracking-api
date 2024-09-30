using ReizzzTracking.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReizzzTracking.BL.ViewModels.RoutineCollectionViewModels
{
    public class RoutineCollectionAddViewModel
    {
        public long CreatedBy { get; set; }

        public string Name { get; set; } = "New Routine Collection";

        public bool IsPublic { get; set; }
        public RoutineCollection ToRoutineCollection(RoutineCollectionAddViewModel routineCollectionVM)
        {
            return new RoutineCollection
            {
                CreatedBy = routineCollectionVM.CreatedBy,
                Name = routineCollectionVM.Name,
                IsPublic = routineCollectionVM.IsPublic
            };
        }
    }
}
