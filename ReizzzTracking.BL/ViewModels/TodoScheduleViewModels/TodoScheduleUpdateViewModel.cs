using ReizzzTracking.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReizzzTracking.BL.ViewModels.TodoScheduleViewModels
{
    public class TodoScheduleUpdateViewModel
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public DateTime? StartAt { get; set; }
        public DateTime? EndAt { get; set; }
        public long? AppliedFor { get; set; }
        public bool? IsDone { get; set; }
        public int? EstimatedTime { get; set; }
        public decimal? ActualTime { get; set; }
        public long? TimeUnitId { get; set; }
        public TodoSchedule ToToDoSchedule(TodoScheduleUpdateViewModel todoVM)
        {
            var result = new TodoSchedule
            {
                Name = todoVM.Name,
                StartAt = todoVM.StartAt,
                EndAt = todoVM.EndAt,
                AppliedFor = todoVM.AppliedFor,
                IsDone = todoVM.IsDone,
                EstimatedTime = todoVM.EstimatedTime,
                ActualTime = todoVM.ActualTime,
                TimeUnitId = todoVM.TimeUnitId,
                CategoryType = 2
            };
            if (todoVM.Id != null)
            {
                result.Id = (long)todoVM.Id;
            }
            return result;
        }
    }
}
