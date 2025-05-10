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
        public string Name { get; set; } = string.Empty;
        public DateTime StartAt { get; set; }
        public int EstimatedTime { get; set; }
        public long TimeUnitId { get; set; }
        public TodoSchedule ToToDoSchedule(TodoScheduleAddViewModel todoVM)
        {
            return new TodoSchedule
            {
                Name = todoVM.Name,
                StartAtUtc = todoVM.StartAt.AddHours(-7),
                EstimatedTime = todoVM.EstimatedTime,
                TimeUnitId = todoVM.TimeUnitId,
            };
        }
    }
}
