using ReizzzTracking.BL.ViewModels.Common;
using ReizzzTracking.BL.ViewModels.RoutineViewModel;
using ReizzzTracking.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReizzzTracking.BL.ViewModels.RoutineCollectionViewModels
{
    public class RoutineCollectionGetViewModel
    {
        public long Id { get; set; }
        public long CreatorId { get; set; }
        public string CreatorName { get; set; }
        public string? CollectionName { get; set; }
        public bool? IsPublic { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public ICollection<RoutineGetViewModel>? Routines { get; set; }
        public RoutineCollectionGetViewModel? FromRoutineCollection(RoutineCollection routineCollection)
        {
            RoutineCollectionGetViewModel result = new RoutineCollectionGetViewModel
            {
                Id = routineCollection.Id,
                CollectionName = routineCollection.Name,
                IsPublic = routineCollection.IsPublic,
                CreatedAt = routineCollection.CreatedAt,
                UpdatedAt = routineCollection.UpdatedAt
            };
            if (routineCollection.CreatedByNavigation is not null)
            {
                result.CreatorId = routineCollection.CreatedByNavigation.Id;
                result.CreatorName = routineCollection.CreatedByNavigation.Name;
            }
            if (routineCollection.Routines is not null && routineCollection.Routines.Any())
            {
                result.Routines = new List<RoutineGetViewModel>();
                foreach (var routine in routineCollection.Routines)
                {
                    var routineVM = new RoutineGetViewModel();
                    routineVM = routineVM.FromRoutine(routine);
                    result.Routines.Add(routineVM);
                }
            }
            return result;
        }

    }
}
