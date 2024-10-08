using ReizzzTracking.BL.ViewModels.TodoScheduleViewModels;
using ReizzzTracking.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReizzzTracking.BL.ViewModels.ToDoScheduleViewModelsToDoSchedule
{
    public class TodoScheduleGetViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime? StartAt { get; set; }
        public DateTime? EndAt { get; set; }
        public bool? IsDone { get; set; }
        public int? EstimatedTime { get; set; }
        public decimal? ActualTime { get; set; }
        public long? TimeUnitId { get; set; }
        public long? CategoryType { get; set; }
        public TodoScheduleGetViewModel FromToDoSchedule(TodoSchedule todoSchedule)
        {
            return new TodoScheduleGetViewModel
            {
                Id = todoSchedule.Id,
                Name = todoSchedule.Name,
                StartAt = todoSchedule.StartAt,
                EndAt = todoSchedule.EndAt,
                IsDone = todoSchedule.IsDone,
                EstimatedTime = todoSchedule.EstimatedTime,
                ActualTime = todoSchedule.ActualTime,
                TimeUnitId = todoSchedule.TimeUnitId,
                CategoryType = todoSchedule.CategoryType
            };
        }
    }
}
