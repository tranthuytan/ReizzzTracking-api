using ReizzzTracking.BL.ViewModels.Common;
using ReizzzTracking.BL.ViewModels.ResultViewModels;
using ReizzzTracking.BL.ViewModels.ToDoScheduleViewModelsToDoSchedule;
using ReizzzTracking.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReizzzTracking.BL.ViewModels.TodoScheduleViewModels
{
    public class TodoScheduleGetResultViewModel : ResultViewModel
    {
        public PaginationGetViewModel<TodoScheduleGetViewModel> PaginatedResult { get; set; } = new();
    }
}
