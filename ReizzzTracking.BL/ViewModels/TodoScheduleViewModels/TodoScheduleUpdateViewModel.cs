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
        public string Name { get; set; } = string.Empty;
        public DateTime? StartAt { get; set; }
        public bool? IsDone { get; set; }
        public int? EstimatedTime { get; set; }
        public long? TimeUnitId { get; set; }
    }
}
