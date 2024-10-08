using ReizzzTracking.DAL.Entities;
using ReizzzTracking.DAL.Repositories.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReizzzTracking.DAL.Repositories.TodoScheduleRepository
{
    public class TodoScheduleRepository : BaseRepository<TodoSchedule>, ITodoScheduleRepository
    {
        public TodoScheduleRepository(ReizzzTrackingV1Context context) : base(context)
        {
        }
    }
}
