using ReizzzTracking.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReizzzTracking.BL.ViewModels.RoutineViewModel
{
    public class RoutineGetViewModel
    {
        public long Id { get; set; }

        public string? StartTime { get; set; }

        public string? Name { get; set; }

        public bool? IsPublic { get; set; }

        public long? CreatedBy { get; set; }

        public long? CategoryType { get; set; }

        public long? RoutineCollectionId { get; set; }
        public RoutineGetViewModel FromRoutine(Routine routine)
        {
            return new RoutineGetViewModel
            {
                Id = routine.Id,
                StartTime = routine.StartTime,
                Name = routine.Name,
                IsPublic = routine.IsPublic,
                CreatedBy = routine.CreatedBy,
                CategoryType = routine.CategoryType,
                RoutineCollectionId = routine.RoutineCollectionId
            };
        }
        public Routine ToRoutine(RoutineGetViewModel routineVM)
        {
            return new Routine
            {
                Id = routineVM.Id,
                StartTime = routineVM.StartTime,
                Name = routineVM.Name,
                IsPublic = routineVM.IsPublic,
                CreatedBy = routineVM.CreatedBy,
                CategoryType = routineVM.CategoryType,
                RoutineCollectionId = routineVM.RoutineCollectionId
            };
        }
    }
}
