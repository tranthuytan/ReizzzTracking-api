using ReizzzTracking.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReizzzTracking.BL.ViewModels.RoutineViewModel
{
    public class RoutineUpdateViewModel
    {
        public long? Id { get; set; }

        public string StartTimeString { get; set; } = "";

        public string Name { get; set; } = "";

        public bool? IsPublic { get; set; }

        public long? CreatedBy { get; set; }
        public long CategoryType { get; set; } = 1;
        public long RoutineCollectionId { get; set; }
        public Routine ToRoutine(RoutineUpdateViewModel routineVM)
        {
            var result = new Routine
            {
                StartTime = routineVM.StartTimeString,
                Name = routineVM.Name,
                IsPublic = routineVM.IsPublic,
                CreatedBy = routineVM.CreatedBy,
                RoutineCollectionId = routineVM.RoutineCollectionId,
            };
            if (routineVM.Id != null)
            {
                result.Id = (long)routineVM.Id;
            }
            return result;
        }
    }
}
