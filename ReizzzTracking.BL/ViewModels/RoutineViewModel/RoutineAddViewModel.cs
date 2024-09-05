using ReizzzTracking.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReizzzTracking.BL.ViewModels.RoutineViewModel
{
    public class RoutineAddViewModel
    {
        public string Name { get; set; }
        public string StartTimeString { get; set; }
        public bool IsPublic { get; set; }
        public long CategoryTypeId { get; set; }
        public long? RoutineCollectionId { get; set; }
        public Routine ToRoutine(RoutineAddViewModel routineVM)
        {
            return new Routine
            {
                Name = routineVM.Name,
                StartTime = routineVM.StartTimeString,
                IsPublic = routineVM.IsPublic,
                CategoryType = routineVM.CategoryTypeId,
                RoutineCollectionId = routineVM.RoutineCollectionId,
            };
        }
    }
}
