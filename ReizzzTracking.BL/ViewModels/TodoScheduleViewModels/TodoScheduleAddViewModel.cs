using ReizzzTracking.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReizzzTracking.BL.ViewModels.TodoScheduleViewModels
{
    public class TodoScheduleAddViewModel
    {
        public string Name { get; set; }
        public DateTime? StartAt { get; set; } = DateTime.Now;
        public bool? IsDone { get; set; } = false;
        public int? EstimatedTime { get; set; }
        public long? TimeUnitId { get; set; }
        public long? CategoryType { get; set; } = 2;
        public TodoSchedule ToToDoSchedule(TodoScheduleAddViewModel todoVM)
        {
            return new TodoSchedule
            {
                Name = todoVM.Name,
                StartAt = todoVM.StartAt,
                IsDone = todoVM.IsDone,
                EstimatedTime = todoVM.EstimatedTime,
                TimeUnitId = todoVM.TimeUnitId,
                CategoryType = todoVM.CategoryType,
            };
        }
    }
}
