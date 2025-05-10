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
        public string Name { get; set; } = string.Empty;
        public DateTime? StartAt => StartAtUtc.GetValueOrDefault().AddHours(7);
        public DateTime? StartAtUtc { get; set; }
        public DateTime? EndAt => EndAtUtc is null ? null : EndAtUtc.GetValueOrDefault().AddHours(7);
        public DateTime? EndAtUtc { get; set; }
        public bool? IsDone { get; set; }
        public int? EstimatedTime { get; set; }
        public decimal? ActualTime { get; set; }
        public string? TimeUnitString { get; set; }
        public long? CategoryType { get; set; }
        public TodoScheduleGetViewModel FromToDoSchedule(TodoSchedule todoSchedule)
        {
            return new TodoScheduleGetViewModel
            {
                Id = todoSchedule.Id,
                Name = todoSchedule.Name,
                StartAtUtc = todoSchedule.StartAtUtc,
                EndAtUtc = todoSchedule.EndAtUtc,
                IsDone = todoSchedule.IsDone,
                EstimatedTime = todoSchedule.EstimatedTime,
                ActualTime = todoSchedule.ActualTime,
                TimeUnitString = TimeUnit.FromValue((int)todoSchedule.TimeUnitId!)!.Name,
                CategoryType = todoSchedule.CategoryType
            };
        }
    }
}
